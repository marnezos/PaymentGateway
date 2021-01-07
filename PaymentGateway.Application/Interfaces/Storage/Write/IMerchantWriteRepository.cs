using PaymentGateway.Domain.Merchants;

namespace PaymentGateway.Application.Interfaces.Storage.Write
{
    /// <summary>
    /// Described a write-only repository for merchants
    /// </summary>
    public interface IMerchantWriteRepository : IWriteRepository<Merchant>
    {
    }
}
