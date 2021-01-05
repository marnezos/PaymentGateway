using PaymentGateway.Domain.Common;
using System;

namespace PaymentGateway.Domain.Payments
{
    /// <summary>
    /// Placeholder entity with no validations
    /// </summary>
    public class PaymentResponse:Entity
    {
        public string GatewayUniqueRequestId { get; set; }
        public Guid ResponseId { get; set; }
        public bool Successful { get; set; }
        public DateTime TimeStamp { get; set; }

        public PaymentResponse() { }
        public PaymentResponse(string gatewayUniqueRequestId, Guid responseId, bool successful, DateTime timestamp)
        {
            GatewayUniqueRequestId = gatewayUniqueRequestId;
            ResponseId = responseId;
            Successful = successful;
            TimeStamp = timestamp;
        }

        public override ValidationResults Validate()
        {
            return new ValidationResults();
        }
    }
}
