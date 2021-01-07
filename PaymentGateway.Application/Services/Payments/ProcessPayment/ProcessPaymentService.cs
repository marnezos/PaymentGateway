using PaymentGateway.Application.DTOs.Payments.ProcessPayment;
using PaymentGateway.Application.Interfaces.Bank;
using PaymentGateway.Application.Interfaces.Storage.Read;
using PaymentGateway.Application.Interfaces.Storage.Write;
using PaymentGateway.Domain.Cards;
using PaymentGateway.Domain.Economics;
using PaymentGateway.Domain.Merchants;
using PaymentGateway.Domain.Payments;
using System;
using System.Threading.Tasks;

namespace PaymentGateway.Application.Services.Payments.ProcessPayment
{
    /// <summary>
    /// Main business functionality
    /// Assumption: Card details are stored in their entirety. PCI prohbits this. Are payment Gateways different?
    /// ToDo: Refactor
    /// ToDo: Log
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

        public async Task<ProcessedPaymentStatusDto> ProcessPayment(PaymentProcessRequestDto message)
        {
            ProcessedPaymentStatusDto reply = new ProcessedPaymentStatusDto();
            reply.RequestId = message.MerchantUniqueRequestId;

            PaymentServiceBillOfMaterials billOfMaterials = new PaymentServiceBillOfMaterials(message);

            PaymentServiceSequenceStep[] steps = new[]
            {
                new PaymentServiceSequenceStep(){ExecuteStep = GetMerchant, ErrorMessage = "Merchant identity not found."},
                new PaymentServiceSequenceStep(){ExecuteStep = EnsureRequestUnique, ErrorMessage = "Request Id must be unique."},
                new PaymentServiceSequenceStep(){ExecuteStep = GetCurrency, ErrorMessage = "Invalid currency or currency not supported."},
                new PaymentServiceSequenceStep(){ExecuteStep = GetCard, ErrorMessage = "Invalid card details."},
                new PaymentServiceSequenceStep(){ExecuteStep = GetMoneyAmount, ErrorMessage = "Invalid amount."},
                new PaymentServiceSequenceStep(){ExecuteStep = GetPaymentRequest, ErrorMessage = "Internal payment error."},
                new PaymentServiceSequenceStep(){ExecuteStep = StorePaymentRequest, ErrorMessage = "Error while storing request."},
                new PaymentServiceSequenceStep(){ExecuteStep = GetStoredPaymentRequest, ErrorMessage = "Error while validating stored requesst."},
                new PaymentServiceSequenceStep(){ExecuteStep = SendToBankToProcessPayment, ErrorMessage = "Error while dispatching request to the acquiring bank."},
                new PaymentServiceSequenceStep(){ExecuteStep = StorePaymentResponse, ErrorMessage = "Error while storing response. Contact support."},
            };

            foreach (PaymentServiceSequenceStep step in steps)
            {
                if (!await step.ExecuteStep(billOfMaterials))
                {
                    throw new InvalidOperationException(step.ErrorMessage);
                }
            }

            reply.Success = billOfMaterials.PaymentResponse.Successful;
            reply.ResponseId = billOfMaterials.PaymentResponse.ResponseId;
            reply.Timestamp = billOfMaterials.PaymentResponse.Timestamp;
            return reply;
        }

        private async Task<bool> GetMerchant(PaymentServiceBillOfMaterials billOfMaterials)
        {
            try 
            {
                billOfMaterials.Merchant = await _readOnlyStorage.MerchantReadRepository.GetByIdAsync(billOfMaterials.Request.MerchantId);
                return (billOfMaterials.Merchant != null && billOfMaterials.Merchant.IsValid);
            }
            catch { return false; }
        }

        private async Task<bool> GetCurrency(PaymentServiceBillOfMaterials billOfMaterials)
        {
            try
            {
                billOfMaterials.Currency = await _readOnlyStorage.CurrencyReadRepository.GetByNameAsync(billOfMaterials.Request.CurrencyIso4217);
                return (billOfMaterials.Currency != null && billOfMaterials.Currency.IsValid);
            }
            catch { return false; }
        }

        private async Task<bool> EnsureRequestUnique(PaymentServiceBillOfMaterials billOfMaterials)
        {
            try
            {
                PaymentRequest existingRequest = await _readOnlyStorage.PaymentRequestReadRepository.GetByMerchantIdAndMerchantUniqueIdAsync(billOfMaterials.Merchant.Id,
                                                                                                                    billOfMaterials.Request.MerchantUniqueRequestId);

                return (existingRequest is null);
            }
            catch { return false; }
        }

        private async Task<bool> GetCard(PaymentServiceBillOfMaterials billOfMaterials)
        {
            try
            {
                billOfMaterials.Card = new Card(billOfMaterials.Request.CardNumber,
                                                billOfMaterials.Request.CardExpirationMonth,
                                                billOfMaterials.Request.CardExpirationYear,
                                                billOfMaterials.Request.CardCvv);

                return (billOfMaterials.Card != null);
            }
            catch { return false; }
        }

        private async Task<bool> GetMoneyAmount(PaymentServiceBillOfMaterials billOfMaterials)
        {
            try
            {
                billOfMaterials.MoneyAmount = new MoneyAmount(billOfMaterials.Currency, billOfMaterials.Request.Amount);
                return (billOfMaterials.MoneyAmount != null);
            }
            catch { return false; }
        }

        private async Task<bool> GetPaymentRequest(PaymentServiceBillOfMaterials billOfMaterials)
        {
            try
            {
                billOfMaterials.PaymentRequest = new PaymentRequest(0,
                                                                    billOfMaterials.Request.MerchantUniqueRequestId,
                                                                    billOfMaterials.Merchant,
                                                                    billOfMaterials.Card,
                                                                    billOfMaterials.MoneyAmount,
                                                                    DateTime.Now);
                return (billOfMaterials.PaymentRequest != null && billOfMaterials.PaymentRequest.IsValid);
            }
            catch { return false; }
        }

        private async Task<bool> StorePaymentRequest(PaymentServiceBillOfMaterials billOfMaterials)
        {
            try
            {
                await _writeOnlyStorage.PaymentRequestWriteRepository.SaveAsync(billOfMaterials.PaymentRequest);
                return true;
            }
            catch { return false; }
        }

        private async Task<bool> GetStoredPaymentRequest(PaymentServiceBillOfMaterials billOfMaterials)
        {
            try
            {
                billOfMaterials.StoredRequest = await _readOnlyStorage.PaymentRequestReadRepository
                                                        .GetByMerchantIdAndMerchantUniqueIdAsync(billOfMaterials.Merchant.Id, billOfMaterials.Request.MerchantUniqueRequestId);

                return (billOfMaterials.StoredRequest != null && (billOfMaterials.StoredRequest.UniqueHash == billOfMaterials.PaymentRequest.UniqueHash));
            }
            catch { return false; }

        }

        private async Task<bool> SendToBankToProcessPayment(PaymentServiceBillOfMaterials billOfMaterials)
        {
            try
            {
                billOfMaterials.PaymentResponse = await _bank.ProcessPayment(billOfMaterials.StoredRequest);
                return (billOfMaterials.PaymentResponse != null);
            }
            catch { return false; }
        }

        private async Task<bool> StorePaymentResponse(PaymentServiceBillOfMaterials billOfMaterials)
        {
            PaymentResponse paymentResponse = billOfMaterials.PaymentResponse;
            paymentResponse.PaymentRequest = billOfMaterials.StoredRequest;

            try
            {
                await _writeOnlyStorage.PaymentResponseWriteRepository
                                            .SaveAsync(paymentResponse);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

    }

    class PaymentServiceBillOfMaterials
    {
        public PaymentServiceBillOfMaterials(PaymentProcessRequestDto request)
        {
            Request = request;
        }
        public PaymentProcessRequestDto Request { get; }
        public Merchant Merchant { get; set; }
        public Currency Currency { get; set; }
        public Card Card { get; set; }
        public MoneyAmount MoneyAmount { get; set; }
        public PaymentRequest PaymentRequest { get; set; }
        public PaymentRequest StoredRequest { get; set; }
        public PaymentResponse PaymentResponse { get; set; }

    }

    class PaymentServiceSequenceStep
    {
        public Func<PaymentServiceBillOfMaterials, Task<bool>> ExecuteStep { get; set; }
        public string ErrorMessage { get; set; }
    }

}
