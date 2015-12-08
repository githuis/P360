using System;
using NUnit.Framework;
using Ordersystem.Utilities;
using Ordersystem.Model;
using Ordersystem.Enums;
using Ordersystem.Exceptions;
using System.Collections.Generic;

namespace AndroidEnvironmentTests
{
    [TestFixture]
    public class TestsSample
    {
        private Customer _dummyCustomer;
        private Orderlist _dummyOrderlist;
        private LocalDatabase _localDatabase;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            _localDatabase = new LocalDatabase("localDatabase");
            _dummyCustomer = new Customer("1303951337", "Thomas Frandsen", Diet.Full);
            _dummyOrderlist = new Orderlist(new List<DayMenu>(), new DateTime(1995, 3, 13), new DateTime(), Diet.Full);
            _localDatabase.ClearDatabase();
        }

        [TearDown]
        public void TearDown()
        {
            _localDatabase.ClearDatabase();
        }

        [Test]
        public void SaveNLoad_dummyInput_OrderlistStartDateIsUnchanged()
        {
            _localDatabase.SaveSession(_dummyOrderlist, _dummyCustomer);
            var loadedOrderlist = _localDatabase.GetOrderlist(_dummyCustomer.PersonNumber);

            Assert.True(loadedOrderlist.StartDate == new DateTime(1995, 3, 13));
        }

        [Test]
        public void SaveNDelete_dummyInput_NoExceptionsIsThrown()
        {
            _localDatabase.SaveSession(_dummyOrderlist, _dummyCustomer);

            Assert.DoesNotThrow(() => _localDatabase.DeleteSession(_dummyCustomer.PersonNumber));
        }

        [Test]
        public void DeleteNotFound_dummyInput_ItemNotFoundExceptionIsThrown()
        {
            Assert.Throws(typeof(ItemNotFoundException), () => _localDatabase.DeleteSession(_dummyCustomer.PersonNumber));
        }
    }
}