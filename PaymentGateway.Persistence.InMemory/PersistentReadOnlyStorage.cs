using Microsoft.EntityFrameworkCore;
using PaymentGateway.Application.Interfaces.Storage.Read;
using PaymentGateway.Persistence.InMemory.Context;
using PaymentGateway.Persistence.InMemory.Repositories.Economics;
using System;

namespace PaymentGateway.Persistence.InMemory
{
    public class PersistentReadOnlyStorage : IPersistentReadOnlyStorage
    {
        private readonly bool _disposed = false;
        private readonly PaymentGatewayContext _dbContext;

        public PersistentReadOnlyStorage(InMemoryPersistenceOptions options)
        {
            _dbContext = new PaymentGatewayContext(options);
            _dbContext.Database.EnsureCreated();
        }

        public ICurrencyReadOnlyRepository CurrencyReadRepository => new CurrencyReadOnlyRepository(_dbContext);

        public IMerchantReadRepository MerchantReadRepository => throw new System.NotImplementedException();

        public IPaymentRequestReadOnlyRepository PaymentRequestReadRepository => throw new System.NotImplementedException();

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed && disposing)
            {
                _dbContext.Dispose();
            }
        }
    }
}
