namespace PaymentGateway.Application.Interfaces.Storage.Write
{
    public interface IPeristentWriteOnlyStorage
    {
        ICurrencyWriteRepository CurrencyWriteRepository { get; }
        IMerchantWriteRepository MerchantWriteRepository { get; }
        IPaymentRequestWriteRepository PaymentRequestWriteRepository { get; }
    }
}
