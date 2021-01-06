using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentGateway.Application.DTOs.Payments
{
    public class ProcessedPaymentStatusDto
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
    }
}
