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
using System.Net;
using Android.Graphics.Drawables;

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
        private int dishNameMaxLength = 12; //To not disturb row titles

        //'Globals' for inside LayoutHandler
        private Activity activity;
        private DayMenu testMenu;
        private int infoRowId;
        private Point displaySize;
        private DateTime testDate;
        private Customer customer;
        private Orderlist orderlist;
        private List<Drawable> dishImages;

        //10px per container per side. 50px for the arrow.
        private int paddingTotal = 8 * 10;
        private int arrowSize = 50;


        //The Colors to be used throughout the whole UI.
        public Color RowBackgroundColor = Color.ParseColor("#F9F9F9");
        public Color RowCompletedColor = Color.ParseColor("#75EA94");
        public Color RowErrorColor = Color.ParseColor("#E03A3A");
        public Color HeaderColor = Color.ParseColor("#417378");
        public Color TutorialColor = Color.ParseColor("#F2F3EB");
        public Color TutorialText = Color.ParseColor("#212121");

        public LayoutHandler(Activity activity)
        {
            this.activity = activity;

            testMenu = new DayMenu(
                new Dish("Kartofler m. Sovs", "Kartofler med brun sovs og millionbøf", ""),
                new Dish("Rød grød med fløde", "Rød grød med fløde til.", ""),
                new Dish("Jordbær grød m. mælk", "Jordbærgrød", ""),
                DateTime.Today);
            testDate = DateTime.Today;

            //Initialize dishImages with transparent ColorDrawables
            List<Drawable> temp = new List<Drawable>();
            Drawable drawable = new ColorDrawable(Color.Transparent);
            for (int i = 0; i < 31; i++)
            {
                temp.Add(drawable);
            }
            dishImages = temp;

            infoRowId = int.MaxValue;
        }

        public void SetCustomerAndList(Customer customer, Orderlist orderlist)
        {
            this.customer = customer;
            this.orderlist = orderlist;

            //Start downloading images asyncrone
            int count = 1;
            foreach (DayMenu dayMenu in orderlist.DayMenus)
            {
                if (dayMenu.Dish1.ImageSource != "")
                    downloadDrawableFromUrlAsync(dayMenu.Dish1.ImageSource, "dish" + count, count);
                if (dayMenu.Dish2.ImageSource != "")
                    downloadDrawableFromUrlAsync(dayMenu.Dish2.ImageSource, "dish" + (count + 1), count + 1);
                if (dayMenu.SideDish.ImageSource != "")
                    downloadDrawableFromUrlAsync(dayMenu.SideDish.ImageSource, "dish" + (count + 2), count + 2);
                count += 3;
            }
        }

        public void CreateDayMenuDisplay(DayMenu dayMenu, TableRow row, TableLayout parent)
        {
            int childId = parent.IndexOfChild(row);

            TableRow newRow = new TableRow(activity);
            newRow.AddView(new TextView(activity) { Text = "" }, 0);

            newRow.AddView(LinearBuilder(parent, row, dayMenu.Dish1, DayMenuChoice.Dish1), 1);
            newRow.AddView(LinearBuilder(parent, row, dayMenu.Dish2, DayMenuChoice.Dish2), 2);
            newRow.AddView(LinearBuilder(parent, row, dayMenu.SideDish, true), 3);
            newRow.AddView(LinearNoFood(parent, row), 4);

            newRow.SetBackgroundColor(RowBackgroundColor);
            newRow.SetMinimumHeight(maxRowHeight);
            parent.AddView(newRow, childId + 1);
            infoRowId = (childId + 1);
        }

        public void ResizeTableRow(List<TableRow> rows, TableRow rowToChange, TableLayout parent)
        {
            bool open;
            open = IsOpen(rowToChange);
            Console.WriteLine(open.ToString());
            try
            {
                parent.RemoveViewAt(infoRowId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            foreach (TableRow row in rows)
            {
                CloseRow(parent, row);
            }
            if (open)
                CloseRow(parent, rowToChange);
            else
                OpenRow(parent, rowToChange);

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

        public void SetRowErrorColor(TableRow row)
        {
            row.SetBackgroundColor(RowErrorColor);
        }

        public void CloseRow(TableLayout table, TableRow row)
        {
            row.SetMinimumHeight(minRowHeight);

            Console.WriteLine(table.IndexOfChild(row));

            //Sub-menu's first child (0) is a linearlayout, others are TextView. If is submenu, remove row.
            var child = row.GetChildAt(0);
            if (child is LinearLayout)
                table.RemoveViewAt(table.IndexOfChild(row) + 1);
            else
                ChangeArrowTo(row, Resource.Drawable.Forward);
            infoRowId = int.MaxValue;

        }

        public void ForceCloseInfoRow(TableLayout layout)
        {
            if (infoRowId != int.MaxValue)
                layout.RemoveViewAt(infoRowId);
        }

        private void OpenRow(TableLayout table, TableRow row)
        {
            row.SetMinimumHeight(mediumRowHeight);
            ChangeArrowTo(row, Resource.Drawable.ExpandArrow);
            CreateDayMenuDisplay(customer.Order.DayMenuSelections[table.IndexOfChild(row) / 2].DayMenu, row, table);
            UpdateDayMenuColors(table, row);
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
                var child = row.GetChildAt(i);
                if (child is ImageView) {
                    ImageView iv = (ImageView)child;
                    iv.SetImageResource(arrow);
                    break;
                }
            }
        }

        public bool IsOpen(TableRow row)
        {
            return row.Height == mediumRowHeight;
        }

        async void downloadDrawableFromUrlAsync(string url, string fileName, int dishIndex)
        {
            var webClient = new WebClient();
            byte[] imageBytes = null;

            try
            {
                imageBytes = await webClient.DownloadDataTaskAsync(url);
            }
            catch (Exception)
            {

            }

            string localPath = "android.resource://com.P360.Ordersystem/drawable/" + fileName;
            dishImages[dishIndex - 1] = new BitmapDrawable(BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length));


            BitmapFactory.Options options = new BitmapFactory.Options();
            options.InJustDecodeBounds = true;
            await BitmapFactory.DecodeFileAsync(localPath, options);

        }

        private LinearLayout LinearBuilder(TableLayout table, TableRow row, Dish dish, DayMenuChoice choice)
        {
            LinearLayout linearLayout = new LinearLayout(activity);
            linearLayout.Orientation = Orientation.Vertical;
            linearLayout.SetMinimumWidth((displaySize.X / 4) - (paddingTotal / 2));
            linearLayout.SetPadding(10, 10, 10, 10);
            linearLayout.SetBackgroundColor(RowBackgroundColor);

            TextView titleView = new TextView(activity);
            ImageView imageView = new ImageView(activity);
            TextView descriptionView = new TextView(activity);

            titleView.Text = dish.Name;
            titleView.TextSize = textSizeLarge;

            imageView.SetAdjustViewBounds(true);
            imageView.SetMaxHeight(maxRowHeight);
            imageView.SetMaxWidth(linearLayout.MinimumWidth);
            int dayMenuIndex = orderlist.DayMenus.FindIndex(x => x.Dish1 == dish || x.Dish2 == dish || x.SideDish == dish);
            int imageIndex = 0;

            if (orderlist.DayMenus[dayMenuIndex].Dish1 == dish)
                imageIndex = dayMenuIndex * 3;
            if (orderlist.DayMenus[dayMenuIndex].Dish2 == dish)
                imageIndex = (dayMenuIndex * 3) + 1;
            if (orderlist.DayMenus[dayMenuIndex].SideDish == dish)
                imageIndex = (dayMenuIndex * 3) + 2;

            imageView.SetImageDrawable(dishImages[imageIndex]);

			descriptionView.Text = dish.Description;
			descriptionView.TextSize = textSizeMed;

			linearLayout.AddView (titleView);
			linearLayout.AddView (imageView);
			linearLayout.AddView (descriptionView);

			int index = table.IndexOfChild(row);
			index /= 2;
			if(customer.Order.DayMenuSelections[index].Choice == DayMenuChoice.NoChoice)
				ClearRowText (row, customer.Order.DayMenuSelections [index].Date);


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
				customer.Order.DayMenuSelections[index].Choice = choice;
				UpdateRowText(row, customer.Order.DayMenuSelections[index].Date, dish);
				UpdateDayMenuColors(table, row);
			};

			return linearLayout;
		}

		//overload for sidedishes
		private LinearLayout LinearBuilder (TableLayout table, TableRow row, Dish dish, bool sideDish)
		{
			LinearLayout linearLayout = new LinearLayout (activity);
			linearLayout.Orientation = Orientation.Vertical;
			linearLayout.SetMinimumWidth ( (displaySize.X / 4) - (paddingTotal / 2));
			linearLayout.SetPadding (10, 10, 5, 10);
			linearLayout.SetBackgroundColor (RowBackgroundColor);

			TextView titleView = new TextView (activity);
			ImageView imageView = new ImageView (activity);
			TextView descriptionView = new TextView (activity);

			titleView.Text = dish.Name;
			titleView.TextSize = textSizeLarge;

            imageView.SetAdjustViewBounds(true);
            imageView.SetMaxHeight(maxRowHeight);
            imageView.SetMaxWidth(linearLayout.MinimumWidth);
            int dayMenuIndex = orderlist.DayMenus.FindIndex(x => x.Dish1 == dish || x.Dish2 == dish || x.SideDish == dish);
            int imageIndex = 0;

            if (orderlist.DayMenus[dayMenuIndex].Dish1 == dish)
                imageIndex = dayMenuIndex * 3;
            if (orderlist.DayMenus[dayMenuIndex].Dish2 == dish)
                imageIndex = (dayMenuIndex * 3) + 1;
            if (orderlist.DayMenus[dayMenuIndex].SideDish == dish)
                imageIndex = (dayMenuIndex * 3) + 2;

            imageView.SetImageDrawable(dishImages[imageIndex]);

            descriptionView.Text = dish.Description;
			descriptionView.TextSize = textSizeMed;

			linearLayout.AddView (titleView);
			linearLayout.AddView (imageView);
			linearLayout.AddView (descriptionView);

			linearLayout.Click += (object sender, EventArgs e) => {
				int index = table.IndexOfChild(row);
				index /= 2;
				customer.Order.DayMenuSelections[index].SideDish = !customer.Order.DayMenuSelections[index].SideDish;

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
				customer.Order.DayMenuSelections[index].Choice = DayMenuChoice.NoDish;
				UpdateDayMenuColors(table, row);
				ClearRowText(row, orderlist.DayMenus[index].Date);
			};

			return linearLayout;
		}

		private void UpdateRowText(TableRow row, DateTime date, Dish dish)
		{
			string s = DayOfWeekToDanish (date.DayOfWeek) + " d.";
			s += date.ToShortDateString ();

			if(dish != null)
			{
				s += ": ";

				if (dish.Name.Length > dishNameMaxLength)
				{
					s += dish.Name.Substring (0, dishNameMaxLength);
					s += "...";
				} else
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
			if (customer.Order.DayMenuSelections[index].SideDish)
				ColorDish ((LinearLayout)row.GetChildAt (3));
			//If dish 1 is selected for row, color it
			if (customer.Order.DayMenuSelections[index].Choice == DayMenuChoice.Dish1)
			{
				ColorDish ((LinearLayout)row.GetChildAt (1));
				ColorRow (oldrow);
			}
			//otherwise, if dish 2 is selected for row, color it.
			else if (customer.Order.DayMenuSelections[index].Choice == DayMenuChoice.Dish2)
			{
				ColorDish ((LinearLayout)row.GetChildAt (2));
				ColorRow (oldrow);
			}
			else if (customer.Order.DayMenuSelections[index].Choice == DayMenuChoice.NoDish)
			{
				CleanRow (oldrow);
				RemoveAllColors (row);
				ColorRow (oldrow);
				ColorDish (lin4);
			}
			else if (customer.Order.DayMenuSelections[index].Choice == DayMenuChoice.NoChoice)
			{
				CleanRow (oldrow);
				RemoveAllColors (row);
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
			lin.SetBackgroundColor(RowCompletedColor);
		}

		private bool CompareDish(Dish dish, LinearLayout lin)
		{
			return !(dish == null) && dish.Name == ((TextView)lin.GetChildAt (0)).Text;
		}
	}
}
