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
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btn_Log_In { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField TxtBox_CPRTextField { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		Log_In win_Log_In { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (btn_Log_In != null) {
				btn_Log_In.Dispose ();
				btn_Log_In = null;
			}
			if (TxtBox_CPRTextField != null) {
				TxtBox_CPRTextField.Dispose ();
				TxtBox_CPRTextField = null;
			}
			if (win_Log_In != null) {
				win_Log_In.Dispose ();
				win_Log_In = null;
			}
		}
	}
}
