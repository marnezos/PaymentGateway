using PaymentGateway.Application.DTOs.Payments;
using PaymentGateway.Application.Interfaces.Bank;
using PaymentGateway.Application.Interfaces.Storage.Read;
using PaymentGateway.Application.Interfaces.Storage.Write;
using PaymentGateway.Application.Services.Bank;
using Rebus.Bus;
using Rebus.Handlers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.Application.Services.Payments
{
    public class ProcessPaymentMessageHandler : IHandleMessages<PaymentProcessRequestDto>
    {
        private readonly IBus _bus;
        private readonly IPersistentReadOnlyStorage _readOnlyStorage;
        private readonly IPeristentWriteOnlyStorage _writeOnlyStorage;
        public ProcessPaymentMessageHandler(IBus bus,
                                            IPersistentReadOnlyStorage readOnlyStorage, 
                                            IPeristentWriteOnlyStorage writeOnlyStorage)
        {
            _bus = bus;
            _readOnlyStorage = readOnlyStorage;
            _writeOnlyStorage = writeOnlyStorage;
        }

        public async Task Handle(PaymentProcessRequestDto message)
        {
            IAcquiringBank acquiringBank = new AcquiringBankService(_bus);
            ProcessPaymentService service = new ProcessPaymentService(acquiringBank,_readOnlyStorage, _writeOnlyStorage);
            await _bus.Reply(await service.ProcessPayment(message)); 
        }
    }
}
