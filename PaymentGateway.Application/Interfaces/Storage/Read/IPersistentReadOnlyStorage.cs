
namespace PaymentGateway.Application.Interfaces.Storage.Read
{
    public interface IPersistentReadOnlyStorage
    {
        ICurrencyReadRepository CurrencyReadRepository { get; }
        IMerchantReadRepository MerchantReadRepository { get; }
        IPaymentRequestReadRepository PaymentRequestReadRepository { get; }
    }
}
