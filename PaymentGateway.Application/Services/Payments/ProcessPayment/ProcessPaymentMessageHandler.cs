using PaymentGateway.Application.DTOs;
using PaymentGateway.Application.DTOs.Payments.ProcessPayment;
using PaymentGateway.Application.Interfaces.Bank;
using PaymentGateway.Application.Interfaces.Storage.Read;
using PaymentGateway.Application.Interfaces.Storage.Write;
using PaymentGateway.Application.Services.Bank;
using Rebus.Bus;
using Rebus.Handlers;
using System;
using System.Threading.Tasks;

namespace PaymentGateway.Application.Services.Payments.ProcessPayment
{
    /// <summary>
    /// Messages for payment processing will be handled here 
    /// </summary>
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
            ProcessPaymentService service = new ProcessPaymentService(acquiringBank, _readOnlyStorage, _writeOnlyStorage);
            ApplicationMessage<ProcessedPaymentStatusDto> response;

            try
            {
                response = new ApplicationMessage<ProcessedPaymentStatusDto>();
                response.Payload = await service.ProcessPayment(message);
                response.ServiceSuccess = true;
            }
            catch (Exception ex)
            {
                response = new ApplicationMessage<ProcessedPaymentStatusDto>()
                {
                    ServiceSuccess = false,
                    ErrorMessage = ex.Message
                };
            }
            await _bus.Reply(response);
        }
    }
}
