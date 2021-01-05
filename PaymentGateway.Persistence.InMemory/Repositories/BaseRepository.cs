using Microsoft.EntityFrameworkCore;
using PaymentGateway.Application.Interfaces.Storage.Read;
using PaymentGateway.Domain.Common;
using PaymentGateway.Persistence.InMemory.Context;
using PaymentGateway.Persistence.InMemory.DataEntities;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGateway.Persistence.InMemory.Repositories
{
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
            return (T)await Aggregate.FirstAsync(a => a.Id == id);
        }

        public abstract IQueryable<DBEntity> Aggregate { get; }

    }
}
