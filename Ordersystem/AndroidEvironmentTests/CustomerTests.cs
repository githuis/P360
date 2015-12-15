using System;
using Ordersystem.Enums;
using Ordersystem.Model;
using NUnit.Framework;

namespace AndroidEnvironMentTests
{
    [TestFixture]
    public class CustomerTests
    {
        [Test]
        public void Customer_CreateWithValidInput_AssertTrue()
        {
            var cust = new Customer("1224000001", "Jesus Kristus", Diet.LowFat);

            Assert.IsTrue(cust.PersonNumber == "1224000001" && cust.Name == "Jesus Kristus" && cust.Diet == Diet.LowFat);
        }

        [Test]
        public void Customer_CreateWithUpperBorderInput_AssertTrue()
        {
            var cust = new Customer("1231999999", "ææææææææææææææææææææææææææææææææææææææææææææææææææææ", Diet.Full);

            Assert.IsTrue(cust.PersonNumber == "1231999999" && cust.Name == "ææææææææææææææææææææææææææææææææææææææææææææææææææææ" && cust.Diet == Diet.Full);
        }

        [Test]
        public void Customer_CreateWithLowerBorderInput_AssertTrue()
        {
            var cust = new Customer("0000000000", "a", Diet.Full);

            Assert.IsTrue(cust.PersonNumber == "0000000000" && cust.Name == "a" && cust.Diet == Diet.Full);
        }

        [Test]
        public void Customer_CreateWithNullPersonNumber_AssertThrow()
        {
			Assert.Throws (typeof(ArgumentNullException), () => {
				new Customer (null, "Karl", Diet.Full);
			});
        }

        [Test]
        public void Customer_CreateWithWhitespacePersonNumber_AssertThrow()
        {
			Assert.Throws (typeof(ArgumentNullException), () => {
				new Customer (" ", "Karl", Diet.Full);
			});
        }

        [Test]
        public void Customer_CreateWithNullName_AssertThrow()
        {
			Assert.Throws (typeof(ArgumentNullException), () => {
				new Customer ("0000000000", null, Diet.Full);
			});
        }

        [Test]
        public void Customer_CreateWithWhitespaceName_AssertThrow()
        {
			Assert.Throws (typeof(ArgumentNullException), () => {
				new Customer ("0000000000", " ", Diet.Full);
			});
        }
    }
}
