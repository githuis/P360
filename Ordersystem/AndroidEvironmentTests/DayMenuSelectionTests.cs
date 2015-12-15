using System;
using NUnit.Framework;
using Ordersystem.Enums;
using Ordersystem.Model;

namespace AndroidEnvironmentTests
{
    [TestFixture]
    public class DayMenuSelectionTests
    {
        [Test]
        public void DayMenuSelection_CreateWithValidInput_AssertTrue()
        {
            var dayMenu = new DayMenu(new Dish("a", "a", "a"), new Dish("a", "a", "a"), new Dish("a", "a", "a"),
                DateTime.Today);
            var selection = new DayMenuSelection(dayMenu, DayMenuChoice.Dish1, true);

            Assert.IsTrue(selection.Choice == DayMenuChoice.Dish1 && selection.SideDish && selection.Date == DateTime.Today && selection.DayMenu == dayMenu);
        }

        [Test]
        public void DayMenuSelection_CreateWithNullDayMenu_AssertThrow()
        {
			Assert.Throws (typeof(ArgumentNullException), () => {
				new DayMenuSelection (null, DayMenuChoice.Dish1, true);
			});
        }

        [Test]
        public void DayMenuSelection_GetDishes_AssertTrue()
        {
            var dayMenu = new DayMenu(new Dish("a", "a", "a"), new Dish("a", "a", "a"), new Dish("a", "a", "a"),
                DateTime.Today);
            var selection = new DayMenuSelection(dayMenu, DayMenuChoice.Dish1, true);
            var dishes = selection.GetDishes();

            Assert.IsTrue(dishes.Contains(selection.DayMenu.Dish1) && dishes.Contains(selection.DayMenu.Dish2) && dishes.Contains(selection.DayMenu.SideDish));
        }
    }
}
