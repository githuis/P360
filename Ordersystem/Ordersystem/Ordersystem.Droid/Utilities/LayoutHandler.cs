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
		}

		private void CloseRow(TableRow row)
		{
			row.SetMinimumHeight (minRowHeight);
		}

		private bool isOpen(TableRow row)
		{
			return row.Height == maxRowHeight;
		}
	}
}

