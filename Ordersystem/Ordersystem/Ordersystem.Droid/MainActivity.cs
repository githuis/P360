using System;
using Ordersystem.Functions;
using System.Collections.Generic;
using Ordersystem.Model;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Ordersystem.Utilities;
using Android.Graphics;

namespace Ordersystem.Droid
{
	// OLD, keep for backup [Activity (Label = "Ordersystem", MainLauncher = true, Icon = "@drawable/icon")]
	[Activity (Label = "Ordersystem", MainLauncher = true, Icon = "@drawable/icon", ScreenOrientation = Android.Content.PM.ScreenOrientation.Landscape,Theme = "@android:style/Theme.Holo.Light.NoActionBar")]
	public class MainActivity : Activity
	{
		LocalManager localManager;
		LayoutHandler layoutHandler;
		List<TableRow> rows;
		Customer sessionCustomer { get { return localManager.Customer; } }
		Orderlist sessionOrderlist { get { return localManager.Orderlist; } }
		private int daysInMonth = 0;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			Xamarin.Forms.Forms.Init (this, bundle);
			InitializeLogInScreen ();
		}

		public void LogIn(Button button,EditText editText, TextView errorMsg)
		{
			button.Click += delegate
			{		
				if(localManager.IsValidSocialSecurityNumber(editText.Text))
				{
					localManager.LogIn(editText.Text);
					daysInMonth = sessionOrderlist.DayMenus.Count;
					sessionCustomer.Order.SetSelectionLength(daysInMonth, sessionOrderlist);
					layoutHandler.SetCustomerAndList(sessionCustomer, sessionOrderlist);

					SetContentView (Resource.Layout.Main_Window);
					CreateMainWindow ();
				}
				else
				{
					errorMsg.Visibility = ViewStates.Visible;	
				}
			};
		}
			
		public void CreateMainWindow()
		{
			AddRowsToList ((TableLayout)FindViewById (Resource.Id.tableLayout1));
			InitializeRows ();

			InitializeTutorial ();
			InitializeHeader ();
			InitializeOrderButton ();
			Dish.SelectedDishes = new Dish[33];
			Dish.SelectedSideDishes = new Dish[33];
		}

		private void AddRowsToList(TableLayout table)
		{
			for (int i = 0; i < table.ChildCount; i++) 
			{
				View view = table.GetChildAt(i);
				if (view is TableRow) 
				{
					TableRow row = (TableRow)view;
					rows.Add (row);
				}
			}
		}

		private void InitializeRows()
		{
			TableLayout tableLyout = (TableLayout) FindViewById(Resource.Id.tableLayout1);
			int index;

			foreach (var row in rows)
			{
				TextView v = (TextView) row.GetChildAt(0);
				v.SetTextSize (Android.Util.ComplexUnitType.Px, 24);
				index = tableLyout.IndexOfChild (row) / 2;
				row.SetBackgroundColor(layoutHandler.RowBackgroundColor);
				row.SetMinimumHeight (layoutHandler.GetMinimumHeight ());
				layoutHandler.ClearRowText (row, sessionCustomer.Order.DayMenuSelections[index].Date);
				if (index > daysInMonth)
					row.Visibility = ViewStates.Gone;

				row.Click += (object sender, EventArgs e) => {
					layoutHandler.ResizeTableRow(rows, (TableRow) sender, (TableLayout)FindViewById (Resource.Id.tableLayout1));
				};
			}
		}

		private void InitializeTutorial()
		{
			FindViewById (Resource.Id.tutorialPane).SetBackgroundColor (layoutHandler.TutorialColor);
			(FindViewById (Resource.Id.tutorialTitle) as TextView).SetTextColor (layoutHandler.TutorialText);
			(FindViewById (Resource.Id.tutorialText) as TextView).SetTextColor (layoutHandler.TutorialText);
			ImageView btn = (ImageView) FindViewById (Resource.Id.tutorialExit);
			btn.SetImageResource(Resource.Drawable.Delete);
			btn.Click += (object sender, EventArgs e) => {
				TableLayout tl = (TableLayout) FindViewById(Resource.Id.tableLayout1);
				//tl.RemoveView(FindViewById(Resource.Id.tutorialPane));
				FindViewById(Resource.Id.tutorialPane).Visibility = ViewStates.Gone;
			};

		}

		private void InitializeHeader()
		{
			LinearLayout header = (LinearLayout)FindViewById (Resource.Id.header);
			Button btn = (Button)FindViewById (Resource.Id.headerLogOut);
			TextView headerUserName = (TextView)FindViewById (Resource.Id.headerUserName);

			header.SetBackgroundColor (layoutHandler.HeaderColor);
			header.SetMinimumHeight (layoutHandler.GetMinimumHeight ());

			headerUserName.Text = sessionCustomer.Name;
			//Mangler logUd knap.
			//btn.SetImageResource (Resource.Drawable.Delete);
			btn.Click += (object sender, EventArgs e) => {
				SetContentView(Resource.Layout.Log_In);
				InitializeLogInScreen();
			};
				
		}

		private void InitializeLogInScreen()
		{
			// Set our view to the Log_In
			SetContentView (Resource.Layout.Log_In);

			//Finding the different widgets ID, to use in the LogIn method a few lines below.
			Button button = FindViewById<Button> (Resource.Id.loginButton);
			EditText editText = FindViewById<EditText> (Resource.Id.loginInputBar);
			TextView errorMsg = FindViewById<TextView> (Resource.Id.loginErrorMessageText); 

			//Initialize managers
			layoutHandler = new LayoutHandler(this);
			localManager = new LocalManager();

			//Initialize list for TableRows.
			rows = new List<TableRow>();

			//Set screensize
			Point p = new Point(0,0);
			WindowManager.DefaultDisplay.GetSize(p);
			layoutHandler.SetDisplaySize (p);

			//Checks  the users login info
			LogIn(button,editText,errorMsg);
		}

		private void InitializeOrderButton ()
		{
			Button button = FindViewById<Button> (Resource.Id.sendOrderButton);
			button.SetMinimumHeight (layoutHandler.GetMediumHeight());

			button.Click += (object sender, EventArgs e) => {
				//Call the sending of the order here.
				CloseAllRows();
				CheckAllChoicesFilled(sender, e);

			};
		}

		private void SendOrderClick(object sender, EventArgs e)
		{
			Android.App.AlertDialog.Builder builder = new AlertDialog.Builder (this);
			AlertDialog alertDialog = builder.Create ();
			alertDialog.SetTitle ("Send bestilling?");
			alertDialog.SetMessage ("Der er nogle dage, hvor der ikke er valgt mad. Send alligevel?\nSå udfylder Aalborg madservice for dig.");

			alertDialog.SetButton ("Send alligevel", (s, ev) => {
				localManager.FillInvalidOrder();
				localManager.SendOrder();
			});

			alertDialog.SetButton2 ("Gå tilbage", (s, ev) => {
				ErrorRowsOnReturn();
				//TableLayout tableLayout = (TableLayout) FindViewById(Resource.Id.tableLayout1);

			});

			alertDialog.Show ();
		}

		private void CheckAllChoicesFilled(object sender, EventArgs e)
		{
			if (!localManager.IsOrderValid ())
				SendOrderClick (sender, e);
			else
				localManager.SendOrder ();
		}

		private void ErrorRowsOnReturn()
		{
			TableLayout tableLayout = (TableLayout) FindViewById(Resource.Id.tableLayout1);
			int index;

			foreach (var row in rows)
			{
				if(layoutHandler.IsOpen(row))
					layoutHandler.CloseRow (tableLayout, row);
				index = tableLayout.IndexOfChild (row) / 2;
				if (sessionCustomer.Order.DayMenuSelections [index].Choice == Ordersystem.Enums.DayMenuChoice.NoChoice)
					layoutHandler.SetRowErrorColor (row);
			}
		}

		private void CloseAllRows()
		{
			TableLayout tableLayout = (TableLayout) FindViewById(Resource.Id.tableLayout1);
			layoutHandler.ForceCloseInfoRow (tableLayout);
		}
	}
}


