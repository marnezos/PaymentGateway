using System;

namespace PaymentGateway.API.ViewModels.Payments
{
    public class PaymentDetailsResponse
    {
        public string MerchantUniqueRequestId { get; set; }
        public string CardNumber { get; set; }
        public byte CardExpirationMonth { get; set; }
        public ushort CardExpirationYear { get; set; }
        public string CardCvv { get; set; }
        public string CurrencyIso4217 { get; set; }
        public decimal Amount { get; set; }
        public Guid ResponseId { get; set; }
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
        public DateTime Timestamp { get; set; }

    }
}
