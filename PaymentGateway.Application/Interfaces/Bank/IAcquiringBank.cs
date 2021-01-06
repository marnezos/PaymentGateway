using PaymentGateway.Application.DTOs.Banks;
using System.Threading.Tasks;

namespace PaymentGateway.Application.Interfaces.Bank
{
    public interface IAcquiringBank
    {
        Task<PaymentResponseDto> ProcessPayment(PaymentRequestDto request);
    }
}
