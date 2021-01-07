using System;

namespace PaymentGateway.Application.Interfaces.Storage.Write
{
    /// <summary>
    /// Described a write-only repository set (ISR issue?)
    /// </summary>
    public interface IPeristentWriteOnlyStorage: IDisposable
    {
        ICurrencyWriteOnlyRepository CurrencyWriteRepository { get; }
        IMerchantWriteRepository MerchantWriteRepository { get; }
        IPaymentRequestWriteOnlyRepository PaymentRequestWriteRepository { get; }
        IPaymentResponseWriteOnlyRepository PaymentResponseWriteRepository { get; }
    }
}
