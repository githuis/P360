using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Threading.Tasks;
using Ordersystem.Enums;
using SQLite;
using Ordersystem.Model;
using Ordersystem.Utilities;

namespace Ordersystem.Functions
{
    public class LocalManager
    {
        /*public CommunicationManager()
        {
            _localDatabase = new LocalDatabase("LocalDatabase");
        }*/

        private Customer _customer;
        private Orderlist _orderlist;
        private LocalDatabase _localDatabase;
		private IMCSManager _mcsManager;

		public LocalManager()
		{
			_mcsManager = Xamarin.Forms.DependencyService.Get<IMCSManager> ();
			_localDatabase = new LocalDatabase ("LocalDatabase");
		}


        /// <summary>
        /// Checks whether the social security is valid
        /// </summary>
        /// <param name="socialSecurityNumber"></param>
        public bool IsValidSocialSecurityNumber(string socialSecurityNumber)
        {
            bool length, day, month, isNumbers;

            isNumbers = IsStringDigitsOnly(socialSecurityNumber);

            length = socialSecurityNumber.Length == 10; //Check if it is exactly 10 numbers long

            //If the number is not exactly 10 long, return false already, to prevent further errors.
            if (!length)
                return false;

            day = IsNumberBetween(socialSecurityNumber.Substring(0, 2), 01, 31); //Check the first two numbers to be a valid day. The day must be 1 - 31.
            month = IsNumberBetween(socialSecurityNumber.Substring(2, 2), 01, 12); //Check the 2nd and 3rd numbers to be a valid month. The month should be 1 - 12.

            return day && month && isNumbers;
        }

		public void SendOrder ()
		{
			_mcsManager.SendOrder (_customer.Order, _customer.PersonNumber);
		}

		public void LogIn (string personNumber)
		{
			GetCustomerFromDB (personNumber);

			_localDatabase.CleanOldSessions ();
			_customer.Order = _localDatabase.GetOrder (_customer.PersonNumber);

			if (_customer.Order == null)
			{
				NewSession ();
			}
			else
			{
				ResumeSession ();
			}
		}

		public void LogOut()
		{
			if (!_customer.Order.Sent) StoreSession ();
			_localDatabase.Close ();

			_customer = null;
			_orderlist = null;
		}

		public void GetCustomerFromDB (string personNumber)
		{
			_customer = _mcsManager.GetCustomerByPersonNumber (personNumber);
		}

		public Customer Customer {get{return _customer;}}
		public Orderlist Orderlist {get{return _orderlist;}}

        private bool IsNumberBetween(string num, int min, int max)
        {
            int number;
            if (int.TryParse(num, out number))
            {
                return (number >= min && number <= max);
            }
            return false;
        }

        private bool IsStringDigitsOnly(string str)
        {
            return str.ToCharArray().All(c => c >= '0' && c <= '9');
        }

        private void NewSession()
        {
            _customer.Order = new Order();
			_orderlist = _mcsManager.GetOrderlistByDiet (_customer.Diet);

			foreach (DayMenu menu in _orderlist.DayMenus)
			{
				_customer.Order.AddDayMenuSelection (menu);
			}
        }

        private void ResumeSession()
        {
			_orderlist = _localDatabase.GetOrderlist (_customer.PersonNumber);
			_orderlist = _mcsManager.GetOrderlistByDiet (_orderlist.Diet, _orderlist.EndDate);

			foreach (DayMenu menu in _orderlist.DayMenus)
			{
				_customer.Order.DayMenuSelections.First (s => s.Date == menu.Date).DayMenu = menu;
			}
        }

        /// <summary>
        /// Stores the current session in the database.
        /// </summary>
        public void StoreSession()
        {
            _localDatabase.SaveSession(_orderlist, _customer);
        }

        public bool IsOrderValid()
        {
            return _customer.Order.DayMenuSelections.All(selection => selection.Choice != DayMenuChoice.NoChoice);
        }

		public void FillInvalidOrder()
		{
			foreach (DayMenuSelection selection in _customer.Order.DayMenuSelections.Where (selection => selection.Choice == DayMenuChoice.NoChoice))
			{
				selection.Choice = DayMenuChoice.Dish1;
			}
		}
    }
}
