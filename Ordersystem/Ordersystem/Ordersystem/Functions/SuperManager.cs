using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordersystem.Functions
{
    public class SuperManager
    {
        private LocalManager _localManager;
        private MCSManager _mcsManager;

        /*public bool LogIn(string ssn)
        {
            if (!_localManager.ValidSocialSecurityNumber(ssn))
            {
                return false;
            }
            try
            {
                _mcsManager.RequestUserData();
            }
            catch(Exception)
            {
                //Undersøg hvilke exceptions RequestUserData() kan kaste
            }
            
        }*/
    }
}
