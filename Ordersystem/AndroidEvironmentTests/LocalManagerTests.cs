using System;
using NUnit.Framework;
using Ordersystem.Utilities;
using Ordersystem.Model;
using Ordersystem.Enums;
using Ordersystem.Exceptions;
using System.Collections.Generic;
using Ordersystem.Functions;
using System.IO;

namespace AndroidEvironmentTests
{
	[TestFixture]
	public class LocalManagerTests
	{
		private LocalManager _localManager;

		[TestFixtureSetUp]
		public void FixtureSetUp()
		{
			_localManager = new LocalManager ("DerpTest");
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

		/* 
		 * Can't test further for two reasons:
		 * 1: Cannot connect to the database, due to authorization errors.
		 * 2: Xamarins implementation of NUnit doesn't support mocking of objects.
		 * Since all methods other that IsValidSocialSecurityNumber requires succesfully 
		 * running the LogIn method, since it's the only method setting the _customer field,
		 * and it's trying to connect to the database, all further testing is made impossible.
		 */
	}
}

