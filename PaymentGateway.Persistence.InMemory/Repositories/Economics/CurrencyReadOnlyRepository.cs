using Microsoft.EntityFrameworkCore;
using PaymentGateway.Application.Interfaces.Storage.Read;
using PaymentGateway.Domain.Economics;
using PaymentGateway.Persistence.InMemory.Context;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGateway.Persistence.InMemory.Repositories.Economics
{
    public class CurrencyReadOnlyRepository : BaseReadRepository<Domain.Economics.Currency, DataEntities.Economics.Currency>, ICurrencyReadOnlyRepository
    {
        public CurrencyReadOnlyRepository(PaymentGatewayContext context) : base(context) { }

        public async Task<Currency> GetByNameAsync(string currencyName)
        {
            return await Aggregate.FirstOrDefaultAsync(c => c.Name == currencyName);
        }

        public override IQueryable<DataEntities.Economics.Currency> Aggregate
        {
            get
            {
                return _db.Currency;
            }
        }
    }
}
