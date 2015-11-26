using System;
using NUnit.Framework;
using Ordersystem.Utilities;


namespace AndroidTests
{
    [TestFixture]
    public class LocalDatabaseTests
    {
        private LocalDatabase _localDatabase;

        [TestFixtureSetUp]
        public void FixtureSetUp()
        {
            _localDatabase = new LocalDatabase("testDatabase");
            _localDatabase.ClearDatabase();
        }


        [TearDown]
        public void TearDown()
        {
            _localDatabase.ClearDatabase();
        }

        [Test]
        public void Pass()
        {
            Console.WriteLine("test1");
            Assert.True(true);
        }
    }
}