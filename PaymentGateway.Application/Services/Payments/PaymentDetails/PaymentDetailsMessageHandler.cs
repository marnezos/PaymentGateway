using PaymentGateway.Application.DTOs;
using PaymentGateway.Application.DTOs.Payments.PaymentDetails;
using PaymentGateway.Application.Interfaces.Storage.Read;
using Rebus.Bus;
using Rebus.Handlers;
using System;
using System.Threading.Tasks;

namespace PaymentGateway.Application.Services.Payments.PaymentDetails
{
    /// <summary>
    /// Messages for payment details will be handled here 
    /// </summary>
    public class PaymentDetailsMessageHandler : IHandleMessages<PaymentDetailsRequestDto>
    {
        private readonly IBus _bus;
        private readonly IPersistentReadOnlyStorage _readOnlyStorage;

        public PaymentDetailsMessageHandler(IBus bus,
                                            IPersistentReadOnlyStorage readOnlyStorage)
        {
            _bus = bus;
            _readOnlyStorage = readOnlyStorage;
        }

        public async Task Handle(PaymentDetailsRequestDto message)
        {
            PaymentDetailsService service = new PaymentDetailsService(_readOnlyStorage);
            ApplicationMessage<PaymentDetailsResponseDto> response;
            try
            {
                response = new ApplicationMessage<PaymentDetailsResponseDto>();
                response.Payload = await service.RetrievePaymentDetails(message);
                response.ServiceSuccess = true;
            }
            catch (Exception ex)
            {
                response = new ApplicationMessage<PaymentDetailsResponseDto>()
                {
                    ServiceSuccess = false,
                    ErrorMessage = ex.Message
                };
            }
            await _bus.Reply(response);
        }
    }
}
