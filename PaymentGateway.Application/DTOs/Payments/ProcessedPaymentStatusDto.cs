using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentGateway.Application.DTOs.Payments
{
    /// <summary>
    /// Message format for the response to the merchant
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
