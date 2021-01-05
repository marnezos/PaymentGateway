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

    }
}
