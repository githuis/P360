using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Ordersystem.Functions;

namespace Ordersystem.Droid
{
	[Activity (Label = "Ordersystem.Droid", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		CommunicationManager cm = new CommunicationManager();

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Log_In);

			// Get our widgets from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button> (Resource.Id.myButton);
			EditText editText = FindViewById<EditText> (Resource.Id.editText1);
			TableRow tablerow1 = FindViewById<TableRow> (Resource.Id.row1);
			TextView table_text2 = FindViewById<TextView> (Resource.Id.row_name2);

			LogIn(button,editText);
			tablerow1.Visibility = ViewStates.Invisible;
			table_text2.Text = "test";

		}

		public void LogIn(Button button,EditText editText)
		{
			button.Click += delegate 
			{
				if(cm.ValidSocialSecurityNumber(editText.Text))
				{
					SetContentView (Resource.Layout.Main_Window);
				}
			};
		}
	}
}


