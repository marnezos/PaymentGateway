namespace PaymentGateway.Application.DTOs
{
    /// <summary>
    /// Message wrapper for Rebus messages
    /// </summary>
    public class ApplicationMessage<T>
    {
        public T Payload { get; set; }
        public bool ServiceSuccess { get; set; }
        public string ErrorMessage { get; set; }
    }
}
