using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentGateway.Application.Interfaces.Storage.Read;
using PaymentGateway.Domain.Economics;
using PaymentGateway.Persistence.InMemory;
using System.Threading.Tasks;

namespace PaymentGateway.IntegrationTests.Persistence.InMemory.Repositories.Economics
{
    [TestClass]
    public class CurrencyReadOnlyRepositoryTests
    {
        [TestMethod]
        public async Task Should_ReturnCurrencyFromDatabase_Given_EuroAsParameter()
        {
            using IPersistentReadOnlyStorage persistentReadOnlyStorage = new PersistentReadOnlyStorage(new InMemoryPersistenceOptions()
            {
                InMemoryDbName = "test"
            });

            Currency currency = await persistentReadOnlyStorage.CurrencyReadRepository.GetByNameAsync("EUR");

            Assert.AreEqual("EUR",currency.Name);

        }

    }
}
