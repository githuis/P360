﻿using System;
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
		LocalManager lm;
		LayoutHandler layoutHandler;
		List<TableRow> rows;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			Xamarin.Forms.Forms.Init (this, bundle);

			lm = new LocalManager();

			InitializeLogInScreen ();
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
				else if(lm.IsValidSocialSecurityNumber(editText.Text))
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
			foreach (var row in rows)
			{
				TextView v = (TextView) row.GetChildAt(0);
				v.SetTextSize (Android.Util.ComplexUnitType.Px, 32);
				row.SetBackgroundColor(layoutHandler.RowBackgroundColor);
				row.SetMinimumHeight (layoutHandler.GetMinimumHeight ());
				layoutHandler.ClearRowText (row, DateTime.Now); //  FIX DATE

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

			//headerUserName = Model.Customer.name;
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
				SendOrderClick(sender, e);
			};
		}

		private void SendOrderClick(object sender, EventArgs e)
		{
			/*Android.App.AlertDialog.Builder builder = new AlertDialog.Builder (this);
			AlertDialog alertDialog = builder.Create ();
			alertDialog.SetTitle ("Send bestilling?");
			alertDialog.SetMessage ("Der er nogle dage, hvor der ikke er valgt mad. Send alligevel?");

			alertDialog.SetButton ("Send alligevel", (s, ev) => {
				lm.IsOrderValid();
				lm.SendOrder();
			});

			alertDialog.SetButton2 ("Gå tilbage", (s, ev) => {
				
			});

			alertDialog.Show ();*/
		}
	}
}


