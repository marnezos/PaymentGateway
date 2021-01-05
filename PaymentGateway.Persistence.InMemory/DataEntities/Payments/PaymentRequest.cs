using PaymentGateway.Persistence.InMemory.DataEntities.Economics;
using PaymentGateway.Persistence.InMemory.DataEntities.Merchants;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaymentGateway.Persistence.InMemory.DataEntities.Payments
{
    public class PaymentRequest
    {
        [Key]
        public virtual int Id { get; set; }

        [Required]
        public virtual int MerchantId { get; set; }

        [Required]
        public virtual int CurrencyId { get; set; }

        [Required, StringLength(512)]
        public virtual string MerchantUniqueRequestId { get; set; }

        [Required, StringLength(512)]
        public virtual string GatewayUniqueRequestId { get; set; }

        [Required, StringLength(30)]
        public virtual string CardNumber { get; set; }

        [Required]
        public virtual byte CardExpirationMonth { get; set; }

        [Required]
        public virtual ushort CardExpirationYear { get; set; }

        [Required, StringLength(5)]
        public virtual string CardCvv { get; set; }

        [Required]
        public virtual decimal Amount { get; set; }



        [ForeignKey(nameof(MerchantId))]
        public virtual Merchant Merchant { get; set; }

        [ForeignKey(nameof(MerchantId))]
        public virtual Currency Currency { get; set; }

    }
}
