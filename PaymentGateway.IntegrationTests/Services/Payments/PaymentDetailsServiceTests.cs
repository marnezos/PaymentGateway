using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentGateway.Application.DTOs.Payments.PaymentDetails;
using PaymentGateway.Application.DTOs.Payments.ProcessPayment;
using PaymentGateway.Application.Interfaces.Bank;
using PaymentGateway.Application.Interfaces.Storage.Read;
using PaymentGateway.Application.Interfaces.Storage.Write;
using PaymentGateway.Application.Services.Bank;
using PaymentGateway.Application.Services.Payments.PaymentDetails;
using PaymentGateway.Application.Services.Payments.ProcessPayment;
using PaymentGateway.Domain.Cards;
using PaymentGateway.Domain.Economics;
using PaymentGateway.Domain.Merchants;
using PaymentGateway.Domain.Payments;
using PaymentGateway.Persistence.InMemory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.IntegrationTests.Services.Payments
{
    [TestClass]
    public class PaymentDetailsServiceTests
    {

        private static IPeristentWriteOnlyStorage _persistentWriteOnlyStorage;
        private static IPersistentReadOnlyStorage _persistentReadOnlyStorage;
        [ClassInitialize]
        public static void InitDb(TestContext context)
        {
            _persistentWriteOnlyStorage = new PersistentWriteOnlyStorage(new InMemoryPersistenceOptions()
            {
                InMemoryDbName = "test"
            });
            _persistentReadOnlyStorage = new PersistentReadOnlyStorage(new InMemoryPersistenceOptions()
            {
                InMemoryDbName = "test"
            });
        }

        [TestMethod]
        public async Task Should_RetrieveAPaymentDetailsInDatabase_Given_ValidPayment()
        {
            IAcquiringBank bank = new FakeBank();
            string uniqueRequestId = Guid.NewGuid().ToString();

            PaymentProcessRequestDto request = new PaymentProcessRequestDto()
            {
                Amount = 42.0M,
                CardCvv = "123",
                CardExpirationMonth = 1,
                CardExpirationYear = 2030,
                CardNumber = "4111111111111111",
                CurrencyIso4217 = "EUR",
                MerchantId = 1,
                MerchantUniqueRequestId = uniqueRequestId
            };
            
            ProcessPaymentService processPaymentService = new ProcessPaymentService(bank, _persistentReadOnlyStorage, _persistentWriteOnlyStorage);
            await processPaymentService.ProcessPayment(request);

            PaymentDetailsService paymentDetailsService = new PaymentDetailsService(_persistentReadOnlyStorage);
            PaymentDetailsResponseDto detailsResponseDto = await paymentDetailsService.RetrievePaymentDetails(new PaymentDetailsRequestDto()
            {
                MerchantId = 1,
                MerchantUniqueRequestId = uniqueRequestId
            });

            Assert.IsNotNull(detailsResponseDto);
        }


        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException), "A non existing response was erroneously replied with an existing.")]
        public async Task Should_NotRetrieveAPaymentDetailsInDatabase_Given_AnInvalidRequestId()
        {
            IAcquiringBank bank = new FakeBank();
            string uniqueRequestId = Guid.NewGuid().ToString();

            PaymentProcessRequestDto request = new PaymentProcessRequestDto()
            {
                Amount = 42.0M,
                CardCvv = "123",
                CardExpirationMonth = 1,
                CardExpirationYear = 2030,
                CardNumber = "4111111111111111",
                CurrencyIso4217 = "EUR",
                MerchantId = 1,
                MerchantUniqueRequestId = uniqueRequestId
            };

            ProcessPaymentService processPaymentService = new ProcessPaymentService(bank, _persistentReadOnlyStorage, _persistentWriteOnlyStorage);
            await processPaymentService.ProcessPayment(request);

            PaymentDetailsService paymentDetailsService = new PaymentDetailsService(_persistentReadOnlyStorage);
            await paymentDetailsService.RetrievePaymentDetails(new PaymentDetailsRequestDto()
            {
                MerchantId = 1,
                MerchantUniqueRequestId = "whatever"
            });

        }


        [ClassCleanup]
        public static void TearDownTemporaryObjects()
        {
            _persistentWriteOnlyStorage.Dispose();
            _persistentReadOnlyStorage.Dispose();
        }
    }
}
