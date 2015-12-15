using System;
using NUnit.Framework;
using Ordersystem.Enums;
using Ordersystem.Model;

namespace AndroidEnvironmentTests
{
    [TestFixture]
    public class DayMenuTests
    {
        [Test]
        public void DayMenu_CreateWithValidInput_AssertTrue()
        {
            var dish1 = new Dish("Dish1", "This", "Here");
            var dish2 = new Dish("Dish2", "That", "There");
            var sdish = new Dish("SideDish", "Something", "Somewhere");
            var dm = new DayMenu(dish1, dish2, sdish, new DateTime(2015, 11, 30));

            Assert.IsTrue(dm.Dish1 == dish1 && dm.Dish2 == dish2 && dm.SideDish == sdish && dm.Date == new DateTime(2015, 11, 30));
        }

        [Test]
        public void DayMenu_CreateWithNullDish1_AssertTrue()
        {
            var dish2 = new Dish("Dish2", "That", "There");
            var sdish = new Dish("SideDish", "Something", "Somewhere");
			Assert.Throws (typeof(ArgumentNullException), () => {
				new DayMenu (null, dish2, sdish, new DateTime (2015, 11, 30));
			});
        }

        [Test]
        public void DayMenu_CreateWithNullDish2_AssertTrue()
        {
            var dish1 = new Dish("Dish1", "This", "Here");
            var sdish = new Dish("SideDish", "Something", "Somewhere");
			Assert.Throws (typeof(ArgumentNullException), () => {
				new DayMenu (dish1, null, sdish, new DateTime (2015, 11, 30));
			});
        }

        [Test]
        public void DayMenu_CreateWithNullSideDish_AssertTrue()
        {
            var dish1 = new Dish("Dish1", "This", "Here");
            var dish2 = new Dish("Dish2", "That", "There");
			Assert.Throws (typeof(ArgumentNullException), () => {
				new DayMenu (dish1, dish2, null, new DateTime (2015, 11, 30));
			});
        }
    }
}
