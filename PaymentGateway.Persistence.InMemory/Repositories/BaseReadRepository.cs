using Microsoft.EntityFrameworkCore;
using PaymentGateway.Application.Interfaces.Storage.Read;
using PaymentGateway.Domain.Common;
using PaymentGateway.Persistence.InMemory.Context;
using PaymentGateway.Persistence.InMemory.DataEntities;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGateway.Persistence.InMemory.Repositories
{

    /// <summary>
    /// Provided basic read functionality (for DRY reasons)
    /// </summary>
    public abstract class BaseReadRepository<T, DBEntity> : IReadRepository<T> 
                                                            where T : Entity
                                                            where DBEntity : DataEntity<T>
    {
        protected readonly PaymentGatewayContext _db;

        protected BaseReadRepository(PaymentGatewayContext context)
        {
            _db = context;
        }

        public  async Task<T> GetByIdAsync(int id)
        {
            var entity = await Aggregate.FirstOrDefaultAsync(a => a.Id == id);
            return entity.GetDomainObject();
        }

        public abstract IQueryable<DBEntity> Aggregate { get; }

    }
}
