using System;
using System.Collections.Generic;

using Ordersystem.Model;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace Ordersystem.Droid
{
	public class LayoutHandler
	{
		private int minRowHeight = 40;
		private int maxRowHeight = 80;
		private int textSizeLarge = 31;
		private int textSizeMed = 23;

		//'Globals' for inside LayoutHandler
		private Activity activity;
		private DayMenu testMenu;
		private int infoRowId;


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
			TableRow newRow = new TableRow (activity);
			newRow.AddView (new TextView (activity) { Text = "" }, 0);
			newRow.AddView (LinearBuilder (parent, row, dayMenu.Dish1.Name, "https://upload.wikimedia.org/wikipedia/commons/d/d9/Test.png", dayMenu.Dish1.Description), 1);
			newRow.AddView (LinearBuilder (parent, row, dayMenu.Dish2.Name, "https://upload.wikimedia.org/wikipedia/commons/d/d9/Test.png", dayMenu.Dish2.Description), 2);
			newRow.AddView (LinearBuilder (parent, row, dayMenu.SideDish.Name, "https://upload.wikimedia.org/wikipedia/commons/d/d9/Test.png", dayMenu.SideDish.Description), 3);
			newRow.AddView(LinearNoFood(), 4);


			newRow.SetBackgroundColor(RowBackgroundColor);
			newRow.SetMinimumHeight (minRowHeight);
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

		private void OpenRow(TableRow row, TableLayout tl)
		{
			row.SetMinimumHeight (maxRowHeight);
			ChangeArrowTo(row, Resource.Drawable.ExpandArrow);
			CreateDayMenuDisplay(testMenu, row, tl);
		}

		private void CloseRow(TableRow row)
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

			//Gets the image-view component of the row, and sets the arrow to the forward arrow
			for (int i = 0; i < row.ChildCount; i++)
			{
				var child = row.GetChildAt (i);
				if (child != null && child is ImageView) {
					ImageView iv = (ImageView)child;
					iv.SetImageResource (Resource.Drawable.Forward);
				} else if (child != null && child is GridLayout) {
					row.RemoveView (child);
					i--;
				} else if (child != null && child is LinearLayout) {
					row.RemoveView (child);
					i--;
				}
			}
		}

		private bool IsOpen(TableRow row)
		{
				return row.Height == maxRowHeight;
		}

		/*private GridView GridMaker(Activity activity)
		{
			GridView view = new GridView (activity);
			view.SetColumnWidth(200);
			view.NumColumns = 4;
			view.Id = 69;
			view.SetVerticalSpacing(10);
			view.SetHorizontalSpacing(10);
			view.SetGravity(GravityFlags.Center);
			view.StretchMode = StretchMode.StretchColumnWidth;
			view.seta
			

			return view;
		}*/

	/*	private GridLayout GridMaker(Activity activity, TableRow row)
		{			
			GridLayout gridLayout = new GridLayout(activity);

			for (int i = 0; i < row.ChildCount; i++)
			{
				var child = row.GetChildAt (i);
				if (child != null && child is GridLayout)
				{
					gridLayout = (GridLayout)child;
				}
			}

			gridLayout.RemoveAllViews();

			int total = 6;
			int column = 3;
			int rows = total / column;

			 
			gridLayout.SetBackgroundColor (Android.Graphics.Color.Red);
			gridLayout.AddView(new TextView (activity)
				{
					Text = "Ret 1",
					TextSize = 30
				});

			ImageView ret1img = new ImageView(activity);
			ret1img.SetImageResource(Resource.Drawable.Icon);
			GridLayout.LayoutParams param1 =new GridLayout.LayoutParams();
			param1.Height = WindowManagerLayoutParams.WrapContent;
			param1.Width = WindowManagerLayoutParams.WrapContent;
			param1.RightMargin = 5;
			param1.TopMargin = 5;
			param1.SetGravity (GravityFlags.Left);
			ret1img.LayoutParameters = param1;
			gridLayout.AddView(ret1img);

			gridLayout.AddView(new TextView (activity)
				{
					Text = "Ret 2",
					TextSize = 30
				});

			ImageView ret2img = new ImageView(activity);
			ret2img.SetImageResource(Resource.Drawable.Icon);
			GridLayout.LayoutParams param2 =new GridLayout.LayoutParams();
			param2.Height = WindowManagerLayoutParams.WrapContent;
			param2.Width = WindowManagerLayoutParams.WrapContent;
			param2.RightMargin = 5;
			param2.TopMargin = 5;
			param2.SetGravity (GravityFlags.Left);
			ret2img.LayoutParameters = param2;
			gridLayout.AddView(ret2img);

			gridLayout.AddView(new TextView (activity)
				{
					Text = "Biret",
					TextSize = 30
				});

			ImageView biretimg = new ImageView(activity);
			biretimg.SetImageResource(Resource.Drawable.Icon);
			GridLayout.LayoutParams parambi =new GridLayout.LayoutParams();
			parambi.Height = WindowManagerLayoutParams.WrapContent;
			parambi.Width = WindowManagerLayoutParams.WrapContent;
			parambi.RightMargin = 5;
			parambi.TopMargin = 5;
			parambi.SetGravity (GravityFlags.Left);
			biretimg.LayoutParameters = parambi;
			gridLayout.AddView(biretimg);




			return gridLayout ;
		}*/

		private LinearLayout LinearBuilder(TableRow row, string title, string imgURI, string desc)
		{
			LinearLayout linearLayout = new LinearLayout (activity);
			linearLayout.Orientation = Orientation.Vertical;

			//linearLayout.SetPadding (0, 10, 0, 10);

			linearLayout.AddView(new TextView (activity)
				{
					Text = title,
					TextSize = textSizeLarge
				});
			
			ImageView retimg = new ImageView (activity);
			//retimg.SetImageResource (Resource.Drawable.Untitled);
			retimg.SetImageURI (Android.Net.Uri.Parse (imgURI));
			linearLayout.AddView (retimg);

			linearLayout.AddView(new TextView (activity)
				{
					Text = desc,
					TextSize = textSizeMed
				});

			linearLayout.Click += delegate {
				CloseRow(row);
			};

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
					TextSize = 30
				});

			ImageView retimg = new ImageView (activity);
			retimg.SetImageResource (Resource.Drawable.Untitled);

			linearLayout.AddView (retimg);

			return linearLayout;
		}
			
	}
}

