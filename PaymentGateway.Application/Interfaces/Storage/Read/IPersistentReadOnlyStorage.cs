
using System;

namespace PaymentGateway.Application.Interfaces.Storage.Read
{
    public interface IPersistentReadOnlyStorage: IDisposable
    {
        ICurrencyReadOnlyRepository CurrencyReadRepository { get; }
        IMerchantReadRepository MerchantReadRepository { get; }
        IPaymentRequestReadOnlyRepository PaymentRequestReadRepository { get; }
    }
}
