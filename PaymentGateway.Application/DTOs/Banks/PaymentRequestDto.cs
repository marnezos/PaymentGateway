using PaymentGateway.Domain.Payments;

namespace PaymentGateway.Application.DTOs.Banks
{
    /// <summary>
    /// Application will fill and forward the following model to the acquiring bank to process a payment.
    /// </summary>
    public class PaymentRequestDto
    {
        public string GatewayUniqueRequestId { get; set; }
        public string CurrencyISO4217 { get; set; }
        public decimal Amount { get; set; }
        public string CardNumber { get; set; }
        public int CardExpirationMonth { get; set; }
        public int CardExpirationYear { get; set; }
        public string CardCvv { get; set; }
        public string MerchantName { get; set; }
        public string MerchantEmail { get; set; }

        public static implicit operator PaymentRequestDto(PaymentRequest paymentRequest)
        {
            if (paymentRequest is null || !paymentRequest.IsValid) return null;
            return new PaymentRequestDto()
            {
                GatewayUniqueRequestId = paymentRequest.UniqueHash,
                CurrencyISO4217 = paymentRequest.Amount.Currency.Name,
                Amount = paymentRequest.Amount.Amount,
                CardNumber = paymentRequest.Card.Number,
                CardExpirationMonth = paymentRequest.Card.ExpirationMonth,
                CardExpirationYear = paymentRequest.Card.ExpirationYear,
                CardCvv = paymentRequest.Card.CVV,
                MerchantEmail = paymentRequest.Merchant.Email,
                MerchantName = paymentRequest.Merchant.Name
            };
        }

    }
}
