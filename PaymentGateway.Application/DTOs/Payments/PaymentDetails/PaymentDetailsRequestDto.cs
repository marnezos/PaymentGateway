namespace PaymentGateway.Application.DTOs.Payments.PaymentDetails
{
    /// <summary>
    /// Application expects the following in order to provide details for a previously processed payment.
    /// </summary>
    public class PaymentDetailsRequestDto
    {
        public int MerchantId { get; set; }
        public string MerchantUniqueRequestId { get; set; }
    }
}
