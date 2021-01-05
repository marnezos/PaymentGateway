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
            Merchant merchant = new Merchant(1, "supermerchant", "supermerchant@example.com");

            var validationResults = merchant.Validate();

            Assert.IsFalse(validationResults.HasErrors);
        }

        [TestMethod]
        public void Should_ReturnValidationError_When_AMerchantWithAnEmptyNameAndAValidEmailIsCreated()
        {
            Merchant merchant = new Merchant(1, "", "supermerchant@example.com");

            var validationResults = merchant.Validate();

            Assert.IsTrue(validationResults.HasErrors);
        }

        [TestMethod]
        public void Should_ReturnValidationError_When_AMerchantWithAnVeryLongNameAndAValidEmailIsCreated()
        {
            Merchant merchant = new Merchant(1, new string('x',51), "supermerchant@example.com");

            var validationResults = merchant.Validate();

            Assert.IsTrue(validationResults.HasErrors);
        }

        [TestMethod]
        public void Should_ReturnValidationError_When_AMerchantWithANameAndAnInvalidEmailIsCreated()
        {
            Merchant merchant = new Merchant(1, "supermerchant", "supermerchant@example");

            var validationResults = merchant.Validate();

            Assert.IsTrue(validationResults.HasErrors);
        }

        [TestMethod]
        public void Should_ReturnValidationError_When_AMerchantWithANameAndAnEmptyEmailIsCreated()
        {
            Merchant merchant = new Merchant(1, "supermerchant", "");

            var validationResults = merchant.Validate();

            Assert.IsTrue(validationResults.HasErrors);
        }

        [TestMethod]
        public void Should_ReturnValidationError_When_AMerchantWithoutANameAndAnEmptyEmailIsCreated()
        {
            Merchant merchant = new Merchant(1, "", "");

            var validationResults = merchant.Validate();

            Assert.IsTrue(validationResults.HasErrors);
        }

        [TestMethod]
        public void Should_ReturnValidationError_When_AMerchantWithoutANameAndAnInvalidEmailIsCreated()
        {
            Merchant merchant = new Merchant(1, "", "supermail");

            var validationResults = merchant.Validate();

            Assert.IsTrue(validationResults.HasErrors);
        }

        [TestMethod]
        public void Should_ReturnValidationError_When_AMerchantWithoutANameAndAnVeryLongEmailIsCreated()
        {
            Merchant merchant = new Merchant(1, "supermerchant", $"{new string('x', 244)}@example.com");

            var validationResults = merchant.Validate();

            Assert.IsTrue(validationResults.HasErrors);
        }

        [TestMethod]
        public void Should_CovertCorrectlyToString_WhenAValidMerchantIsGiven()
        {
            Merchant merchant = new Merchant(1, "supermerchant", "supermerchant@example.com");
            Assert.AreEqual("1-supermerchant-supermerchant@example.com", merchant.ToString());
        }
    }
}
