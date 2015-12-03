using System;
using System.Collections.Generic;

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
		private int maxRowHeight = 180;

		public LayoutHandler ()
		{


		}
			
		public void ResizeTableRow(List<TableRow> rows, TableRow rowToChange, Activity activity)
		{
			foreach (TableRow row in rows) {
				CloseRow (row);
			}
			if (isOpen (rowToChange))
				CloseRow (rowToChange);
			else
				OpenRow (rowToChange, activity);
		}

		private void OpenRow(TableRow row, Activity activity)
		{
			row.SetMinimumHeight (maxRowHeight);

			//Gets the image-view component of the row, and sets the arrow the the expand arrow
			for (int i = 0; i < row.ChildCount; i++) {
				var child = row.GetChildAt (i);
				if (child is ImageView) {
					ImageView iv = (ImageView)child;
					iv.SetImageResource (Resource.Drawable.ExpandArrow);
					break;
				}
			}

			//row.AddView (GridMaker(activity, row), 1);
			row.AddView(LinearBuilder(activity, row), 1);
			row.AddView(LinearBuilder(activity, row), 2);
			row.AddView(LinearBuilder(activity, row), 3);
			row.AddView(LinearNoFood(activity), 4);

		}

		private void CloseRow(TableRow row)
		{
			row.SetMinimumHeight (minRowHeight);

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

		private bool isOpen(TableRow row)
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

		private LinearLayout LinearBuilder(Activity activity, TableRow row)
		{
			LinearLayout linearLayout = new LinearLayout (activity);
			linearLayout.Orientation = Orientation.Vertical;
			//linearLayout.SetPadding (0, 10, 0, 10);

			linearLayout.AddView(new TextView (activity)
				{
					Text = "Biret",
					TextSize = 30
				});
			
			ImageView retimg = new ImageView (activity);
			retimg.SetImageResource (Resource.Drawable.Untitled);
			linearLayout.AddView (retimg);

			linearLayout.AddView(new TextView (activity)
				{
					Text = "Ret information",
					TextSize = 22
				});

			linearLayout.Click += delegate {
				CloseRow(row);
			};

			return linearLayout;
		}

		private LinearLayout LinearNoFood(Activity activity)
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

