using PaymentGateway.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace PaymentGateway.Persistence.InMemory.DataEntities
{
    /// <summary>
    /// Specifies a data entity and requires that a GetDomainObject and a LoadDomainObject methods are implemented (for mapping).
    /// </summary>
    public abstract class DataEntity<T> where T : Entity
    {
        [Key]
        public virtual int Id { get; set; }

        public abstract T GetDomainObject();

        public abstract void LoadDomainObject(T domainObject);
    }
}
