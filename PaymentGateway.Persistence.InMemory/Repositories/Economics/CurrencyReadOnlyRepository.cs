using PaymentGateway.Application.Interfaces.Storage.Read;
using PaymentGateway.Persistence.InMemory.Context;
using System.Linq;

namespace PaymentGateway.Persistence.InMemory.Repositories.Economics
{
    public class CurrencyReadOnlyRepository : BaseReadRepository<Domain.Economics.Currency, DataEntities.Economics.Currency>, ICurrencyReadOnlyRepository
    {
        public CurrencyReadOnlyRepository(PaymentGatewayContext context) : base(context) { }
        public override IQueryable<DataEntities.Economics.Currency> Aggregate
        {
            get
            {
                return _db.Currency;
            }
        }
    }
}
