using System.ComponentModel.DataAnnotations;

namespace PaymentGateway.Persistence.InMemory.DataEntities
{
    public abstract class DataEntity<T>
    {
        [Key]
        public virtual int Id { get; set; }

        public abstract T GetDomainObject();
    }
}
