using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentGateway.Domain.Merchants;

namespace PaymentGateway.UnitTests.Domain.Merchants
{
    [TestClass]
    public class MerchantTests
    {
        [TestMethod]
        public void Should_ReturnNoValidationErrors_When_AMerchantWithANameAndAValidEmailIsCreated()
        {
            Merchant merchant = new Merchant("supermerchant", "supermerchant@example.com");

            var validationResults = merchant.Validate();

            Assert.IsFalse(validationResults.HasErrors);
        }

        [TestMethod]
        public void Should_ReturnValidationError_When_AMerchantWithAnEmptyNameAndAValidEmailIsCreated()
        {
            Merchant merchant = new Merchant("", "supermerchant@example.com");

            var validationResults = merchant.Validate();

            Assert.IsTrue(validationResults.HasErrors);
        }


        [TestMethod]
        public void Should_ReturnValidationError_When_AMerchantWithANameAndAnInvalidEmailIsCreated()
        {
            Merchant merchant = new Merchant("supermerchant", "supermerchant@example");

            var validationResults = merchant.Validate();

            Assert.IsTrue(validationResults.HasErrors);
        }

        [TestMethod]
        public void Should_ReturnValidationError_When_AMerchantWithANameAndAnEmptyEmailIsCreated()
        {
            Merchant merchant = new Merchant("supermerchant", "");

            var validationResults = merchant.Validate();

            Assert.IsTrue(validationResults.HasErrors);
        }

        [TestMethod]
        public void Should_ReturnValidationError_When_AMerchantWithoutANameAndAnEmptyEmailIsCreated()
        {
            Merchant merchant = new Merchant("", "");

            var validationResults = merchant.Validate();

            Assert.IsTrue(validationResults.HasErrors);
        }

        [TestMethod]
        public void Should_ReturnValidationError_When_AMerchantWithoutANameAndAnInvalidEmailIsCreated()
        {
            Merchant merchant = new Merchant("", "supermail");

            var validationResults = merchant.Validate();

            Assert.IsTrue(validationResults.HasErrors);
        }
    }
}
