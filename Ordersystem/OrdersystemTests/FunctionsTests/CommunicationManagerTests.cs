using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ordersystem.Functions;

namespace OrdersystemTests.FunctionsTests
{
    [TestClass]
    public class CommunicationManagerTests
    {
        [TestMethod]
        public void ValidSocialSecurityNumber_ValidInput_AssertTrue()
        {
            LocalManager comMan = new LocalManager();

            Assert.IsTrue(comMan.ValidSocialSecurityNumber("0706950435"));
        }

        [TestMethod]
        public void ValidSocialSecurityNumber_BorderInput_AssertTrue()
        {
            LocalManager comMan = new LocalManager();

            Assert.IsTrue(comMan.ValidSocialSecurityNumber("3112999999"));
        }

        [TestMethod]
        public void ValidSocialSecurityNumber_DayOverBorderImput_AsserFalse()
        {
            LocalManager comMan = new LocalManager();

            Assert.IsFalse(comMan.ValidSocialSecurityNumber("3212999999"));
        }

        [TestMethod]
        public void ValidSocialSecurityNumber_MonthOverBorderImput_AsserFalse()
        {
            LocalManager comMan = new LocalManager();

            Assert.IsFalse(comMan.ValidSocialSecurityNumber("3113999999"));
        }

        [TestMethod]
        public void ValidSocialSecurityNumber_VeryInvalidInput_AsserFalse()
        {
            LocalManager comMan = new LocalManager();

            Assert.IsFalse(comMan.ValidSocialSecurityNumber("9999999999"));
        }

        [TestMethod]
        public void ValidSocialSecurityNumber_ZeroInput_AssertFalse()
        {
            LocalManager comMan = new LocalManager();

            Assert.IsFalse(comMan.ValidSocialSecurityNumber("0000000000"));
        }
    }
}
