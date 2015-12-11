using System;
using System.Collections.Generic;

using Ordersystem.Model;
using Ordersystem.Enums;

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
		private int mediumRowHeight = 150;
		private int maxRowHeight = 200;
		private int textSizeLarge = 20;
		private int textSizeMed = 15;

		//'Globals' for inside LayoutHandler
		private Activity activity;
		private DayMenu testMenu;
		private int infoRowId;
		private Point displaySize;
		private DateTime testDate;
		private Customer customer;

		//10px per container per side. 50px for the arrow.
		private int paddingTotal = 8 * 10;
		private int arrowSize = 50;


		//The Colors to be used throughout the whole UI.
		public Color RowBackgroundColor = Color.ParseColor("#F9F9F9");
		public Color RowCompletedColor = Color.ParseColor("#75EA94");
		public Color HeaderColor = Color.ParseColor("#417378"); // Skal vælges rigtigt
		public Color TutorialColor = Color.ParseColor("#F2F3EB");
		public Color TutorialText = Color.ParseColor("#212121");

		public LayoutHandler (Activity activity, Customer customer)
		{
			this.activity = activity;
			this.customer = customer;

			testMenu = new DayMenu (
				new Dish ("Kartofler m. Sovs", "Kartofler med brun sovs og millionbøf", ""),
				new Dish ("Rød grød med fløde", "Rød grød med fløde til.", ""),
				new Dish ("Jordbær grød m. mælk", "Jordbærgrød", ""),
				DateTime.Today);
			testDate = DateTime.Today;

			infoRowId = int.MaxValue;
		}

		public void CreateDayMenuDisplay(DayMenu dayMenu, TableRow row, TableLayout parent)
		{
			int childId = parent.IndexOfChild (row);

			TableRow newRow = new TableRow (activity);
			newRow.AddView (new TextView (activity) { Text = "" }, 0);

			newRow.AddView (LinearBuilder (parent, row, dayMenu.Dish1,false), 1);
			newRow.AddView (LinearBuilder (parent, row, dayMenu.Dish2,false), 2);
			newRow.AddView (LinearBuilder (parent, row, dayMenu.SideDish,true), 3);
			newRow.AddView(LinearNoFood(parent,row), 4);


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
				Console.WriteLine (e.Message);
			}

			foreach (TableRow row in rows)
			{
				CloseRow (parent, row);
			}
			if (open)
				CloseRow (parent, rowToChange);
			else
				OpenRow (parent, rowToChange);

		}

		public int GetMinimumHeight()
		{
			return minRowHeight;
		}
		public int GetMediumHeight()
		{
			return mediumRowHeight;
		}

		public void SetDisplaySize(Point size)
		{
			displaySize = size;
		}

		private void OpenRow(TableLayout table, TableRow row)
		{
			row.SetMinimumHeight (mediumRowHeight);
			ChangeArrowTo(row, Resource.Drawable.ExpandArrow);
			CreateDayMenuDisplay(testMenu, row, table);
			UpdateDayMenuColors (table, row);
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
			return row.Height == mediumRowHeight;
		}


		private LinearLayout LinearBuilder (TableLayout table, TableRow row, Dish dish, DayMenuChoice choice)
		{
			LinearLayout linearLayout = new LinearLayout (activity);
			linearLayout.Orientation = Orientation.Vertical;
			linearLayout.SetMinimumWidth ( (displaySize.X / 4) - (paddingTotal / 2));
			linearLayout.SetPadding (10, 10, 10, 10);
			linearLayout.SetBackgroundColor (RowBackgroundColor);

			TextView titleView = new TextView (activity);
			ImageView imageView = new ImageView (activity);
			TextView descriptionView = new TextView (activity);

			titleView.Text = dish.Name;
			titleView.TextSize = textSizeLarge;

			imageView.SetImageURI (Android.Net.Uri.Parse("http://www.kesanacats.dk/wp-content/uploads/killroy-lille-billed-fra-sølvkatten.png"));

			descriptionView.Text = dish.Description;
			descriptionView.TextSize = textSizeMed;

			linearLayout.AddView (titleView);
			linearLayout.AddView (imageView);
			linearLayout.AddView (descriptionView);


			/*if (!isSideDish) 
			{
				linearLayout.Click += (object sender, EventArgs e) => {
					
					Dish.SelectedDishes [index] = dish;
					UpdateRowText(row, testDate, dish); // FIX DATE!!
					customer.Order.DayMenuSelections[index].Choice = Ordersystem.Enums.DayMenuChoice.

				};
			} 
			else 
			{
				linearLayout.Click += (object sender, EventArgs e) => {
					
					int index = table.IndexOfChild(row);
					index /= 2;
					SelectSideDish(index, dish);

					//Dish.SelectedSideDishes[index] = dish;
					//Console.WriteLine("IN:" + index);
				};
			}*/

			linearLayout.Click += (object sender, EventArgs e) => {
				int index = table.IndexOfChild(row);
				index /= 2;
				customer.Order.DayMenuSelections[index].Choice = choice;

				UpdateDayMenuColors(table, row);
			};

			return linearLayout;
		}

		private LinearLayout LinearNoFood(TableLayout table, TableRow row)
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

			linearLayout.Click += (object sender, EventArgs e) => {
				int index = table.IndexOfChild(row);
				index /= 2;
				Dish.SelectedDishes[index] = null;
				Dish.SelectedSideDishes[index] = null;
				UpdateDayMenuColors(table, row);
				ClearRowText(row, testDate); // FIX DATE!
			};

			return linearLayout;
		}

		public void SelectSideDish(int index, Dish dish)
		{
			//unselect side dish
			if (Dish.SelectedSideDishes [index] != null)
				Dish.SelectedSideDishes [index] = null;
			//Select sidedish if and only if there is a main dish selected.
			else if (Dish.SelectedDishes[index] != null)
			{
				Dish.SelectedSideDishes [index] = dish;
			}
		}

		private void UpdateRowText(TableRow row, DateTime date, Dish dish)
		{
			string s = DayOfWeekToDanish (date.DayOfWeek) + " d. ";
			s = date.ToShortDateString ();

			if(dish != null)
			{
				s += " - ";
				s += dish.Name;
			}
			GetTextView (row, 0).Text = s;
		}

		public void ClearRowText(TableRow row, DateTime date)
		{
			GetTextView (row, 0).Text = DayOfWeekToDanish (date.DayOfWeek) + " d. " + date.ToShortDateString ();
		}

		private string DayOfWeekToDanish(DayOfWeek dow)
		{
			switch(dow)
			{
			case DayOfWeek.Monday:
				return "Mandag";
			case DayOfWeek.Tuesday:
				return "Tirsdag";
			case DayOfWeek.Wednesday:
				return "Onsdag";
			case DayOfWeek.Thursday:
				return "Torsdag";
			case DayOfWeek.Friday:
				return "Fredag";
			case DayOfWeek.Saturday:
				return "Lørdag";
			case DayOfWeek.Sunday:
				return "Søndag";
			default:
				return "Mandag.. eller noget";

			}
		}

		private TextView GetTextView(TableRow row, int index)
		{
			var tv = row.GetChildAt (index);
			if(tv is TextView)
			{
				TextView textView = (TextView)tv;
				return textView;
			}
			throw new InvalidCastException ("Row does not contain a TextView at " + index.ToString());
		}

		private void UpdateDayMenuColors(TableLayout table, TableRow row)
		{
			TableRow oldrow = row;
			oldrow.SetBackgroundColor (RowBackgroundColor);
			int id = table.IndexOfChild (oldrow), index;
			id++;
			id /= 2;
			index = (infoRowId - 1) / 2;

			/*if (table.ChildCount == 65)
				index-=2;
			else if (table.ChildCount == 64)
				index--;*/

			//Console.WriteLine (id);

			row = (TableRow) table.GetChildAt (infoRowId);
			var dish1 = row.GetChildAt (1);
			var dish2 = row.GetChildAt (2);
			var sidedish = row.GetChildAt (3);
			var nofood = row.GetChildAt (4);

			LinearLayout lin1 = (LinearLayout)dish1;
			LinearLayout lin2 = (LinearLayout)dish2;
			LinearLayout lin3 = (LinearLayout)sidedish;
			LinearLayout lin4 = (LinearLayout)nofood;

			//Console.WriteLine ((dish1 is LinearLayout) + " " + (dish2 is LinearLayout) + " " + (dish2 is LinearLayout));
			//Console.WriteLine("IN2:" + index + "\nID:" + id);
			//Console.WriteLine (Dish.SelectedSideDishes [index].Name);
			//Console.WriteLine ("Passed Test:" + CompareDish (Dish.SelectedSideDishes [id], (LinearLayout)sidedish));

			RemoveAllColors (row);
			//If sidedish is selected for row, color it
			if (CompareDish (Dish.SelectedSideDishes [index], lin3))
				ColorDish ((LinearLayout)row.GetChildAt (3));
			//If dish 1 is selected for row, color it
			if (CompareDish (Dish.SelectedDishes [index], lin1))
			{
				ColorDish ((LinearLayout)row.GetChildAt (1));
				ColorRow (oldrow);
			}
			//otherwise, if dish 2 is selected for row, color it.
			else if (CompareDish (Dish.SelectedDishes [index], lin2))
			{
				ColorDish ((LinearLayout)row.GetChildAt (2));
				ColorRow (oldrow);
			}
			else if (Dish.SelectedDishes [id] == null)
			{
				CleanRow (oldrow);
				RemoveAllColors (row);
				ColorDish (lin4);
			}
			
		}

		private void ColorRow(TableRow row)
		{
			row.SetBackgroundColor (RowCompletedColor);
		}

		private void CleanRow(TableRow row)
		{
			row.SetBackgroundColor (RowBackgroundColor);
		}

		private void RemoveAllColors(TableRow row)
		{
			row.GetChildAt(1).SetBackgroundColor(RowBackgroundColor);
			row.GetChildAt(2).SetBackgroundColor(RowBackgroundColor);
			row.GetChildAt(3).SetBackgroundColor(RowBackgroundColor);	
			row.GetChildAt(4).SetBackgroundColor(RowBackgroundColor);

		}

		private void ColorDish(LinearLayout lin)
		{
			Console.WriteLine ("Werks");
			lin.SetBackgroundColor(RowCompletedColor);
		}

		private bool CompareDish(Dish dish, LinearLayout lin)
		{
			return !(dish == null) && dish.Name == ((TextView)lin.GetChildAt (0)).Text;
		}

	}
}
