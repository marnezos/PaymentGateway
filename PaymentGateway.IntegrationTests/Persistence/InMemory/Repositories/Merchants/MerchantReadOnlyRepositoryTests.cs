using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentGateway.Application.Interfaces.Storage.Read;
using PaymentGateway.Domain.Merchants;
using PaymentGateway.Persistence.InMemory;
using System.Threading.Tasks;

namespace PaymentGateway.IntegrationTests.Persistence.InMemory.Repositories.Merchants
{
    [TestClass]
    public class MerchantReadOnlyRepositoryTests
    {

        [TestMethod]
        public async Task Should_ReturnAMerchant_Given_IdAsParameter()
        {
            using IPersistentReadOnlyStorage persistentReadOnlyStorage = new PersistentReadOnlyStorage(new InMemoryPersistenceOptions()
            {
                InMemoryDbName = "test"
            });

            Merchant merchant = await persistentReadOnlyStorage.MerchantReadRepository.GetByIdAsync(1);

            Assert.AreEqual("merchant1", merchant.Name);

        }
    }
}
