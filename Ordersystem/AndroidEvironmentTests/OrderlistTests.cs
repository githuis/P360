using System;
using System.Collections.Generic;
using NUnit.Framework;
using Ordersystem.Enums;
using Ordersystem.Model;

namespace AndroidEnvironmentTests
{
    [TestFixture]
    public class OrderlistTests
    {
        [Test]
        public void Orderlist_CreateWithValidInput_AssertTrue()
        {

            var list = new List<DayMenu>();
            var orderlist = new Orderlist(list, new DateTime(2015, 11, 30), new DateTime(2015, 12, 30),
                Diet.Full);
            
            Assert.IsTrue(list == orderlist.DayMenus && orderlist.StartDate == new DateTime(2015, 11, 30) &&
				orderlist.EndDate == new DateTime(2015, 12, 30) && orderlist.Diet == Diet.Full);
        }

        [Test]
        public void Orderlist_CreateWithNullList_AssertThrow()
        {
			Assert.Throws (typeof(ArgumentNullException), () => {
				new Orderlist (null, new DateTime (2015, 11, 30), new DateTime (2015, 12, 30), Diet.Full);
			});
        }

        [Test]
        public void Orderlist_AddDayMenu_AssertTrue()
        {
            var orderlist = new Orderlist(new List<DayMenu>(), new DateTime(2015, 11, 30), new DateTime(2015, 12, 30),
                Diet.Full);
            var dayMenu = new DayMenu(new Dish("a", "a", "a"), new Dish("a", "a", "a"), new Dish("a", "a", "a"),
                DateTime.Today);

            orderlist.AddDayMenu(dayMenu);

            Assert.IsTrue(orderlist.DayMenus.Contains(dayMenu));
        }

        [Test]
        public void Orderlist_AddNullDayMenu_AssertThrow()
        {
            var orderlist = new Orderlist(new List<DayMenu>(), new DateTime(2015, 11, 30), new DateTime(2015, 12, 30),
                Diet.Full);

			Assert.Throws (typeof(ArgumentNullException), () => {
				orderlist.AddDayMenu (null);
			});

            Assert.IsTrue(orderlist.DayMenus.Count == 0);
        }

        [Test]
        public void Orderlist_GetDayMenuByDate_AssertTrue()
        {
            var orderlist = new Orderlist(new List<DayMenu>{new DayMenu(new Dish("a", "a", "a"), new Dish("a", "a", "a"), new Dish("a", "a", "a"),
                DateTime.Today)}, new DateTime(2015, 11, 30), new DateTime(2015, 12, 30),
                Diet.Full);

            var dayMenu = orderlist.GetDayMenuByDate(DateTime.Today);

            Assert.IsTrue(orderlist.DayMenus.Contains(dayMenu) && dayMenu.Date == DateTime.Today);
        }

        [Test]
        public void Orderlist_GetDayMenuByWrongDate_AssertThrow()
        {
            var orderlist = new Orderlist(new List<DayMenu>{new DayMenu(new Dish("a", "a", "a"), new Dish("a", "a", "a"), new Dish("a", "a", "a"),
                DateTime.Today)}, new DateTime(2015, 11, 30), new DateTime(2015, 12, 30),
                Diet.Full);

			Assert.Throws (typeof(NullReferenceException), () => {
				orderlist.GetDayMenuByDate (new DateTime (1999, 1, 1));
			});
        }
    }
}
