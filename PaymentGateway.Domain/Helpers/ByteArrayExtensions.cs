using System.Text;

namespace PaymentGateway.Domain.Helpers
{
    public static class ByteArrayExtensions
    {
        public static string ToHexString(this byte[] array)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (byte b in array)
            {
                stringBuilder.Append(b.ToString("X2"));
            }
            return stringBuilder.ToString();
        }
    }
}
