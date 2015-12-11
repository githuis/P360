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
        private string filename = "localDatabase";

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            _localDatabase = new LocalDatabase(filename);
            _dummyCustomer = new Customer("1303951337", "Thomas Frandsen", Diet.Full);
            _dummyOrderlist = new Orderlist(new List<DayMenu>(), new DateTime(1995, 3, 13), new DateTime(), Diet.Full);
            _localDatabase.ClearDatabase();
            _localDatabase.Close();
        }

        [SetUp]
        public void SetUp()
        {
            _localDatabase.Open(filename);
        }

        [TearDown]
        public void TearDown()
        {
            _localDatabase.ClearDatabase();
            _localDatabase.Close();
        }

        [Test]
        public void SaveNLoad_dummyInput_OrderlistStartDateIsUnchanged()
        {
            _localDatabase.SaveSession(_dummyOrderlist, _dummyCustomer);
            _localDatabase.Close();
            _localDatabase.Open(_localDatabase.Filename);
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