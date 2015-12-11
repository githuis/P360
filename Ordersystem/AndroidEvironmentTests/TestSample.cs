using System;
using NUnit.Framework;
using Ordersystem.Utilities;


namespace AndroidEnvironmentTests
{
    [TestFixture]
    public class TestsSample
    {
        private LocalDatabase _localDatabase;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            _localDatabase = new LocalDatabase("localDatabase");
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