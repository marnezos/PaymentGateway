using PaymentGateway.Domain.Common;
using System.Text.RegularExpressions;

namespace PaymentGateway.Domain.Merchants
{

    /// <summary>
    /// Any merchant that may use our gateway.
    /// </summary>
    public class Merchant : Entity
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public Merchant() { }

        public Merchant(int id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
        }

        public override ValidationResults Validate()
        {
            var validationResults = new ValidationResults();
            //Name must not be empty
            if (Name == string.Empty)
            {
                validationResults.AddValidationError("Merchant's name cannot be empty.");
            }
            else if (Name.Length > 50)
            {
                validationResults.AddValidationError("Merchant's name may not be longer than 50 characters.");
            }

            //Email must not be empty
            if (Email == string.Empty)
            {
                validationResults.AddValidationError("Merchant's email cannot be empty.");
            } 
            else if (Email.Length > 255)
            {
                validationResults.AddValidationError("Merchant's email may not be longer than 255 characters.");
            }
            else if (!Regex.IsMatch(Email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase))
            {
                //Regex from : https://stackoverflow.com/questions/16167983/best-regular-expression-for-email-validation-in-c-sharp/16168118
                validationResults.AddValidationError("Merchant's email is not valid.");
            }
            return validationResults;
        }

        public override string ToString()
        {
            return string.Join('-', Id.ToString(), Name, Email);
        }
    }
}
