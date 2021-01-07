using PaymentGateway.Domain.Economics;

namespace PaymentGateway.Application.Interfaces.Storage.Write
{
    /// <summary>
    /// Described a write-only repository for currencies
    /// </summary>
    public interface ICurrencyWriteOnlyRepository : IWriteRepository<Currency>
    {
    }
}
