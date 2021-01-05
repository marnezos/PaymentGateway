using PaymentGateway.Application.Interfaces.Storage.Write;
using System;

namespace PaymentGateway.Persistence.InMemory
{
    public class PersistentWriteOnlyStorage : IPeristentWriteOnlyStorage
    {
        public ICurrencyWriteRepository CurrencyWriteRepository => throw new NotImplementedException();

        public IMerchantWriteRepository MerchantWriteRepository => throw new NotImplementedException();

        public IPaymentRequestWriteRepository PaymentRequestWriteRepository => throw new NotImplementedException();
    }
}
