using PaymentGateway.Domain.Economics;
using System.Threading.Tasks;

namespace PaymentGateway.Application.Interfaces.Storage.Read
{
    public interface ICurrencyReadOnlyRepository:IReadRepository<Currency>
    {
        public Task<Currency> GetByNameAsync(string currencyName);
    }
}
