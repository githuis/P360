using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Ordersystem.Functions;

namespace Ordersystem.Droid
{
    [Activity(Label = "Ordersystem.Droid", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        //Made a communicationManager object
        private readonly CommunicationManager cm = new CommunicationManager();

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view to the Log_In
            SetContentView(Resource.Layout.Log_In);

            //Finding the different widgets ID, so i can use them
            var button = FindViewById<Button>(Resource.Id.myButton);
            var editText = FindViewById<EditText>(Resource.Id.editText1);
            var errorMsg = FindViewById<TextView>(Resource.Id.errorMsg);

            LogIn(button, editText, errorMsg);
        }

        public void LogIn(Button button, EditText editText, TextView errorMsg)
        {
            button.Click += delegate
            {
                if (cm.ValidSocialSecurityNumber(editText.Text))
                    SetContentView(Resource.Layout.Main_Window);
                else
                    errorMsg.Visibility = ViewStates.Visible;
            };
        }


        //A try at making the row system, where all the rows already are made, and you need to set them to visable or invisable. And change the buttons and text to them hard code style
        //Its shit.
        public void CreateMainWindow()
        {
            //TableLayout Table1 = FindViewById<TableLayout> (Resource.Id.table);
            int i;

            var tablerow1 = FindViewById<TableRow>(Resource.Id.row1);
            var tablerow2 = FindViewById<TableRow>(Resource.Id.row2);
            var tablerow3 = FindViewById<TableRow>(Resource.Id.row3);
            var tablerow4 = FindViewById<TableRow>(Resource.Id.row4);
            var tablerow5 = FindViewById<TableRow>(Resource.Id.row5);
            var tablerow6 = FindViewById<TableRow>(Resource.Id.row6);
            var tablerow7 = FindViewById<TableRow>(Resource.Id.row7);
            var tablerow8 = FindViewById<TableRow>(Resource.Id.row8);
            var tablerow9 = FindViewById<TableRow>(Resource.Id.row9);
            var tablerow10 = FindViewById<TableRow>(Resource.Id.row10);
            var tablerow11 = FindViewById<TableRow>(Resource.Id.row11);
            var tablerow12 = FindViewById<TableRow>(Resource.Id.row12);
            var tablerow13 = FindViewById<TableRow>(Resource.Id.row13);
            var tablerow14 = FindViewById<TableRow>(Resource.Id.row14);
            var tablerow15 = FindViewById<TableRow>(Resource.Id.row15);
            var tablerow16 = FindViewById<TableRow>(Resource.Id.row16);
            var tablerow17 = FindViewById<TableRow>(Resource.Id.row17);
            var tablerow18 = FindViewById<TableRow>(Resource.Id.row18);
            var tablerow19 = FindViewById<TableRow>(Resource.Id.row19);
            var tablerow20 = FindViewById<TableRow>(Resource.Id.row20);
            var tablerow21 = FindViewById<TableRow>(Resource.Id.row21);
            var tablerow22 = FindViewById<TableRow>(Resource.Id.row22);
            var tablerow23 = FindViewById<TableRow>(Resource.Id.row23);
            var tablerow24 = FindViewById<TableRow>(Resource.Id.row24);
            var tablerow25 = FindViewById<TableRow>(Resource.Id.row25);
            var tablerow26 = FindViewById<TableRow>(Resource.Id.row26);
            var tablerow27 = FindViewById<TableRow>(Resource.Id.row27);
            var tablerow28 = FindViewById<TableRow>(Resource.Id.row28);
            var tablerow29 = FindViewById<TableRow>(Resource.Id.row29);
            var tablerow30 = FindViewById<TableRow>(Resource.Id.row30);
            var tablerow31 = FindViewById<TableRow>(Resource.Id.row31);


            var table_text1 = FindViewById<TextView>(Resource.Id.row_name1);
            var table_button1 = FindViewById<Button>(Resource.Id.row_button1);
            tablerow1.Visibility = ViewStates.Visible;
            table_text1.Text = "test";
        }
    }
}