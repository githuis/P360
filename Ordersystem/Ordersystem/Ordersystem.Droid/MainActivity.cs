using System;
using Ordersystem.Functions;
using System.Collections.Generic;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Ordersystem.Utilities;

namespace Ordersystem.Droid
{
	[Activity (Label = "Ordersystem.Droid", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		//Made a communicationManager object
		LocalManager lm = new LocalManager();

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view to the Log_In
			SetContentView (Resource.Layout.Log_In);

			//Finding the different widgets ID, so i can use them
			Button button = FindViewById<Button> (Resource.Id.myButton);
			EditText editText = FindViewById<EditText> (Resource.Id.editText1);
			TextView errorMsg = FindViewById<TextView> (Resource.Id.errorMsg); 

			LogIn(button,editText,errorMsg);
		}

		public void LogIn(Button button,EditText editText, TextView errorMsg)
		{
			button.Click += delegate
			{
				if(lm.IsValidSocialSecurityNumber(editText.Text))
				{
					SetContentView (Resource.Layout.Main_Window);
					//CreateMainWindow ();
				}
				else
				{
					errorMsg.Visibility = ViewStates.Visible;	
				}
			};
		}


		 //A try at making the row system, where all the rows already are made, and you need to set them to visable or invisable. And change the buttons and text to them hard code style
		 //Its shit.
		public void CreateMainWindow()
		{
			//TableLayout Table1 = FindViewById<TableLayout> (Resource.Id.table);
			int i;

			TableRow tablerow1 = FindViewById<TableRow> (Resource.Id.row1);
			TableRow tablerow2 = FindViewById<TableRow> (Resource.Id.row2);
			TableRow tablerow3 = FindViewById<TableRow> (Resource.Id.row3);
			TableRow tablerow4 = FindViewById<TableRow> (Resource.Id.row4);
			TableRow tablerow5 = FindViewById<TableRow> (Resource.Id.row5);
			TableRow tablerow6 = FindViewById<TableRow> (Resource.Id.row6);
			TableRow tablerow7 = FindViewById<TableRow> (Resource.Id.row7);
			TableRow tablerow8 = FindViewById<TableRow> (Resource.Id.row8);
			TableRow tablerow9 = FindViewById<TableRow> (Resource.Id.row9);
			TableRow tablerow10 = FindViewById<TableRow> (Resource.Id.row10);
			TableRow tablerow11 = FindViewById<TableRow> (Resource.Id.row11);
			TableRow tablerow12 = FindViewById<TableRow> (Resource.Id.row12);
			TableRow tablerow13 = FindViewById<TableRow> (Resource.Id.row13);
			TableRow tablerow14 = FindViewById<TableRow> (Resource.Id.row14);
			TableRow tablerow15 = FindViewById<TableRow> (Resource.Id.row15);
			TableRow tablerow16 = FindViewById<TableRow> (Resource.Id.row16);
			TableRow tablerow17 = FindViewById<TableRow> (Resource.Id.row17);
			TableRow tablerow18 = FindViewById<TableRow> (Resource.Id.row18);
			TableRow tablerow19 = FindViewById<TableRow> (Resource.Id.row19);
			TableRow tablerow20 = FindViewById<TableRow> (Resource.Id.row20);
			TableRow tablerow21 = FindViewById<TableRow> (Resource.Id.row21);
			TableRow tablerow22 = FindViewById<TableRow> (Resource.Id.row22);
			TableRow tablerow23 = FindViewById<TableRow> (Resource.Id.row23);
			TableRow tablerow24 = FindViewById<TableRow> (Resource.Id.row24);
			TableRow tablerow25 = FindViewById<TableRow> (Resource.Id.row25);
			TableRow tablerow26 = FindViewById<TableRow> (Resource.Id.row26);
			TableRow tablerow27 = FindViewById<TableRow> (Resource.Id.row27);
			TableRow tablerow28 = FindViewById<TableRow> (Resource.Id.row28);
			TableRow tablerow29 = FindViewById<TableRow> (Resource.Id.row29);
			TableRow tablerow30 = FindViewById<TableRow> (Resource.Id.row30);
			TableRow tablerow31 = FindViewById<TableRow> (Resource.Id.row31);


			TextView table_text1 = FindViewById<TextView> (Resource.Id.row_name1);
			Button table_button1 = FindViewById<Button> (Resource.Id.row_button1);
			tablerow1.Visibility = ViewStates.Visible;
			table_text1.Text = "test";	
		}

		protected override void OnPause ()
		{
			InactivityHandler.Start (lm);
			base.OnPause ();
		}

		protected override void OnResume ()
		{
			InactivityHandler.Stop ();
			base.OnResume ();
		}
	}
}


