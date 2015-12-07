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
using Android.Graphics;

namespace Ordersystem.Droid
{
	// OLD = [Activity (Label = "Ordersystem", MainLauncher = true, Icon = "@drawable/icon")]
	//Test too see if this forces landscape mode in the main screen.
	[Activity (Label = "Ordersystem", MainLauncher = true, Icon = "@drawable/icon", ScreenOrientation = Android.Content.PM.ScreenOrientation.Landscape)]
	public class MainActivity : Activity
	{
		CommunicationManager communicationManager;
		LayoutHandler layoutHandler;
		List<TableRow> rows;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view to the Log_In
			SetContentView (Resource.Layout.Log_In);

			//Finding the different widgets ID, to use in the LogIn method a few lines below.
			Button button = FindViewById<Button> (Resource.Id.loginButton);
			EditText editText = FindViewById<EditText> (Resource.Id.loginInputBar);
			TextView errorMsg = FindViewById<TextView> (Resource.Id.loginErrorMessageText); 

			//Initialize managers
			communicationManager = new CommunicationManager();
			layoutHandler = new LayoutHandler(this);

			//Initialize list for TableRows.
			rows = new List<TableRow>();

			//Set screensize
			Point p = new Point(0,0);
			WindowManager.DefaultDisplay.GetSize(p);
			layoutHandler.SetDisplaySize (p);

			//Checks  the users login info
			LogIn(button,editText,errorMsg);
		}

		public void LogIn(Button button,EditText editText, TextView errorMsg)
		{
			button.Click += delegate 
			{
				if(editText.Text == "") // START DEBUG ONLY -- MUST BE REMOVED BEFORE SHIPPING -- CRITICAL
				{
					SetContentView (Resource.Layout.Main_Window);
					CreateMainWindow ();
				} // END DEBUG ONLY
				else if(communicationManager.ValidSocialSecurityNumber(editText.Text))
				{
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
			Dish.SelectedDishes = new Dish[31];
			
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
			foreach (var row in rows)
			{
				TextView v = (TextView) row.GetChildAt(0);
				v.SetTextSize (Android.Util.ComplexUnitType.Px, 32);
				row.SetBackgroundColor(layoutHandler.RowBackgroundColor);
				row.SetMinimumHeight (layoutHandler.GetMinimumHeight ());

				row.Click += (object sender, EventArgs e) => {
					layoutHandler.ResizeTableRow(rows, (TableRow) sender, (TableLayout)FindViewById (Resource.Id.tableLayout1));
				};
			}
		}
	}
}


