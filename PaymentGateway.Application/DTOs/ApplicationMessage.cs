namespace PaymentGateway.Application.DTOs
{
    public class ApplicationMessage<T>
    {
        public T Payload { get; set; }
        public bool ServiceSuccess { get; set; }
        public string ErrorMessage { get; set; }
    }
}
