using System.ComponentModel.DataAnnotations;

namespace PaymentGateway.Persistence.InMemory.DataEntities.Economics
{
    public class Currency
    {
        [Key]
        public virtual int Id{ get; set; }

        [Required, StringLength(3)]
        public virtual string Name { get; set; }

    }
}
