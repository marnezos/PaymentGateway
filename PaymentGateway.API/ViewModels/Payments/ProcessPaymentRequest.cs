using PaymentGateway.Application.DTOs.Payments;
using System;
using System.ComponentModel.DataAnnotations;

namespace PaymentGateway.API.ViewModels.Payments
{
    /// <summary>
    /// ViewModel for the API. 
    /// Merchants will supply this to request a payment processing from us
    /// </summary>
    public class ProcessPaymentRequest
    {
        [Required, MaxLength(512)]
        public string MerchantUniqueRequestId { get; set; }

        [Required, RegularExpression("^\\d+$"), MaxLength(30)]
        public string  CardNumber { get; set; }

        [Required, Range(1,12)]
        public byte  CardExpirationMonth { get; set; }

        [Required, Range(1950, ushort.MaxValue)]
        public ushort  CardExpirationYear { get; set; }

        [Required, RegularExpression("^\\d+$"), MaxLength(5)]
        public string  CardCvv { get; set; }

        [Required, RegularExpression("^[a-zA-Z]+$"),MinLength(3), MaxLength(3)]
        public string CurrencyIso4217 { get; set; }

        [Required, Range(0.01, 99999.99)]
        public decimal Amount { get; set; }


        public static implicit operator PaymentProcessRequestDto(ProcessPaymentRequest request)
        {
            return new PaymentProcessRequestDto()
            {
                CurrencyIso4217 = request.CurrencyIso4217,
                Amount = request.Amount,
                CardCvv = request.CardCvv,
                CardExpirationMonth = request.CardExpirationMonth,
                CardExpirationYear = request.CardExpirationYear,
                CardNumber = request.CardNumber
            };
        }

    }
}
