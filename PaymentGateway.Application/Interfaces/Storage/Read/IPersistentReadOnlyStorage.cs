
using System;

namespace PaymentGateway.Application.Interfaces.Storage.Read
{
    public interface IPersistentReadOnlyStorage: IDisposable
    {
        ICurrencyReadOnlyRepository CurrencyReadRepository { get; }
        IMerchantReadOnlyRepository MerchantReadRepository { get; }
        IPaymentRequestReadOnlyRepository PaymentRequestReadRepository { get; }
        IPaymentResponseReadOnlyRepository PaymentResponseReadOnlyRepository { get; }
    }
}
