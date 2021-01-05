using PaymentGateway.Domain.Common;

namespace PaymentGateway.Domain.Economics
{
    public class Currency:Entity
    {
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
            return validationResults;
        }
    }
}
