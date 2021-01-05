using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentGateway.Domain.Economics;
using System;

namespace PaymentGateway.UnitTests.Domain.Economics
{
    [TestClass]
    public class MoneyAmountTests
    {
        private Currency _euros = new Currency(1, "EUR");
        [TestMethod]
        public void Should_CorrectlyInstantiateAnAmountOfMoney_When_ACurrencyAndAPositiveAmountIsGiven()
        {
            MoneyAmount moneyAmount = new MoneyAmount(_euros, 42.0M);
            Assert.IsNotNull(moneyAmount);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "A money amount with negative amount was inappropriately allowed.")]
        public void Should_ProhibitAMoneyAMount_When_AMoneyAmountWithNegativeAmountIsRequested()
        {
            new MoneyAmount(_euros, -0.01M);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "A money amount with empty currency was inappropriately allowed.")]
        public void Should_ProhibitAMoneyAMount_When_AMoneyAmountWithoutACurrencyIsRequested()
        {
            new MoneyAmount(null, -0.01M);
        }

        [TestMethod]
        public void Should_CovertCorrectlyToString_WhenAValidMoneyAmountIsGiven()
        {
            MoneyAmount moneyAmount = new MoneyAmount(_euros, 42.12M);
            Assert.AreEqual("1-EUR-42.12", moneyAmount.ToString());
        }
    }
}
