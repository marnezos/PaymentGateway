using PaymentGateway.Application.DTOs.Banks;
using System.Threading.Tasks;

namespace PaymentGateway.Application.Interfaces.Bank
{
    /// <summary>
    /// Describes an Acquiring bank. So far only one method is expected.
    /// </summary>
    public interface IAcquiringBank
    {
        Task<PaymentResponseDto> ProcessPayment(PaymentRequestDto request);
    }
}
