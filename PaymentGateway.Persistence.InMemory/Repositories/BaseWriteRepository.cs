using Microsoft.EntityFrameworkCore;
using PaymentGateway.Application.Interfaces.Storage.Write;
using PaymentGateway.Domain.Common;
using PaymentGateway.Persistence.InMemory.Context;
using PaymentGateway.Persistence.InMemory.DataEntities;
using System.Threading.Tasks;

namespace PaymentGateway.Persistence.InMemory.Repositories
{
    public abstract class BaseWriteRepository<T, DBEntity> : IWriteRepository<T> 
                                                            where T : Entity
                                                            where DBEntity : DataEntity<T>, new()
    {
        protected readonly PaymentGatewayContext _db;

        protected BaseWriteRepository(PaymentGatewayContext context)
        {
            _db = context;
        }

        public async Task SaveAsync(T entity)
        {
            DBEntity dbEntity = new DBEntity();
            dbEntity.LoadDomainObject(entity);

            _db.Entry(dbEntity).State = entity.Id == 0 ?
                               EntityState.Added :
                               EntityState.Modified;
            await _db.SaveChangesAsync();
        }


    }
}
