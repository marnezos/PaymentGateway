using PaymentGateway.Application.DTOs.Payments;
using PaymentGateway.Application.Interfaces.Bank;
using PaymentGateway.Application.Interfaces.Storage.Read;
using PaymentGateway.Application.Interfaces.Storage.Write;
using PaymentGateway.Domain.Cards;
using PaymentGateway.Domain.Economics;
using PaymentGateway.Domain.Merchants;
using PaymentGateway.Domain.Payments;
using Rebus.Bus;
using Rebus.Handlers;
using System;
using System.Threading.Tasks;

namespace PaymentGateway.Application.Services.Payments
{
    /// <summary>
    /// Main business functionality
    /// </summary>
    public class ProcessPaymentService 
    {
        private readonly IPersistentReadOnlyStorage _readOnlyStorage;
        private readonly IPeristentWriteOnlyStorage _writeOnlyStorage;
        private readonly IAcquiringBank _bank;

        public ProcessPaymentService(IAcquiringBank bank,
                                     IPersistentReadOnlyStorage readOnlyStorage,
                                     IPeristentWriteOnlyStorage writeOnlyStorage)
        {
            _readOnlyStorage = readOnlyStorage;
            _writeOnlyStorage = writeOnlyStorage;
            _bank = bank;
        }

        public async Task<ProcessedPaymentStatusDto> ProcessPayment (PaymentProcessRequestDto message)
        {
            ProcessedPaymentStatusDto reply = new ProcessedPaymentStatusDto();

            //1. Retrieve merchant from storage
            Merchant merchant = await _readOnlyStorage.MerchantReadRepository.GetByIdAsync(message.MerchantId);
            if (merchant is null)
            {
                reply.Success = false;
                reply.ErrorMessage = "Merchant Id not found.";
                //ToDo: Log
            }

            //2. Retrieve currency from storage
            Currency currency = await _readOnlyStorage.CurrencyReadRepository.GetByNameAsync(message.CurrencyIso4217);
            if (merchant is null)
            {
                reply.Success = false;
                reply.ErrorMessage = "Currency not found.";
                //ToDo: Log
            }

            //3. Store Request in Storage and get id
            //Setup
            Card card = new Card(message.CardNumber, message.CardExpirationMonth, message.CardExpirationYear, message.CardCvv);
            MoneyAmount amount = new MoneyAmount(currency, message.Amount);
            PaymentRequest paymentRequest = new PaymentRequest(message.MerchantUniqueRequestId, merchant, card, amount, DateTime.Now);

            //Store
            await _writeOnlyStorage.PaymentRequestWriteRepository.SaveAsync(paymentRequest);

            //Retrieve back to ensure correct storage
            PaymentRequest storedRequest = await _readOnlyStorage.PaymentRequestReadRepository
                                                    .GetByMerchantIdAndMerchantUniqueIdAsync(merchant.Id, message.MerchantUniqueRequestId);

            if (storedRequest is null)
            {
                reply.Success = false;
                reply.ErrorMessage = "Unknown error (5001).";
                //ToDo: Log
            }
            else if (storedRequest.UniqueHash != paymentRequest.UniqueHash)
            {
                reply.Success = false;
                reply.ErrorMessage = "Unknown error (5002).";
                //ToDo: Log
            }
            else
            {
                //4. Forward request to the bank
                DTOs.Banks.PaymentResponseDto bankResponse = await _bank.ProcessPayment(new DTOs.Banks.PaymentRequestDto()
                {

                });


                //5. Store reply 

                //6. Respond to merchant
            }
            return null;
        }

    }
}
