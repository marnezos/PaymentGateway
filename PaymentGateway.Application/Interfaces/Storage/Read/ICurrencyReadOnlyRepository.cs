using PaymentGateway.Domain.Economics;
using System.Threading.Tasks;

namespace PaymentGateway.Application.Interfaces.Storage.Read
{
    /// <summary>
    /// Described a read-only repository for currencies
    /// </summary>
    public interface ICurrencyReadOnlyRepository:IReadRepository<Currency>
    {
        public Task<Currency> GetByNameAsync(string currencyName);
    }
}
