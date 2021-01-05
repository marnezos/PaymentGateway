using System.ComponentModel.DataAnnotations;

namespace PaymentGateway.Persistence.InMemory.DataEntities.Merchants
{
    public class Merchant
    {
        [Key]
        public virtual int Id { get; set; }

        [Required, StringLength(50)]
        public virtual string Name { get; set; }

        [Required, StringLength(255)]
        public virtual string Email { get; set; }
    }
}
