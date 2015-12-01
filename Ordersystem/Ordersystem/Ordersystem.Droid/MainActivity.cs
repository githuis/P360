﻿using System;
using Ordersystem.Functions;
using System.Collections.Generic;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Graphics;

namespace Ordersystem.Droid
{
	[Activity (Label = "Ordersystem", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		//Made a communicationManager object
		CommunicationManager cm;
		LayoutHandler lh;
		List<TableRow> rows;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);


			// Set our view to the Log_In
			SetContentView (Resource.Layout.Log_In);

			//Finding the different widgets ID, so i can use them
			Button button = FindViewById<Button> (Resource.Id.myButton);
			EditText editText = FindViewById<EditText> (Resource.Id.editText1);
			TextView errorMsg = FindViewById<TextView> (Resource.Id.errorMsg); 

			//Initialize managers
			cm = new CommunicationManager();
			lh = new LayoutHandler();

			//
			rows = new List<TableRow>();

			LogIn(button,editText,errorMsg);
		}

		public void LogIn(Button button,EditText editText, TextView errorMsg)
		{
			button.Click += delegate 
			{
				if(editText.Text == "") // START DEBUG ONLY
				{
					SetContentView (Resource.Layout.Main_Window);
					CreateMainWindow ();
				} // END DEBUG ONLY
				else if(cm.ValidSocialSecurityNumber(editText.Text))
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


		 //A try at making the row system, where all the rows already are made, and you need to set them to visable or invisable. And change the buttons and text to them hard code style
		 //Its shit.
		public void CreateMainWindow()
		{
			//TableLayout Table1 = FindViewById<TableLayout> (Resource.Id.table);

			AddRowsToList ((TableLayout)FindViewById (Resource.Id.tableLayout1));
			AddListnersToList(rows);

			TextView table_text1 = FindViewById<TextView> (Resource.Id.row_name1);
			Button table_button1 = FindViewById<Button> (Resource.Id.row_button1);

		}

		private void AddRowsToList(TableLayout table)
		{
			for (int i = 0; i < table.ChildCount; i++) 
			{
				View view = table.GetChildAt(i);
				if (view is TableRow) 
				{
					TableRow row = (TableRow)view;
					row.SetBackgroundColor(Color.ParseColor("#F9F9F9"));
					rows.Add (row);
				}
			}
		}

		private void AddListnersToList(List<TableRow> rows)
		{
			foreach (var row in rows) 
			{
				row.Click += (object sender, EventArgs e) => {
					lh.ResizeTableRow(rows, (TableRow) sender);
				};
			}
		}
	}
}


