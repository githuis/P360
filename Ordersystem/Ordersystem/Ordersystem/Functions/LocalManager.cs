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

		/// <summary>
		/// Initializes a new instance of the <see cref="Ordersystem.Functions.LocalManager"/> class.
		/// </summary>
		public LocalManager(string filename)
		{
			_mcsManager = Xamarin.Forms.DependencyService.Get<IMCSManager> ();
			_localDatabase = new LocalDatabase (filename);
		}

		/// <summary>
		/// Gets the MCSManager.
		/// </summary>
		/// <value>The MCSManager.</value>
		public IMCSManager MCSManager { get { return _mcsManager; } }

        /// <summary>
        /// Checks whether the social security is valid
        /// </summary>
        /// <param name="socialSecurityNumber"></param>
		/// <returns><c>true</c> if the social security number; otherwise <c>false</c></returns>
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

		/// <summary>
		/// Logs in to the system with the given personnumber.
		/// </summary>
		/// <param name="personNumber">The given personnumber.</param>
		/// <exception cref="Ordersystem.Exceptions.InvalidCustomerException">Throws this exception when customer is invalid.</exception>
		/// <exception cref="Ordersystem.Exceptions.CustomerNotFoundException">Throws this exceoption when no customer was found.</exception>
		/// <exception cref="Ordersystem.Exceptions.InvalidOrderlistException">Throws this exception when orderlist is invalid.</exception>
		/// <exception cref="Ordersystem.Exceptions.OrderlistNotFoundException">Thows this exception when no orderlist is found.</exception>
		public void LogIn (string personNumber)
		{
			_customer = _mcsManager.GetCustomerByPersonNumber (personNumber);

			_localDatabase.CleanOldSessions ();
			var order = _localDatabase.GetOrder (_customer.PersonNumber);

			if (order == null)
			{
				NewSession ();
			}
			else
			{
				ResumeSession (order);
			}
		}

		/// <summary>
		/// Logs out from the system.
		/// </summary>
		public void LogOut()
		{
			StoreSession ();
			_localDatabase.Close ();

			_customer = null;
			_orderlist = null;
		}

		/// <summary>
		/// Gets the current customer.
		/// </summary>
		/// <value>The customer.</value>
		public Customer Customer { get { return _customer; } set { _customer = value; } }

		/// <summary>
		/// Gets the current orderlist.
		/// </summary>
		/// <value>The orderlist.</value>
		public Orderlist Orderlist { get { return _orderlist; } set { _orderlist = value; } }

        /// <summary>
        /// Determines whether the number is between the specified min and max.
        /// </summary>
        /// <returns><c>true</c> if the number is between the specified min and max; otherwise, <c>false</c>.</returns>
        /// <param name="num">Number.</param>
        /// <param name="min">Minimum.</param>
        /// <param name="max">Maximum.</param>
		public bool IsNumberBetween(string num, int min, int max)
        {
            int number;
            if (int.TryParse(num, out number))
            {
                return (number >= min && number <= max);
            }
            return false;
        }

		/// <summary>
		/// Determines whether the string is only digits.
		/// </summary>
		/// <returns><c>true</c> if the string is digits only; otherwise, <c>false</c>.</returns>
		/// <param name="str">String.</param>
        public bool IsStringDigitsOnly(string str)
        {
            return str.ToCharArray().All(c => c >= '0' && c <= '9');
        }

		//Creates a new session.
        private void NewSession()
        {
            _customer.Order = new Order();
			_orderlist = _mcsManager.GetOrderlistByDiet (_customer.Diet);
			_customer.Order.SetSelectionLength (DateTime.DaysInMonth (_orderlist.DayMenus [0].Date.Year, _orderlist.DayMenus [0].Date.Month), _orderlist);

			foreach (DayMenu menu in _orderlist.DayMenus)
			{
				_customer.Order.AddDayMenuSelection (menu);
			}
        }

		//Resume a session.
		private void ResumeSession(Order order)
        {
			_orderlist = _localDatabase.GetOrderlist (_customer.PersonNumber);
			_orderlist = _mcsManager.GetOrderlistByDiet (_orderlist.Diet, _orderlist.EndDate);
			_customer.Order.SetSelectionLength (DateTime.DaysInMonth (_orderlist.DayMenus [0].Date.Year, _orderlist.DayMenus [0].Date.Month), _orderlist);

			foreach (DayMenuSelection selection in order.DayMenuSelections)
			{
				_customer.Order.ChangeDayMenuSelection (selection.Date, selection.Choice, selection.SideDish);
			}

			foreach (DayMenu menu in _orderlist.DayMenus)
			{
				_customer.Order.DayMenuSelections [menu.Date.Day - 1].DayMenu = menu;
			}

			_customer.Order.Sent = order.Sent;
        }

        /// <summary>
        /// Stores the current session in the database.
        /// </summary>
        public void StoreSession()
        {
            _localDatabase.SaveSession(_orderlist, _customer);
        }

		/// <summary>
		/// Determines whether the current order is valid.
		/// </summary>
		/// <returns><c>true</c> if the order is valid; otherwise, <c>false</c>.</returns>
        public bool IsOrderValid()
        {
            return _customer.Order.DayMenuSelections.All(selection => selection.Choice != DayMenuChoice.NoChoice);
        }

		/// <summary>
		/// Fills an invalid order with default values.
		/// </summary>
		public void FillInvalidOrder()
		{
			foreach (DayMenuSelection selection in _customer.Order.DayMenuSelections.Where (selection => selection.Choice == DayMenuChoice.NoChoice))
			{
				selection.Choice = DayMenuChoice.Dish1;
			}
		}

		//Er duplicate code, ligger i mcsmanager
		private string ParseStringFromDiet(Diet diet)
		{
			switch(diet)
			{
			case Diet.Full:
				return "v-full";
			case Diet.LowFat: 
				return"v-lowFat" ;
			case Diet.EnergyDense:
				return "v-energyDense";
			case Diet.SoftFoodsWPotatoes:
				return "v-softFoodsWPotatoes";
			case Diet.SoftFoodsWMash:
				return "v-softFoodsWMash";
			default:
				throw new ArgumentException ("Invalid diet type");
			}
		}
    }
}
