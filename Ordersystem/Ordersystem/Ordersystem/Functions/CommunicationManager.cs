using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ordersystem.Model;

namespace Ordersystem.Functions
{
    class CommunicationManager
    {
        /// <summary>
        /// Checks whether the social security is valid
        /// </summary>
        /// <param name="socialSecurityNumber"></param>
        public bool ValidSocialSecurityNumber(int socialSecurityNumber)
        {
            bool length, day, month;
            char[] numChar = new char[10];

            length = socialSecurityNumber.ToString().Length == 10; //Check if it is exactly 10 numbers long

            //If the number is not exactly 10 long, return false already, to prevent further errors.
            if (!length)
                return false;

            numChar = socialSecurityNumber.ToString().ToCharArray();

            day = IsBetween(socialSecurityNumber.ToString().Substring(0, 2), 1, 31); //Check the first two numbers to be a valid day. The day must be 1 - 31.
            month = IsBetween(socialSecurityNumber.ToString().Substring(1, 2), 1, 12); //Check the 2nd and 3rd numbers to be a valid month. The month should be 1 - 12.

            return length && day && month;
        }

        private bool IsBetween(string num, int min, int max)
        {
            int number;
            if (int.TryParse(num, out number))
                return (number >= min && number <= max);
            else
                return false;
        }

        public Customer RequestUserData()
        {
            throw new NotImplementedException();
        }

        public Order CheckForSession()
        {
            throw new NotImplementedException();
        }

        public Orderlist RequestOrderlist()
        {
            throw new NotImplementedException();
        }

        public void StoreSession()
        {
            throw new NotImplementedException();
        }

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
