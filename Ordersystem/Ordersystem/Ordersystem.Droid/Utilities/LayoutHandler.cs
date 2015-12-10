using System;
using System.Collections.Generic;
using System.Net;

using Ordersystem.Model;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Graphics;
using System.IO;

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
		public Color RowCompletedColor = Color.ParseColor("#75EA94");
		public Color HeaderColor = Color.ParseColor("#00FFFF"); // Skal vælges rigtigt
		public Color TutorialColor = Color.ParseColor("#F2F3EB");
		public Color TutorialText = Color.ParseColor("#212121");

		public LayoutHandler (Activity activity)
		{
			this.activity = activity;

            Dish dish1 = new Dish("Blomkål", "Smager godt", "http://www.madbanditten.dk/wp-content/uploads/2011/06/billede-3421.jpg");
            Dish dish2 = new Dish("Thor", "Jensen", "https://kinaliv.files.wordpress.com/2013/04/dsc03650.jpg");
            Dish dish3 = new Dish("Random navn", "Random desc", "http://www.maduniverset.dk/images/spinatpie.JPG");
            dish1.Number = 1;
			dish2.Number = 2;
			dish3.Number = 3;
            testMenu = new DayMenu(dish1, dish2, dish3);

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

		/*async void downloadAsync(string url, int dishIndex)
		{
			var webClient = new WebClient();
			byte[] imageBytes = null;

			try{
				imageBytes = await webClient.DownloadDataTaskAsync(url);
			}
			catch(Exception){

			}

			//Saving bitmap locally
			string documentsPath = System.Environment.GetFolderPath (System.Environment.SpecialFolder.Personal);	
			string localFilename = "dish" + dishIndex + ".jpg";
			string localPath = System.IO.Path.Combine (documentsPath, localFilename);

			//Save the Image using writeAsync
			FileStream fs = new FileStream (localPath, FileMode.OpenOrCreate);
			await fs.WriteAsync (imageBytes, 0, imageBytes.Length);
			Console.WriteLine("Saving image in local path: "+localPath);

			//Close file connection
			fs.Close ();

			BitmapFactory.Options options = new BitmapFactory.Options ();
			options.InJustDecodeBounds = true;
			await BitmapFactory.DecodeFileAsync (localPath, options);

		}*/

		private LinearLayout LinearBuilder (TableLayout table, TableRow row, Dish dish,bool isSideDish)
		{
			LinearLayout linearLayout = new LinearLayout (activity);
			linearLayout.Orientation = Orientation.Vertical;
			linearLayout.SetMinimumWidth ( (displaySize.X / 4) - (paddingTotal / 2));
			linearLayout.SetPadding (10, 10, 10, 10);
			linearLayout.SetBackgroundColor (RowBackgroundColor);

			TextView titleView = new TextView (activity);
			ImageView imageView = new ImageView (activity);
			TextView descriptionView = new TextView (activity);
            Android.Net.Uri path = Android.Net.Uri.Parse("android.resource://com.P360.Ordersystem/drawable/dish" + dish.Number.ToString());
            //Android.Net.Uri path = Android.Net.Uri.Parse("http://www.tastyburger.com/wp-content/themes/tastyBurger/images/home/img-large-burger.jpg");

            titleView.Text = dish.Name;

            titleView.TextSize = textSizeLarge;

            imageView.SetImageURI(path);
            imageView.SetScaleType(ImageView.ScaleType.CenterInside);


            descriptionView.Text = dish.Description;
			descriptionView.TextSize = textSizeMed;

			linearLayout.AddView (titleView);
			linearLayout.AddView (imageView);
			linearLayout.AddView (descriptionView);
            linearLayout.SetMinimumWidth(80);
            linearLayout.SetMinimumHeight(60);


			if (!isSideDish) {
				linearLayout.Click += (object sender, EventArgs e) => {
					int index = table.IndexOfChild(row);
					index /= 2;
					Dish.SelectedDishes [index] = dish;
					SelectDish (((TableRow)table.GetChildAt (table.IndexOfChild (row) + 1)), table, (LinearLayout)sender);
				};
			} else 
			{
				linearLayout.Click += (object sender, EventArgs e) => {
					SelectSideDish(((TableRow)table.GetChildAt (table.IndexOfChild (row) + 1)), table, (LinearLayout)sender);
				};
			}



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
				SelectNoDish( ((TableRow)table.GetChildAt(table.IndexOfChild(row) + 1)) ,table,(LinearLayout)sender);
			};

			return linearLayout;
		}

		public void SelectDish(TableRow row, TableLayout table, LinearLayout layout)
		{
			row.GetChildAt (1).SetBackgroundColor (RowBackgroundColor);
			row.GetChildAt (2).SetBackgroundColor (RowBackgroundColor);
			row.GetChildAt (4).SetBackgroundColor (RowBackgroundColor);

			layout.SetBackgroundColor (RowCompletedColor);
		}

		public void SelectSideDish(TableRow row, TableLayout table, LinearLayout layout)
		{
			var background = layout.Background;

			row.GetChildAt (4).SetBackgroundColor (RowBackgroundColor);
			if (background is Android.Graphics.Drawables.ColorDrawable)
			{
				if ((background as Android.Graphics.Drawables.ColorDrawable).Color == RowBackgroundColor) {
					layout.SetBackgroundColor (RowCompletedColor);
				} else 
				{
					layout.SetBackgroundColor (RowBackgroundColor);
				}
			}
		}

		public void SelectNoDish(TableRow row, TableLayout table, LinearLayout layout)
		{
			row.GetChildAt (1).SetBackgroundColor (RowBackgroundColor);
			row.GetChildAt (2).SetBackgroundColor (RowBackgroundColor);
			row.GetChildAt (3).SetBackgroundColor (RowBackgroundColor);

			layout.SetBackgroundColor (RowCompletedColor);
		}

	}
}
