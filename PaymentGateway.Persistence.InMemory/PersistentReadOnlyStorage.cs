using PaymentGateway.Application.Interfaces.Storage.Read;

namespace PaymentGateway.Persistence.InMemory
{
    public class PersistentReadOnlyStorage : IPersistentReadOnlyStorage
    {
        public ICurrencyReadRepository CurrencyReadRepository => throw new System.NotImplementedException();

        public IMerchantReadRepository MerchantReadRepository => throw new System.NotImplementedException();

        public IPaymentRequestReadRepository PaymentRequestReadRepository => throw new System.NotImplementedException();
    }
}
