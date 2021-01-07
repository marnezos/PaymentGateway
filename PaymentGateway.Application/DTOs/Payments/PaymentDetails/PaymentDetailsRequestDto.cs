namespace PaymentGateway.Application.DTOs.Payments.PaymentDetails
{
    public class PaymentDetailsRequestDto
    {
        public int MerchantId { get; set; }
        public string MerchantUniqueRequestId { get; set; }
    }
}
