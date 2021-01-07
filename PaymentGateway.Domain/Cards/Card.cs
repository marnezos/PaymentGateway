using PaymentGateway.Domain.Common;
using PaymentGateway.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentGateway.Domain.Cards
{
    /// <summary>
    /// Accompanies a payment and provides info for the fulfilling bank.
    /// </summary>
    /// <remarks>
    /// Self describing entity. A card always accompanies a payment and therefore doesn't have a lifecycle of its own (in our context anyway).
    /// Assumption: card holder information is not required.
    /// Assumption: expiration year is set to 100 from now as maximum.
    /// Assumption: card number contains only digits.
    /// Assumption: expired cards are allowed to be instantiated.
    /// </remarks>
    public class Card : ValueObject
    {
        /// <value>A number from 1 to 12 representing the month of the year that the card expires on</value>
        public byte ExpirationMonth { get; }

        /// <value>A number from 1950 to Now+100 years representing the year that the card expires on</value>
        public ushort ExpirationYear { get; }

        /// <value>Number printed on the card.</value>
        public string Number { get; }

        /// <value>Accepts a small integer 3-4 digits</value>
        public string CVV { get; set; }

        /// <summary>
        /// A card represents a credit or debit card - to be used for processing payments.
        /// </summary>
        /// <param name="number">Card number. Accepts a string of digits with no spaces.</param>
        /// <param name="expirationMonth">Expiration month. Accepts an number from 1 to 12.</param>
        /// <param name="expirationYear">Expiration year. Accepts an number from 1950+ up to 100 years from now.</param>
        /// <param name="cvv">Card's security code. Accepts a string of 3 to 4 digits.</param>
        public Card(string number, byte expirationMonth, ushort expirationYear, string cvv)
        {
            if (number == string.Empty)
            {
                throw new ArgumentOutOfRangeException("number", "Invalid card number. Card number cannot be empty.");
            }
            else if (!number.ContainsOnlyDigits())
            {
                throw new ArgumentOutOfRangeException("number", "Invalid card number. Card numbers should only contain digits.");
            }
            else if (number.Length < 12) //Assumption: Some Maestro's have 12 digits
            {
                throw new ArgumentOutOfRangeException("number", "Invalid card number. Card number length may not be less than 12 characters.");
            }
            else if (number.Length > 30)// Assumption: Maximum number found is 19. I set it at 30 to accomodate next generation cards (?)
            {
                throw new ArgumentOutOfRangeException("number", "Invalid card number. Card number length may not exceed 30 characters.");
            }

            if (expirationMonth < 1 || expirationMonth > 12)
            {
                throw new ArgumentOutOfRangeException("expirationMonth", "Invalid expiration month. Expiration month should be between 1 and 12");
            }

            int maxAllowedExpirationYear = DateTime.Now.AddYears(100).Year;
            if (expirationYear < 1950 || expirationYear > maxAllowedExpirationYear)
            {
                throw new ArgumentOutOfRangeException("expirationYear", $"Invalid expiration year. Expiration year value must be within the range 1 to { maxAllowedExpirationYear }");
            }

            if (cvv.Length < 3 || cvv.Length > 5)
            {
                throw new ArgumentOutOfRangeException("cvv", "Invalid security (cvv) code length. CVV must be between 3 and 5 digits long.");
            }
            else if (!cvv.ContainsOnlyDigits())
            {
                throw new ArgumentOutOfRangeException("cvv", "Invalid security (cvv) contents. CVV must only contain digits.");
            }

            Number = number;
            ExpirationYear = expirationYear;
            ExpirationMonth = expirationMonth;
            CVV = cvv;

        }

        public override string ToString()
        {
            return string.Join('-', Number, ExpirationYear.ToString().PadLeft(4, '0'), ExpirationMonth.ToString().PadLeft(2, '0'), CVV);
        }


        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return ExpirationMonth;
            yield return Number;
            yield return CVV;
        }
    }
}
