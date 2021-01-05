using System.Collections.Generic;

namespace PaymentGateway.Domain.Common
{
    public class ValidationResults
    {
        public bool HasErrors { get
            {
                return ValidationErrors.Count > 0;
            }
        }
        public List<string> ValidationErrors { get; internal set; } = new List<string>();
        public void AddValidationError(string errorMessage)
        {
            ValidationErrors.Add(errorMessage);
        }
    }
}
