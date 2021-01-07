
using System;

namespace PaymentGateway.Application.Interfaces.Storage.Read
{
    /// <summary>
    /// Described a read-only repository set (ISR issue?)
    /// </summary>
    public interface IPersistentReadOnlyStorage: IDisposable
    {
        ICurrencyReadOnlyRepository CurrencyReadRepository { get; }
        IMerchantReadOnlyRepository MerchantReadRepository { get; }
        IPaymentRequestReadOnlyRepository PaymentRequestReadRepository { get; }
        IPaymentResponseReadOnlyRepository PaymentResponseReadOnlyRepository { get; }
    }
}
