using System;

namespace PaymentGateway.Application.DTOs.Payments.ProcessPayment
{
    /// <summary>
    /// Application will output the following after processing a payment request.
    /// </summary>
    public class ProcessedPaymentStatusDto
    {
        public Guid ResponseId { get; set; }
        public string RequestId { get; set; }
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
