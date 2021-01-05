using System.ComponentModel.DataAnnotations;

namespace PaymentGateway.Persistence.InMemory.DataEntities
{
    public class DataEntity<T>
    {
        [Key]
        public virtual int Id { get; set; }

        //Promise conversion
        public static implicit operator T(DataEntity<T> baseEntity)
        {
            return (T)baseEntity;
        }
    }
}
