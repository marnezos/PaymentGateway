using PaymentGateway.Domain.Common;
using PaymentGateway.Domain.Helpers;

namespace PaymentGateway.Domain.Economics
{
    /// <summary>
    /// Currency object
    /// </summary>
    public class Currency : Entity
    {

        /// <summary>
        /// Three letter abbreviation per ISO 4217. e.g. EUR, USD, GBP etc
        /// </summary>
        public string Name { get; }

        public Currency(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public override ValidationResults Validate()
        {
            var validationResults = new ValidationResults();
            if (Name == string.Empty)
            {
                validationResults.AddValidationError("Currency name cannot be empty.");
            }
            else if (Name.Length != 3)
            {
                validationResults.AddValidationError("Currency name must be exactly 3 characters long.");
            }
            else if (!Name.ContainsOnlyLetters())
            {
                validationResults.AddValidationError("Currency name must contain only letters.");
            }
            return validationResults;
        }
        public override string ToString()
        {
            return string.Join('-', Id.ToString(), Name);
        }
    }
}
