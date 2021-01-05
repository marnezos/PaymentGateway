using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentGateway.Domain.Economics;
using PaymentGateway.Domain.Common;

namespace PaymentGateway.UnitTests.Domain.Economics
{
    [TestClass]
    public class CurrencyTests
    {
        [TestMethod]
        public void Should_ReturnNoValidationErrors_When_ACurrencyWithANameIsRequested()
        {
            Currency currency = new Currency(0, "EUR");
            ValidationResults validationResults = currency.Validate();
            Assert.IsFalse(validationResults.HasErrors);
        }

        [TestMethod]
        public void Should_ReturnValidationErrors_When_ACurrencyWithoutANameIsRequested()
        {
            Currency currency = new Currency(0, "");
            ValidationResults validationResults = currency.Validate();
            Assert.IsTrue(validationResults.HasErrors);
        }
        [TestMethod]
        public void Should_CovertCorrectlyToString_WhenAValidCurrencyIsGiven()
        {
            Currency currency = new Currency(1, "EUR");
            Assert.AreEqual("1-EUR", currency.ToString());
        }
    }
}
