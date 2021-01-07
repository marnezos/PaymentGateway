using Microsoft.EntityFrameworkCore;
using PaymentGateway.Application.Interfaces.Storage.Read;
using PaymentGateway.Persistence.InMemory.Context;
using PaymentGateway.Persistence.InMemory.Repositories.Economics;
using PaymentGateway.Persistence.InMemory.Repositories.Merchants;
using PaymentGateway.Persistence.InMemory.Repositories.Payments;
using System;

namespace PaymentGateway.Persistence.InMemory
{
    /// <summary>
    /// Provides full read-only storage functionality. (ISR?)
    /// </summary>
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

        public IMerchantReadOnlyRepository MerchantReadRepository => new MerchantReadOnlyRepository(_dbContext);

        public IPaymentRequestReadOnlyRepository PaymentRequestReadRepository => new PaymentRequestReadOnlyRepository(_dbContext);
        public IPaymentResponseReadOnlyRepository PaymentResponseReadOnlyRepository => new PaymentResponseReadOnlyRepository(_dbContext);

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
