using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Threading.Tasks;
using SQLite;
using Ordersystem.Model;
using Ordersystem.Utilities;

namespace Ordersystem.Functions
{
    public class CommunicationManager
    {
        /*public CommunicationManager()
        {
            _localDatabase = new LocalDatabase("LocalDatabase");
        }*/

        private Customer _customer;
        private Orderlist _orderlist;
        private LocalDatabase _localDatabase;

        /// <summary>
        /// Checks whether the social security is valid
        /// </summary>
        /// <param name="socialSecurityNumber"></param>
        public bool ValidSocialSecurityNumber(string socialSecurityNumber)
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

        public Customer RequestUserData()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the session matching the Customer from the database and resumes it.
        /// If no such session is found, creates a new session, and fetches required data from Master Cater System.
        /// </summary>
        /*public void GetSession()
        {
            Order order = _localDatabase.GetOrder(x => x.PersonNumber == _customer.PersonNumber);
            if (order != null)
            {
                ResumeSession(order);
            }
            else
            {
                NewSession();
            }
        }

        private void NewSession()
        {
            _customer.Order = new Order();
            _orderlist = RequestOrderlist();
        }

        private void ResumeSession(Order order)
        {
            _customer.Order = order;
            _orderlist = _localDatabase.GetOrderlist(x => x.PersonNumber == _customer.PersonNumber);
        }*/

        private Orderlist RequestOrderlist()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Stores the current session in the database.
        /// </summary>
        /*public void StoreSession()
        {
            _localDatabase.SaveOrder(_orderlist, _customer);
        }*/

        public void CloseSession()
        {
            throw new NotImplementedException();
        }

        public bool IsOrderValid()
        {
            throw new NotImplementedException();
        }

        public void SendOrder()
        {
            throw new NotImplementedException();
        }

    }
}
