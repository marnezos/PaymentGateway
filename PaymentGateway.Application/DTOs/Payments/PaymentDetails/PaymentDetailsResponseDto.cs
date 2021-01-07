using PaymentGateway.Domain.Helpers;
using PaymentGateway.Domain.Payments;
using System;

namespace PaymentGateway.Application.DTOs.Payments.PaymentDetails
{
    public class PaymentDetailsResponseDto
    {
        public string MerchantUniqueRequestId { get; set; }
        public string CardNumber { get; set; }
        public byte CardExpirationMonth { get; set; }
        public ushort CardExpirationYear { get; set; }
        public string CardCvv { get; set; }
        public string CurrencyIso4217 { get; set; }
        public decimal Amount { get; set; }
        public DateTime RequestTimestamp { get; set; }

        public DateTime ResponseTimestamp { get; set; }
        public Guid ResponseId { get; set; }
        public bool Success { get; set; }

        public static implicit operator PaymentDetailsResponseDto(PaymentResponse paymentResponse)
        {
            if (paymentResponse is null) return null;
            return new PaymentDetailsResponseDto()
            {
                Amount = paymentResponse.PaymentRequest.Amount.Amount,
                CardCvv = "***",
                CardExpirationMonth = paymentResponse.PaymentRequest.Card.ExpirationMonth,
                CardExpirationYear = paymentResponse.PaymentRequest.Card.ExpirationYear,
                CardNumber = paymentResponse.PaymentRequest.Card.Number.Obfuscate(4),
                CurrencyIso4217 = paymentResponse.PaymentRequest.Amount.Currency.Name,
                MerchantUniqueRequestId = paymentResponse.PaymentRequest.MerchantUniqueRequestId,
                RequestTimestamp = paymentResponse.PaymentRequest.Timestamp,
                ResponseId = paymentResponse.ResponseId,
                Success = paymentResponse.Successful,
                ResponseTimestamp = paymentResponse.Timestamp
            };
        }

    }
}
