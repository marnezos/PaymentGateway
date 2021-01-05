using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentGateway.Domain.Cards;
using PaymentGateway.Domain.Economics;
using PaymentGateway.Domain.Merchants;
using PaymentGateway.Domain.Payments;
using System;

namespace PaymentGateway.UnitTests.Domain.PaymentRequests
{
    [TestClass]
    public class PaymentRequestTests
    {
        private static Currency _euros = new Currency(1, "EUR");
        private static MoneyAmount _amount = new MoneyAmount(_euros, 42.0M);
        private static Merchant _merchant = new Merchant(1, "test", "tests@example.com");
        private static Card _card = new Card("123456", 1, 2000, "01234");

        [TestMethod]
        public void Should_CorrectlyGenerateASha256Hash_When_AValidPaymentRequestIsGiven()
        {
            PaymentRequest paymentRequest = new PaymentRequest("sample1", _merchant, _card, _amount,  DateTime.Parse("2000-01-01 01:02:03"));
            Assert.AreEqual("76CC59912D9EFF5AB18B5C4C7AA57CF37DB2E74FE4683771B05BE05374C5279F", paymentRequest.UniqueHash);
        }

        [TestMethod]
        public void Should_ReturnNoValidationErrors_When_AValidMPaymentRequestIsRequested()
        {
            PaymentRequest paymentRequest = new PaymentRequest("sample1", _merchant, _card, _amount, DateTime.Parse("2000-01-01 01:02:03"));
            Assert.IsTrue(paymentRequest.IsValid);
        }

        [TestMethod]
        public void Should_ReturnValidationErrors_When_APaymentRequestWithInvalidMerchantIsRequested()
        {
            Merchant merchant = new Merchant(1, "", "");
            PaymentRequest paymentRequest = new PaymentRequest("sample1", merchant, _card, _amount, DateTime.Parse("2000-01-01 01:02:03"));
            Assert.IsFalse(paymentRequest.IsValid);
        }

        [TestMethod]
        public void Should_ReturnValidationErrors_When_APaymentRequestWithAnEmptyMerchantIsRequested()
        {
            PaymentRequest paymentRequest = new PaymentRequest("sample1", null, _card, _amount, DateTime.Parse("2000-01-01 01:02:03"));
            Assert.IsFalse(paymentRequest.IsValid);
        }

        [TestMethod]
        public void Should_ReturnValidationErrors_When_APaymentRequestWithAnEmptyCardIsRequested()
        {
            PaymentRequest paymentRequest = new PaymentRequest("sample1", _merchant, null, _amount, DateTime.Parse("2000-01-01 01:02:03"));
            Assert.IsFalse(paymentRequest.IsValid);
        }


        [TestMethod]
        public void Should_ReturnValidationErrors_When_APaymentRequestWithAnEmptyAmountIsRequested()
        {
            PaymentRequest paymentRequest = new PaymentRequest("sample1", _merchant, _card, null, DateTime.Parse("2000-01-01 01:02:03"));
            Assert.IsFalse(paymentRequest.IsValid);
        }

    }
}
