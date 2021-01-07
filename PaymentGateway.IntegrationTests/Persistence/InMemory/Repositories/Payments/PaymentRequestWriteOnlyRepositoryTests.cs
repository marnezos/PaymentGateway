using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentGateway.Application.Interfaces.Storage.Read;
using PaymentGateway.Application.Interfaces.Storage.Write;
using PaymentGateway.Domain.Cards;
using PaymentGateway.Domain.Economics;
using PaymentGateway.Domain.Merchants;
using PaymentGateway.Domain.Payments;
using PaymentGateway.Persistence.InMemory;
using System;
using System.Threading.Tasks;

namespace PaymentGateway.IntegrationTests.Persistence.InMemory.Repositories.Payments
{
    [TestClass]
    public class PaymentRequestWriteOnlyRepositoryTests
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
        public async Task Should_BeAbleToInsertANewPaymentRequestAndRetrieveItBack_Given_AValidPaymentRequest()
        {
            Merchant merchant = await _persistentReadOnlyStorage.MerchantReadRepository.GetByIdAsync(1);
            Currency currency = await _persistentReadOnlyStorage.CurrencyReadRepository.GetByNameAsync("EUR");
            Card card = new Card("4111111111111111", 1, 2021, "123");
            MoneyAmount amount = new MoneyAmount(currency, 42.42M);
            string uniqueRequestId = Guid.NewGuid().ToString();
            PaymentRequest request = new PaymentRequest(0,uniqueRequestId, merchant, card, amount, DateTime.Now);

            await _persistentWriteOnlyStorage.PaymentRequestWriteRepository.SaveAsync(request);
            PaymentRequest storedRequest = await _persistentReadOnlyStorage.PaymentRequestReadRepository.GetByMerchantIdAndMerchantUniqueIdAsync(merchant.Id, uniqueRequestId);

            Assert.AreEqual(request.UniqueHash, storedRequest.UniqueHash);
        }

        [ClassCleanup]
        public static void TearDownTemporaryObjects()
        {
            _persistentWriteOnlyStorage.Dispose();
            _persistentReadOnlyStorage.Dispose();
        }
    }
}
