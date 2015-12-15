using System;
using System.Collections.Generic;
using NUnit.Framework;
using Ordersystem.Model;
using Ordersystem.Enums;

namespace AndroidEvironmentTests.IntegrationTests
{
	[TestFixture]
	public class ModelIntegration
	{
		[Test]
		public void DaymenuDish_AddDishesToDaymenu_AssertSame()
		{
			var dish1 = new Dish ("Dish1", "Dish1Description", "");
			var dish2 = new Dish ("Dish2", "Dish2Description", "");
			var sideDish = new Dish ("SideDish", "SideDishDescription", "");
			var dayMenu = new DayMenu (dish1, dish2, sideDish, DateTime.Today);

			Assert.AreSame (dayMenu.Dish1, dish1);
			Assert.AreSame (dayMenu.Dish2, dish2);
			Assert.AreSame (dayMenu.SideDish, sideDish);
		}

		[Test]
		public void OrderlistDaymenu_AddDaymenusToOrderlist_AssertSame()
		{
			DayMenu[] dayMenus = new DayMenu[DateTime.DaysInMonth(2016, 7)];

			for (int i = 0; i < DateTime.DaysInMonth (2016, 7); i++)
			{
				dayMenus [i] = new DayMenu (new Dish ("Dish1." + i, "Dish1." + i + "Description", ""), 
					new Dish ("Dish2." + i, "Dish2." + i + "Description", ""), 
					new Dish ("SideDish." + i, "SideDish." + i + "Description", ""), 
					new DateTime (2016, 7, i + 1));
			}

			var orderlist = new Orderlist (new List<DayMenu> (), new DateTime (2016, 7, 1), new DateTime (2016, 7, 18), Diet.Full);
			for (int i = 0; i < DateTime.DaysInMonth (2016, 7); i++) 
			{
				orderlist.AddDayMenu (dayMenus [i]);
			}

			for (int i = 0; i < DateTime.DaysInMonth (2016, 7); i++)
			{
				Assert.AreSame (orderlist.DayMenus [i], dayMenus [i]);
			}
		}

		[Test]
		public void DayMenuSelectionDaymenu_AddDayMenuToDayMenuSelection_AssertSame()
		{
			var dayMenu = new DayMenu (new Dish ("Dish1", "Dish1Description", ""), 
				new Dish ("Dish2", "DishDescription", ""), 
				new Dish ("SideDish", "SideDishDescription", ""), 
				new DateTime (2016, 7, 1));

			var dayMenuSelection = new DayMenuSelection (dayMenu);

			Assert.AreSame (dayMenuSelection.DayMenu, dayMenu);
		}

		[Test]
		public void OrderDayMenuSelection_AddDayMenuSelectionToOrder_AssertSame ()
		{
			int daysInMonth = DateTime.DaysInMonth (DateTime.Today.AddMonths(1).Year, DateTime.Today.AddMonths(1).Month);
			var order = new Order ();
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

			for (int i = 0; i < daysInMonth; i++)
			{
				order.AddDayMenuSelection (dayMenus [i], DayMenuChoice.Dish1);
			}

			for (int i = 0; i < daysInMonth; i++)
			{
				Assert.AreSame (order.DayMenuSelections [i].DayMenu, dayMenus [i]);
				Assert.AreEqual (order.DayMenuSelections [i].Choice, DayMenuChoice.Dish1);
				Assert.IsFalse (order.DayMenuSelections [i].SideDish);
			}
		}

		[Test]
		public void CustomerOrder_AddOrderToCustomer_AssertSame()
		{
			var customer = new Customer ("0706951235", "Rasmus Møller Jensen", Diet.Full);
			int daysInMonth = DateTime.DaysInMonth (DateTime.Today.AddMonths(1).Year, DateTime.Today.AddMonths(1).Month);
			var order = new Order ();
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

			customer.Order = order;

			Assert.AreSame (customer.Order, order);
		}
	}
}

