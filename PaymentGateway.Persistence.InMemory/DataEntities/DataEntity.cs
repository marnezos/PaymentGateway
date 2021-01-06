using PaymentGateway.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace PaymentGateway.Persistence.InMemory.DataEntities
{
    public abstract class DataEntity<T> where T : Entity
    {
        [Key]
        public virtual int Id { get; set; }

        public abstract T GetDomainObject();

        public abstract void LoadDomainObject(T domainObject);
    }
}
