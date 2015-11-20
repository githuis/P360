using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using NUnit.Framework;
using Ordersystem.Model;
using Ordersystem.Utilities;

namespace AndroidTests
{
    /// <summary>
    /// None of these work.
    /// </summary>
    [TestFixture]
    public class TestsSample
    {
        private LocalDatabase _database;

        [TestFixtureSetUp]
        public void FixtureSetup()
        {
            //Xamarin.Forms.Forms.Init()????????????????????????
            _database = new LocalDatabase("TestDatabase");
            _database.ClearDatabase();
        }

        [TearDown]
        public void Tear()
        {
            _database.ClearDatabase();
        }

        [Test]
        public void LocalDatabase_SaveValidSession()
        {
            /* Setup */
            Customer customer = new Customer(1122334455, "Bo", Diet.Full) {Order = new Order()};
            Orderlist orderlist = new Orderlist(new List<DayMenu>(), new DateTime(2014, 11, 20),
                new DateTime(2016, 11, 20), Diet.Full);

            for (int i = 0; i < 31; i++)
            {
                orderlist.AddDayMenu(new DayMenu(new Dish(string.Format("TestDish" + i + ".1"), "This is a test"),
                    new Dish(string.Format("TestDish" + i + ".2"), "This is a test"),
                    new Dish(string.Format("TestSideDish" + i), "This is a test")));
            }

            /* Action */
            _database.SaveOrder(orderlist, customer);

            /* Assert */
            Assert.IsNotNull(_database.GetOrder(customer.PersonNumber));
        }
    }
}