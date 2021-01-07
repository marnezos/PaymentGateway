
using PaymentGateway.Application.DTOs.Payments.PaymentDetails;
using System.ComponentModel.DataAnnotations;

namespace PaymentGateway.API.ViewModels.Payments
{
    public class PaymentDetailsRequest
    {
        [Required, MaxLength(512)]
        public string MerchantUniqueRequestId { get; set; }

        public static implicit operator PaymentDetailsRequestDto(PaymentDetailsRequest request)
        {
            if (request is null) return null;
            return new PaymentDetailsRequestDto()
            {
                MerchantUniqueRequestId = request.MerchantUniqueRequestId
            };
        }
    }
}
