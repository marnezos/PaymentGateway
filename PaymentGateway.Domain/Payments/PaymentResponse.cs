using PaymentGateway.Domain.Common;
using System;

namespace PaymentGateway.Domain.Payments
{
    /// <summary>
    /// Placeholder entity with no validations. To be implemented when a real bank is integrated.
    /// </summary>
    public class PaymentResponse:Entity
    {
        public PaymentRequest PaymentRequest { get; set; }
        public Guid ResponseId { get; set; }
        public bool Successful { get; set; }
        public DateTime Timestamp { get; set; }

        public PaymentResponse() { }
        public PaymentResponse(PaymentRequest paymentRequest, Guid responseId, bool successful, DateTime timestamp)
        {
            PaymentRequest = paymentRequest;
            ResponseId = responseId;
            Successful = successful;
            Timestamp = timestamp;
        }

        public override ValidationResults Validate()
        {
            return new ValidationResults();
        }
    }
}
