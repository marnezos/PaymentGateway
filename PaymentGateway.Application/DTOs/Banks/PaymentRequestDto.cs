namespace PaymentGateway.Application.DTOs.Banks
{
    public class PaymentRequestDto
    {
        public string GatewayUniqueRequestId { get; set; }
        public string CurrencyISO4217 { get; set; }
        public decimal Amount { get; set; }
        public string CardNumber { get; set; }
        public int CardExpirationMonth { get; set; }
        public int CardExpirationYear { get; set; }
        public int CardCvv { get; set; }
        public string MerchantName { get; set; }
        public string MerchantEmail { get; set; }
    }
}
