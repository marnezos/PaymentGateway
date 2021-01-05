using PaymentGateway.Domain.Cards;
using PaymentGateway.Domain.Common;
using PaymentGateway.Domain.Economics;
using PaymentGateway.Domain.Merchants;
using System.Security.Cryptography;
using System;
using System.Text;
using PaymentGateway.Domain.Helpers;

namespace PaymentGateway.Domain.Payments
{
    /// <summary>
    /// The main transactional entity for this context. An aggregate root that holds all necessary info for fulfilling a payment request.
    /// </summary>
    public class PaymentRequest : Entity
    {
        public MoneyAmount Amount { get; set; }
        public Card Card { get; set; }
        public Merchant Merchant { get; set; }

        /// <summary>
        /// Maximum length 512.
        /// </summary>
        public string MerchantUniqueRequestId { get; set; }
        public DateTime Timestamp { get; set; }

        public PaymentRequest() { }

        /// <summary>
        /// Instantiates a new payment request object
        /// </summary>
        /// <param name="merchantUniqueRequestId">Any string that helps identify a payment request uniquely. Maximum length 512.</param>
        /// <param name="merchant">The merchant requesting the payment.</param>
        /// <param name="card">Card to charge.</param>
        /// <param name="amount">Amount to charge.</param>
        /// <param name="timestamp">Request timestamp.</param>
        public PaymentRequest(string merchantUniqueRequestId, Merchant merchant, Card card, MoneyAmount amount, DateTime timestamp)
        {
            MerchantUniqueRequestId = merchantUniqueRequestId;
            Merchant = merchant;
            Card = card;
            Amount = amount;
            Timestamp = timestamp;
        }

        public string UniqueHash
        {
            get
            {
                string hashable = string.Join('-', Id.ToString(),
                                                    MerchantUniqueRequestId,
                                                    Merchant.ToString(),
                                                    Card.ToString(),
                                                    Amount.ToString(),
                                                    Timestamp.ToString());

                using HashAlgorithm algorithm = SHA256.Create();
                return algorithm.ComputeHash(Encoding.UTF8.GetBytes(hashable.ToString())).ToHexString();
            }
        }

        public override ValidationResults Validate()
        {
            ValidationResults validationResults = new ValidationResults();

            if (Amount is null)
            {
                validationResults.AddValidationError("Money amount is empty.");
            }

            if (Card is null)
            {
                validationResults.AddValidationError("Card is empty. A payment request requires a card.");
            }

            if (Merchant is null)
            {
                validationResults.AddValidationError("Merchant is empty. A payment request requires a requesting merchant.");
            }
            else if (!Merchant.IsValid)
            {
                validationResults.AddValidationError("Merchant is not valid. A payment request requires a valid merchant.");
            }

            if (string.IsNullOrEmpty(MerchantUniqueRequestId))
            {
                validationResults.AddValidationError("MerchantUniqueRequestId is empty. A payment request requires a merchant unique request Id.");
            }
            else if (MerchantUniqueRequestId.Length > 512)
            {
                validationResults.AddValidationError("MerchantUniqueRequestId may not be longer than 512 chars.");
            }

            return validationResults;
        }
    }
}
