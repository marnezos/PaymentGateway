using PaymentGateway.Domain.Common;
using System;
using System.Collections.Generic;
using System.Globalization;


namespace PaymentGateway.Domain.Economics
{
    public class MoneyAmount:ValueObject
    {
        public decimal Amount { get; }
        public Currency Currency { get; }

        /// <summary>
        /// Represents and amount of money that is requested during a payment.
        /// </summary>
        /// <param name="currency">Currency is required.</param>
        /// <param name="amount">Amount may not be negative.</param>
        public MoneyAmount(Currency currency, decimal amount)
        {
            
            if (currency is null)
            {
                throw new ArgumentOutOfRangeException("currency", "Currency is required and may not be null.");
            }

            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException("amount", "Amount value cannot be negative.");
            }

            Amount = amount;
            Currency = currency;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Amount;
            yield return Currency;
        }
        public override string ToString()
        {
            return string.Join('-', Currency.ToString(), Amount.ToString(CultureInfo.InvariantCulture));
        }
    }
}
