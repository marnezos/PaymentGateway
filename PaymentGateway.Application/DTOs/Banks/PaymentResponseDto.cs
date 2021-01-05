using System;

namespace PaymentGateway.Application.DTOs.Banks
{
    public class PaymentResponseDto
    {
        public string GatewayUniqueRequestId { get; set; }
        public Guid ResponseId { get; set; }
        public bool Successful { get; set; }
    }
}
