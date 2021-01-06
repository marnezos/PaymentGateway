using PaymentGateway.Application.Interfaces.Storage.Read;
using PaymentGateway.Domain.Merchants;
using PaymentGateway.Persistence.InMemory.Context;
using System.Linq;

namespace PaymentGateway.Persistence.InMemory.Repositories.Merchants
{
    public class MerchantReadOnlyRepository : BaseReadRepository<Merchant, DataEntities.Merchants.Merchant>, IMerchantReadOnlyRepository
    {

        public MerchantReadOnlyRepository(PaymentGatewayContext context) : base(context) { }

        public override IQueryable<DataEntities.Merchants.Merchant> Aggregate
        {
            get
            {
                return _db.Merchant;
            }
        }

    }
}
