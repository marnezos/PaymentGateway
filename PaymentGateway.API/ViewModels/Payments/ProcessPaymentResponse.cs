using PaymentGateway.Application.DTOs.Payments.ProcessPayment;
using System;

namespace PaymentGateway.API.ViewModels.Payments
{
    public class ProcessPaymentResponse
    {
        public Guid ResponseId { get; set; }
        public string MerchantUniqueRequestId { get; set; }
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
        public DateTime Timestamp { get; set; }

        public static implicit operator ProcessPaymentResponse(ProcessedPaymentStatusDto response)
        {
            if (response is null) return null;
            return new ProcessPaymentResponse()
            {
                ErrorMessage = response.ErrorMessage,
                MerchantUniqueRequestId = response.RequestId,
                ResponseId = response.ResponseId,
                Success = response.Success,
                Timestamp = response.Timestamp
            };
        }

    }
}
