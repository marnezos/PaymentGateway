using System;

namespace PaymentGateway.Application.Interfaces.Storage.Write
{
    public interface IPeristentWriteOnlyStorage: IDisposable
    {
        ICurrencyWriteOnlyRepository CurrencyWriteRepository { get; }
        IMerchantWriteRepository MerchantWriteRepository { get; }
        IPaymentRequestWriteOnlyRepository PaymentRequestWriteRepository { get; }
    }
}
