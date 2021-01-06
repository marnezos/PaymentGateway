using PaymentGateway.Application.DTOs.Payments;
using PaymentGateway.Application.Interfaces.Storage.Read;
using PaymentGateway.Application.Interfaces.Storage.Write;
using PaymentGateway.Domain.Merchants;
using Rebus.Bus;
using Rebus.Handlers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.Application.Services.Payments
{
    /// <summary>
    /// Main business functionality
    /// </summary>
    public class ProcessPaymentService: IHandleMessages<PaymentProcessRequestDto>
    {
        private readonly IBus _bus;
        private readonly IPersistentReadOnlyStorage _readOnlyStorage;
        private readonly IPeristentWriteOnlyStorage _writeOnlyStorage;
        public ProcessPaymentService(IBus bus, 
                                     IPersistentReadOnlyStorage readOnlyStorage, 
                                     IPeristentWriteOnlyStorage writeOnlyStorage)
        {
            _bus = bus;
            _readOnlyStorage = readOnlyStorage;
            _writeOnlyStorage = writeOnlyStorage;
        }

        public async Task Handle(PaymentProcessRequestDto message)
        {
            ProcessedPaymentStatusDto reply = new ProcessedPaymentStatusDto();

            //1. Retrieve merchant from storage
            Merchant merchant = await _readOnlyStorage.MerchantReadRepository.GetByIdAsync(message.MerchantId);

            //2. Validate merchant
            if (merchant is null)
            {
                reply.Success = false;
                reply.ErrorMessage = "Merchant Id not found.";
                //ToDo: Log
            }

            //3. Store Request in Storage and get id



            //4. Forward request to the bank

            //5. Store reply 

            //6. Respond to merchant

        }

    }
}
