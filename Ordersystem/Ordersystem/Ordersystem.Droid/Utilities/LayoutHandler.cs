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
		private int minRowHeight = 30;
		private int maxRowHeight = 180;

		public LayoutHandler ()
		{


		}
			
		public void ResizeTableRow(List<TableRow> rows, TableRow rowToChange)
		{
			foreach (TableRow row in rows) {
				CloseRow (row);
			}

			if (isOpen (rowToChange))
				CloseRow (rowToChange);
			else
				OpenRow (rowToChange);
		}

		private void OpenRow(TableRow row)
		{
			row.SetMinimumHeight (maxRowHeight);

			//Gets the image-view component of the row, and sets the arrow the the expand arrow
			ImageView iv = (ImageView) row.GetChildAt(1);
			iv.SetImageResource (Resource.Drawable.ExpandArrow);
		}

		private void CloseRow(TableRow row)
		{
			row.SetMinimumHeight (minRowHeight);

			//Gets the image-view component of the row, and sets the arrow to the forward arrow
			ImageView iv = (ImageView) row.GetChildAt(1);
			iv.SetImageResource (Resource.Drawable.Forward);
		}

		private bool isOpen(TableRow row)
		{
			return row.Height == maxRowHeight;
		}
	}
}

