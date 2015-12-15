using System;
using NUnit.Framework;
using Ordersystem.Model;
using Xamarin.Forms;

namespace AndroidEnvironmentTests
{
    [TestFixture]
    public class DishTests
    {
        [Test]
        public void Dish_CreateWithValidInput_AssertTrue()
        {
			var dish = new Dish("Flæskesteg", "m/ kartofler og brun sovs", "C:/");

			Assert.IsTrue(dish.Name == "Flæskesteg" && dish.Description == "m/ kartofler og brun sovs" && dish.ImageSource == "C:/");
        }

        [Test]
        public void Dish_CreateWithNullName_AssertThrow()
        {
			Assert.Throws (typeof(ArgumentNullException), () => {
				new Dish (null, "m/ kartofler og brun sovs", "C:/");
			});
        }

        [Test]
        public void Dish_CreateWithEmptyName_AssertThrow()
        {
			Assert.Throws (typeof(ArgumentNullException), () => {
				new Dish ("", "m/ kartofler og brun sovs", "C:/");
			});
        }

        [Test]
        public void Dish_CreateWithNullDescription_AssertThrow()
        {
			Assert.Throws (typeof(ArgumentNullException), () => {
				new Dish ("Flæskesteg", null, "C:/");
			});
        }

        [Test]
        public void Dish_CreateWithEmptyDescription_AssertThrow()
        {
			Assert.Throws (typeof(ArgumentNullException), () => {
				new Dish ("Flæskesteg", "", "C:/");
			});
        }

        [Test]
        public void Dish_CreateWithNullImageSource_AssertThrow()
        {
			Assert.Throws (typeof(ArgumentNullException), () => {
				new Dish ("Flæskesteg", "m/ kartofler og brun sovs", null);
			});
        }
    }
}
