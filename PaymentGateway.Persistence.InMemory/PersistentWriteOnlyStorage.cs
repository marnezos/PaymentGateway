using PaymentGateway.Application.Interfaces.Storage.Write;
using PaymentGateway.Persistence.InMemory.Context;
using System;

namespace PaymentGateway.Persistence.InMemory
{
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

        public IPaymentRequestWriteOnlyRepository PaymentRequestWriteRepository => throw new NotImplementedException();

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
