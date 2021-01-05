using PaymentGateway.Application.DTOs.Banks;

namespace PaymentGateway.Application.Interfaces.Bank
{
    public interface IAcquiringBank
    {
        PaymentResponseDto ProcessPayment(PaymentRequestDto request);
    }
}
