using PaymentGateway.Application.DTOs.Payments.PaymentDetails;
using PaymentGateway.Application.Interfaces.Storage.Read;
using PaymentGateway.Domain.Payments;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace PaymentGateway.Application.Services.Payments.PaymentDetails
{
    /// <summary>
    /// One of the two main services. This will query for the payment details.
    /// </summary>
    public class PaymentDetailsService
    {
        private readonly IPersistentReadOnlyStorage _readOnlyStorage;

        public PaymentDetailsService(IPersistentReadOnlyStorage readOnlyStorage)
        {
            _readOnlyStorage = readOnlyStorage;
        }

        public async Task<PaymentDetailsResponseDto> RetrievePaymentDetails(PaymentDetailsRequestDto message)
        {
            PaymentDetailsResponseDto reply;

            PaymentResponse storedResponse = await _readOnlyStorage.PaymentResponseReadOnlyRepository
                                  .GetByMerchantIdAndMerchantUniqueIdAsync(message.MerchantId, message.MerchantUniqueRequestId);

            if (storedResponse == null)
            {
                throw new KeyNotFoundException($"Request with id: {message.MerchantUniqueRequestId} not found.");
            } else
            {
                reply = storedResponse;
            }

            return reply;

        }
    }
}
