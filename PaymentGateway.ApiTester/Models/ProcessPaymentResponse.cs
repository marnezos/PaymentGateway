using System;

namespace PaymentGateway.ApiTester.Models
{
    public class ProcessPaymentResponse
    {
        public Guid ResponseId { get; set; }
        public string MerchantUniqueRequestId { get; set; }
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
        public DateTime Timestamp { get; set; }

    }
}
