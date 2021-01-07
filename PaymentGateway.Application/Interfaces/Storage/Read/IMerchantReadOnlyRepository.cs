using PaymentGateway.Domain.Merchants;

namespace PaymentGateway.Application.Interfaces.Storage.Read
{
    /// <summary>
    /// Described a read-only repository for merchants
    /// </summary>
    public interface IMerchantReadOnlyRepository : IReadRepository<Merchant>
    {
    }
}
