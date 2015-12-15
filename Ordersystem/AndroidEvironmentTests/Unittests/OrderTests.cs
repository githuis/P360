using System;
using System.Collections.Generic;
using NUnit.Framework;
using Ordersystem.Enums;
using Ordersystem.Model;

namespace AndroidEnvironmentTests.Unittests
{
    [TestFixture]
    public class OrderTests
    {
		private Order order;

		[SetUp]
		public void TestSetUp()
		{
			int daysInMonth = DateTime.DaysInMonth (DateTime.Today.AddMonths(1).Year, DateTime.Today.AddMonths(1).Month);
			order = new Order ();
			List<DayMenu> dayMenus = new List<DayMenu>();
			for (int i = 0; i < daysInMonth; i++)
			{
				dayMenus.Add(new DayMenu(new Dish("dish1." + i, "description1." + i, ""),
					new Dish("dish2." + i, "description2." + i, ""),
					new Dish("dish3." + i, "description3." + i, ""),
					new DateTime(DateTime.Today.Year, DateTime.Today.Month, i+1).AddMonths(1)));
			}
			var orderlist = new Orderlist (dayMenus, new DateTime (DateTime.Today.Year, DateTime.Today.Month, 1), 
				new DateTime (DateTime.Today.Year, DateTime.Today.Month, 18), Diet.Full);
			order.SetSelectionLength (daysInMonth, orderlist);
		}

		[TearDown]
		public void TestTearDown()
		{
			order = null;
		}

		[Test]
		public void Order_CreateWithValidInput_AssertTrue()
		{
			Assert.IsTrue(order.Sent == false && order.DayMenuSelections != null);
		}

        [Test]
        public void Order_AddDayMenuSelection_AssertTrue()
        {
		    order.AddDayMenuSelection(new DayMenu(new Dish("a", "a", "a"), new Dish("a", "a", "a"), new Dish("a", "a", "a"),
				new DateTime(DateTime.Today.Year, DateTime.Today.AddMonths(1).Month, 7)), DayMenuChoice.Dish1, true);

			Assert.IsTrue(order.DayMenuSelections[6].Date == new DateTime(DateTime.Today.Year, DateTime.Today.AddMonths(1).Month, 7) &&
				order.DayMenuSelections[6].Choice == DayMenuChoice.Dish1 &&
				order.DayMenuSelections[6].SideDish);
        }

        [Test]
        public void Order_AddDayMenuSelectionWithNullDayMenu_AssertThrow()
        {
			Assert.Throws (typeof(ArgumentNullException), () => {
				order.AddDayMenuSelection (null, DayMenuChoice.Dish1);
			});
        }

        [Test]
        public void Order_ChangeDayMenuSelection_AssertTrue()
        {
            var dayMenu = new DayMenu(new Dish("a", "a", "a"), new Dish("a", "a", "a"), new Dish("a", "a", "a"),
				new DateTime(DateTime.Today.Year, DateTime.Today.AddMonths(1).Month, 7));

            order.AddDayMenuSelection(dayMenu, DayMenuChoice.Dish1, true);

			order.ChangeDayMenuSelection(dayMenu.Date, DayMenuChoice.Dish2, false);

			Assert.IsTrue (order.DayMenuSelections [6].Date == new DateTime(DateTime.Today.Year, DateTime.Today.AddMonths(1).Month, 7) &&
				order.DayMenuSelections [6].Choice == DayMenuChoice.Dish2 &&
				!order.DayMenuSelections [6].SideDish);
        }
    }
}
