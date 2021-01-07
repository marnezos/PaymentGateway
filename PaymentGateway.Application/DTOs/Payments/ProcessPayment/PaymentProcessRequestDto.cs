
namespace PaymentGateway.Application.DTOs.Payments.ProcessPayment
{
    /// <summary>
    /// Application requires the following to process a payment.
    /// </summary>
    public class PaymentProcessRequestDto
    {
        public int MerchantId { get; set; }
        public string MerchantUniqueRequestId { get; set; }
        public string CardNumber { get; set; }
        public byte CardExpirationMonth { get; set; }
        public ushort CardExpirationYear { get; set; }
        public string CardCvv { get; set; }
        public string CurrencyIso4217 { get; set; }
        public decimal Amount { get; set; }
    }
}
