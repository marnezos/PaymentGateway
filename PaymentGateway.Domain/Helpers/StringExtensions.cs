
namespace PaymentGateway.Domain.Helpers
{
    public static class StringExtensions
    {
        public static bool ContainsOnlyDigits(this string thisString)
        {
            foreach (char c in thisString)
            {
                if (c < '0' || c > '9')
                    return false;
            }
            return true;
        }
    }
}
