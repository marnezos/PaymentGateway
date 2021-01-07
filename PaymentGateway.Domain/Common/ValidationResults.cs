using System.Collections.Generic;

namespace PaymentGateway.Domain.Common
{
    /// <summary>
    /// Describes the validation results for validateble classes
    /// </summary>
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
