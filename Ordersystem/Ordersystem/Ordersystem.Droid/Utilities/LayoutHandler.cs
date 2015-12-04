﻿using System;
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
	public class LayoutHandler
	{
		//Numbers describing the looks of the main scren interface
		private int minRowHeight = 100;
		private int maxRowHeight = 200;
		private int textSizeLarge = 20;
		private int textSizeMed = 15;

		//'Globals' for inside LayoutHandler
		private Activity activity;
		private DayMenu testMenu;
		private int infoRowId;
		private Point displaySize;

		//10px per container per side. 50px for the arrow.
		private int paddingTotal = 8 * 10;
		private int arrowSize = 50;


		//The Colors to be used throughout the whole UI.
		public Color RowBackgroundColor = Color.ParseColor("#F9F9F9");
		public Color RowCompletedColor = Color.ParseColor("#00FF00"); // Skal vælges rigtigt
		public Color HeaderColor = Color.ParseColor("#00FFFF"); // Skal vælges rigtigt

		public LayoutHandler (Activity activity)
		{
			this.activity = activity;
			testMenu = new DayMenu (
				new Dish ("Kartofler m. Sovs", "Kartofler med brun sovs og millionbøf"),
				new Dish ("Rød grød med fløde", "Rød grød med fløde til."),
				new Dish ("Jordbær grød m. mælk", "Jordbærgrød"));

			infoRowId = int.MaxValue;

		}

		public void CreateDayMenuDisplay(DayMenu dayMenu, TableRow row, TableLayout parent)
		{
			int childId = parent.IndexOfChild (row);

			TableRow newRow = new TableRow (activity);
			newRow.AddView (new TextView (activity) { Text = "" }, 0);

			newRow.AddView (LinearBuilder (parent, row, dayMenu.Dish1.Name, "https://upload.wikimedia.org/wikipedia/commons/d/d9/Test.png", dayMenu.Dish1.Description), 1);
			newRow.AddView (LinearBuilder (parent, row, dayMenu.Dish2.Name, "https://upload.wikimedia.org/wikipedia/commons/d/d9/Test.png", dayMenu.Dish2.Description), 2);
			newRow.AddView (LinearBuilder (parent, row, dayMenu.SideDish.Name, "https://upload.wikimedia.org/wikipedia/commons/d/d9/Test.png", dayMenu.SideDish.Description), 3);
			newRow.AddView(LinearNoFood(), 4);


			newRow.SetBackgroundColor(RowBackgroundColor);
			newRow.SetMinimumHeight (maxRowHeight);
			parent.AddView(newRow, childId+1);
			infoRowId = (childId + 1);
		}

		public void ResizeTableRow(List<TableRow> rows, TableRow rowToChange, TableLayout parent)
		{
			bool open;
			open = IsOpen (rowToChange);
			Console.WriteLine (open.ToString ());
			try 
			{
				parent.RemoveViewAt (infoRowId);	
			} 
			catch (Exception e) 
			{

			}
			foreach (TableRow row in rows)
			{
				CloseRow (parent, row);
			}
			if (open)
				CloseRow (parent, rowToChange);
			else
				OpenRow (rowToChange, parent);

		}

		public int GetMinimumHeight()
		{
			return minRowHeight;
		}

		public void SetDisplaySize(Point size)
		{
			displaySize = size;
		}

		private void OpenRow(TableRow row, TableLayout tl)
		{
			row.SetMinimumHeight (maxRowHeight);
			ChangeArrowTo(row, Resource.Drawable.ExpandArrow);
			CreateDayMenuDisplay(testMenu, row, tl);
		}

		private void CloseRow(TableLayout table, TableRow row)
		{
			row.SetMinimumHeight (minRowHeight);

			//Sub-menu's first child (0) is a linearlayout, others are TextView. If is submenu, remove row.
			var child = row.GetChildAt(0);
			if (child is LinearLayout)
				table.RemoveViewAt (table.IndexOfChild(row) +1);
			else
				ChangeArrowTo (row, Resource.Drawable.Forward);
			infoRowId = int.MaxValue;

		}

		/// <summary>
		/// Gets the image-view component of the row, and sets the arrow the specified arrow.
		/// Call with Resource.Drawable.'Specific'Arrow
		/// </summary>
		/// <param name="row">Row.</param>
		/// <param name="arrow">Arrow.</param>
		private void ChangeArrowTo(TableRow row, int arrow)
		{
			for (int i = 0; i < row.ChildCount; i++) {
				var child = row.GetChildAt (i);
				if (child is ImageView) {
					ImageView iv = (ImageView)child;
					iv.SetImageResource (arrow);
					break;
				}
			}
		}

		private bool IsOpen(TableRow row)
		{
			return row.Height == maxRowHeight;
		}


		private LinearLayout LinearBuilder(TableLayout table, TableRow row, string title, string imgURI, string description)
		{
			bool openDish = false;
			LinearLayout linearLayout = new LinearLayout (activity);
			linearLayout.Orientation = Orientation.Vertical;
			linearLayout.SetMinimumWidth ( (displaySize.X / 4) - paddingTotal / 4);
			linearLayout.SetPadding (10, 10, 10, 10);

			TextView titleView = new TextView (activity);
			ImageView imageView = new ImageView (activity);
			TextView descriptionView = new TextView (activity);

			titleView.Text = title;
			titleView.TextSize = textSizeLarge;

			imageView.SetImageURI (Android.Net.Uri.Parse(imgURI));

			descriptionView.Text = description;
			descriptionView.TextSize = textSizeMed;

			linearLayout.AddView (titleView);
			linearLayout.AddView (imageView);
			linearLayout.AddView (descriptionView);

			linearLayout.Click += (object sender, EventArgs e) => {
				linearLayout.SetBackgroundColor(RowCompletedColor);
			};

			/*
			 * Does not need a on click atm.
			 * linearLayout.Click += delegate {
				CloseRow(table, row);
			};*/

			return linearLayout;
		}

		private LinearLayout LinearNoFood()
		{
			LinearLayout linearLayout = new LinearLayout (activity);
			linearLayout.Orientation = Orientation.Vertical;
			linearLayout.SetMinimumWidth ( (displaySize.X / 4) - arrowSize - paddingTotal);
			linearLayout.SetPadding(5, 10, 5, 10);




			linearLayout.AddView(new TextView (activity)
				{
					Text = "Ingen mad denne dag",
					TextSize = textSizeLarge
				});

			ImageView retimg = new ImageView (activity);
			retimg.SetImageURI (Android.Net.Uri.Parse("https://upload.wikimedia.org/wikipedia/commons/d/d9/Test.png"));

			linearLayout.AddView (retimg);

			return linearLayout;
		}

	}
}
