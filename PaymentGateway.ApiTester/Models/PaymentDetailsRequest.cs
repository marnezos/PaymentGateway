using System.ComponentModel.DataAnnotations;

namespace PaymentGateway.ApiTester.Models
{
    public class PaymentDetailsRequest
    {
        [Required, MaxLength(512)]
        public string MerchantUniqueRequestId { get; set; }

    }
}
