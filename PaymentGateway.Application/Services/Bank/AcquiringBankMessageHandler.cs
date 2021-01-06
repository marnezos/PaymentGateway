using PaymentGateway.Application.DTOs.Banks;
using PaymentGateway.Application.Interfaces.Bank;
using Rebus.Bus;
using Rebus.Handlers;
using System.Threading.Tasks;

namespace PaymentGateway.Application.Services.Bank
{
    public class AcquiringBankMessageHandler : IHandleMessages<PaymentRequestDto>
    {
        private readonly IBus _bus;
        public AcquiringBankMessageHandler(IBus bus)
        {
            _bus = bus;
        }
        public async Task Handle(PaymentRequestDto message)
        {
            IAcquiringBank service = new FakeBank();
            await _bus.Reply(await service.ProcessPayment(message));
        }

    }
}
