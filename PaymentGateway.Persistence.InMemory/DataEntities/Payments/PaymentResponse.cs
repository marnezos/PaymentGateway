using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaymentGateway.Persistence.InMemory.DataEntities.Payments
{
    public class PaymentResponse
    {
        [Key]
        public virtual int Id { get; set; }

        [Required]
        public int PaymentRequestId { get; set; }

        [ForeignKey(nameof(PaymentRequestId))]
        public virtual PaymentRequest PaymentRequest { get; set; }

        [Required]
        public virtual Guid ResponseId { get; set; }

        [Required]
        public virtual bool Successful { get; set; }

        [Required]
        public virtual DateTime TimeStamp { get; set; }

    }
}
