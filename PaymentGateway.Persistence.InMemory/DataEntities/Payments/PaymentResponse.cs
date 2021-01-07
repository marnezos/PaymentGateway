using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaymentGateway.Persistence.InMemory.DataEntities.Payments
{
    /// <summary>
    /// DB Model for Paymen Responses
    /// </summary>
    public class PaymentResponse : DataEntity<Domain.Payments.PaymentResponse>
    {

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

        public static implicit operator Domain.Payments.PaymentResponse(PaymentResponse paymentResponse)
        {
            if (paymentResponse is null) return null;
            return new Domain.Payments.PaymentResponse(paymentResponse.PaymentRequest, 
                                                       paymentResponse.ResponseId, 
                                                       paymentResponse.Successful, 
                                                       paymentResponse.TimeStamp);
        }

        public override Domain.Payments.PaymentResponse GetDomainObject()
        {
            return this;
        }

        public override void LoadDomainObject(Domain.Payments.PaymentResponse domainObject)
        {
            PaymentRequest = domainObject.PaymentRequest;
            ResponseId = domainObject.ResponseId;
            Successful = domainObject.Successful;
            TimeStamp = domainObject.Timestamp;
        }
    }
}
