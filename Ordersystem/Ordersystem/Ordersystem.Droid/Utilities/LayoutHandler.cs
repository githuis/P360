using System;
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
		private int minRowHeight = 40;
		private int textSizeLarge = 31;
		private int textSizeMed = 23;

		//'Globals' for inside LayoutHandler
		private Activity activity;
		private DayMenu testMenu;


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
		}

		public void CreateDayMenuDisplay(DayMenu dayMenu, TableRow row, TableLayout parent)
		{
			int childId = parent.IndexOfChild (row);

			TableRow newRow = new TableRow (activity);
			newRow.AddView (LinearBuilder (parent, row, dayMenu.Dish1.Name, "https://upload.wikimedia.org/wikipedia/commons/d/d9/Test.png", dayMenu.Dish1.Description), 0);
			newRow.AddView (LinearBuilder (parent, row, dayMenu.Dish2.Name, "https://upload.wikimedia.org/wikipedia/commons/d/d9/Test.png", dayMenu.Dish2.Description), 1);
			newRow.AddView (LinearBuilder (parent, row, dayMenu.SideDish.Name, "https://upload.wikimedia.org/wikipedia/commons/d/d9/Test.png", dayMenu.SideDish.Description), 2);
			newRow.AddView(LinearNoFood(), 3);


			newRow.SetBackgroundColor(RowBackgroundColor);
			newRow.SetMinimumHeight (minRowHeight);
			parent.AddView(newRow, childId);
		}
			
		public void ResizeTableRow(List<TableRow> rows, TableRow rowToChange, TableLayout parent)
		{
			foreach (TableRow row in rows) {
				CloseRow (parent, row);
			}
			if (IsOpen (rowToChange))
				CloseRow (parent, rowToChange);
			else
				OpenRow (rowToChange, parent);
		}

		private void OpenRow(TableRow row, TableLayout tl)
		{
			//row.SetMinimumHeight (maxRowHeight);


			ChangeArrowTo(row, Resource.Drawable.ExpandArrow);

			CreateDayMenuDisplay(testMenu, row, tl);
		}

		private void CloseRow(TableLayout table, TableRow row)
		{
			//Sub-menu's first child (0) is a linearlayout, others are TextView. If is submenu, remove row.
			if (row.GetChildAt (0) is LinearLayout)
				table.RemoveViewAt (table.IndexOfChild (row));
			else
				ChangeArrowTo (row, Resource.Drawable.Forward);
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
			LinearLayout linearLayout = new LinearLayout (activity);
			linearLayout.Orientation = Orientation.Vertical;
			//linearLayout.SetPadding (0, 10, 0, 10);

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
			linearLayout.SetPadding(0, 10, 10, 10);
			linearLayout.LayoutParameters = new LinearLayout.LayoutParams(
				LinearLayout.LayoutParams.WrapContent,
				LinearLayout.LayoutParams.WrapContent);


			linearLayout.AddView(new TextView (activity)
				{
					Text = "Ingen mad denne dag",
					TextSize = textSizeLarge
				});

			ImageView retimg = new ImageView (activity);
			retimg.SetImageResource (Resource.Drawable.Untitled);

			linearLayout.AddView (retimg);

			return linearLayout;
		}
			
	}
}

