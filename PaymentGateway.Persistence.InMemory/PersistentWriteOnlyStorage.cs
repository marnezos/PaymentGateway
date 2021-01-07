using PaymentGateway.Application.Interfaces.Storage.Write;
using PaymentGateway.Persistence.InMemory.Context;
using PaymentGateway.Persistence.InMemory.Repositories.Payments;
using System;

namespace PaymentGateway.Persistence.InMemory
{
    /// <summary>
    /// Provides full write-only storage functionality. (ISR?)
    /// </summary>
    public class PersistentWriteOnlyStorage : IPeristentWriteOnlyStorage
    {
        private readonly bool _disposed = false;
        private readonly PaymentGatewayContext _dbContext;

        public PersistentWriteOnlyStorage(InMemoryPersistenceOptions options)
        {
            _dbContext = new PaymentGatewayContext(options);
            _dbContext.Database.EnsureCreated();
        }

        public ICurrencyWriteOnlyRepository CurrencyWriteRepository => throw new NotImplementedException();

        public IMerchantWriteRepository MerchantWriteRepository => throw new NotImplementedException();

        public IPaymentRequestWriteOnlyRepository PaymentRequestWriteRepository => new PaymentRequestWriteOnlyRepository(_dbContext);

        public IPaymentResponseWriteOnlyRepository PaymentResponseWriteRepository => new PaymentResponseWriteOnlyRepository(_dbContext);

        //ToDo: Honor DRY consolidating Read & Write
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
