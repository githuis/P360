// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace Ordersystem.iOS
{
	[Register ("MainWindow")]
	partial class MainWindow
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel txt_WholeName { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (txt_WholeName != null) {
				txt_WholeName.Dispose ();
				txt_WholeName = null;
			}
		}
	}
}
