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
	[Register ("Main_")]
	partial class Main_
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		Main_window Main_window { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel txt_WholeName { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (Main_window != null) {
				Main_window.Dispose ();
				Main_window = null;
			}
			if (txt_WholeName != null) {
				txt_WholeName.Dispose ();
				txt_WholeName = null;
			}
		}
	}
}
