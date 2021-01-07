using PaymentGateway.Application.DTOs.Banks;
using PaymentGateway.Application.Interfaces.Bank;
using Rebus;
using Rebus.Bus;
using System;
using System.Threading.Tasks;

namespace PaymentGateway.Application.Services.Bank
{
    /// <summary>
    /// Will contact the acquiring bank via the bus
    /// </summary>
    public class AcquiringBankService : IAcquiringBank
    {
        private readonly IBus _bus;
        public AcquiringBankService(IBus bus)
        {
            _bus = bus;
        }
        public async Task<PaymentResponseDto> ProcessPayment(PaymentRequestDto request)
        {
            PaymentResponseDto reply = await _bus.SendRequest<PaymentResponseDto>(request, null, TimeSpan.FromSeconds(10));
            return reply;
        }
    }
}
