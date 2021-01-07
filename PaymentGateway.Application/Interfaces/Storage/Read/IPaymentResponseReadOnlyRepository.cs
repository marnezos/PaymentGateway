using PaymentGateway.Domain.Payments;
using System.Threading.Tasks;

namespace PaymentGateway.Application.Interfaces.Storage.Read
{
    /// <summary>
    /// Described a read-only repository for payment responses
    /// </summary>
    public interface IPaymentResponseReadOnlyRepository : IReadRepository<PaymentResponse>
    {
        public Task<PaymentResponse> GetByMerchantIdAndMerchantUniqueIdAsync(int merchantId, string merchantUniqueRequestId);
    }
}
