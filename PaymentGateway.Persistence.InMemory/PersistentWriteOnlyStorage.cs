using PaymentGateway.Application.Interfaces.Storage.Write;
using System;

namespace PaymentGateway.Persistence.InMemory
{
    public class PersistentWriteOnlyStorage : IPeristentWriteOnlyStorage
    {
        public ICurrencyWriteOnlyRepository CurrencyWriteRepository => throw new NotImplementedException();

        public IMerchantWriteRepository MerchantWriteRepository => throw new NotImplementedException();

        public IPaymentRequestWriteOnlyRepository PaymentRequestWriteRepository => throw new NotImplementedException();
    }
}
