using System;
using NUnit.Framework;
using Ordersystem.Utilities;
using Ordersystem.Model;
using Ordersystem.Enums;
using Ordersystem.Exceptions;
using System.Collections.Generic;
using Ordersystem.Functions;
using System.IO;

namespace AndroidEvironmentTests.IntegrationTests
{
	[TestFixture]
	public class LocalManagerTests
	{
		private LocalManager _localManager;

		[TestFixtureSetUp]
		public void FixtureSetUp()
		{
			_localManager = new LocalManager ("DerpTestTheSecond");
		}

		[SetUp]
		public void TestSetUp()
		{
			int daysInMonth = DateTime.DaysInMonth (DateTime.Today.AddMonths(1).Year, DateTime.Today.AddMonths(1).Month);
			_localManager.Customer = new Customer ("0706951235", "Rasmus Møller Jensen", Diet.Full);
			_localManager.Customer.Order = new Order ();
			List<DayMenu> dayMenus = new List<DayMenu>();
			for (int i = 0; i < daysInMonth; i++)
			{
				dayMenus.Add(new DayMenu(new Dish("dish1." + i, "description1." + i, ""),
					new Dish("dish2." + i, "description2." + i, ""),
					new Dish("dish3." + i, "description3." + i, ""),
					new DateTime(DateTime.Today.Year, DateTime.Today.Month, i+1).AddMonths(1)));
			}
			_localManager.Orderlist = new Orderlist (dayMenus, new DateTime (DateTime.Today.Year, DateTime.Today.Month, 1), 
				new DateTime (DateTime.Today.Year, DateTime.Today.Month, 18), Diet.Full);
			_localManager.Customer.Order.SetSelectionLength (daysInMonth, _localManager.Orderlist);
		}

		[TearDown]
		public void TestTearDown()
		{
			_localManager.Customer = null;
			_localManager.Orderlist = null;
		}

		[Test]
		public void IsValidSocialSecurityNumber_ValidInput_AssertTrue()
		{
			Assert.IsTrue (_localManager.IsValidSocialSecurityNumber ("0706951235"));
		}

		[Test]
		public void IsValidSocialSecurityNumber_InvalidInput_AssertFalse()
		{
			Assert.IsFalse (_localManager.IsValidSocialSecurityNumber ("9999991235"));
		}

		[Test]
		public void LogOut_AssertTrue()
		{
			_localManager.LogOut ();

			Assert.IsTrue (_localManager.Customer == null && _localManager.Orderlist == null);
		}

		[Test]
		public void FillInvalidOrder_AssertTrue()
		{
			_localManager.FillInvalidOrder ();

			foreach (DayMenuSelection selection in _localManager.Customer.Order.DayMenuSelections) {
				Assert.IsTrue (selection.Choice == DayMenuChoice.Dish1);
			}
		}

		[Test]
		public void IsOrderValid_ValidOrder_AssertTrue()
		{
			_localManager.FillInvalidOrder ();

			Assert.IsTrue (_localManager.IsOrderValid ());
		}

		[Test]
		public void IsOrderValid_InvalidOrder_AssertFalse()
		{
			Assert.IsFalse (_localManager.IsOrderValid());
		}

		[Test]
		public void IsNumberBetween_TrueInput_AssertTrue()
		{
			Assert.IsTrue (_localManager.IsNumberBetween("8", 6, 10));
		}

		[Test]
		public void IsNumberBetween_FalseInput_AssertFalse()
		{
			Assert.IsFalse (_localManager.IsNumberBetween ("10", 6, 8));
		}

		[Test]
		public void IsNumberBetween_InvalidInput_AssertFalse()
		{
			Assert.IsFalse (_localManager.IsNumberBetween ("Hej", 6, 8));	
		}

		[Test]
		public void IsStringDigitsOnly_TrueInput_AssertTrue()
		{
			Assert.IsTrue (_localManager.IsStringDigitsOnly ("123456789"));
		}

		[Test]
		public void IsStringDigitsOnly_FalseInput_AssertTrue()
		{
			Assert.IsFalse (_localManager.IsStringDigitsOnly ("asdfghjk"));
		}

		/* 
		 * Can't test LogIn og GetFromDB functions, due to authentication issues with the database and the test environment.
		 */
	}
}

