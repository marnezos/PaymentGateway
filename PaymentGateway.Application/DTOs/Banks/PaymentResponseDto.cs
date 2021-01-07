using PaymentGateway.Domain.Payments;
using System;

namespace PaymentGateway.Application.DTOs.Banks
{
    public class PaymentResponseDto
    {
        public string GatewayUniqueRequestId { get; set; }
        public Guid ResponseId { get; set; }
        public bool Successful { get; set; }
        public DateTime Timestamp { get; set; }

        public static implicit operator PaymentResponse(PaymentResponseDto paymentResponse)
        {
            if (paymentResponse is null) return null;
            return new PaymentResponse()
            {
                ResponseId = paymentResponse.ResponseId,
                Successful = paymentResponse.Successful,
                Timestamp = paymentResponse.Timestamp
            };
        }

    }
}
