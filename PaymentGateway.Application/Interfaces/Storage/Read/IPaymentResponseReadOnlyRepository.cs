using PaymentGateway.Domain.Payments;
using System.Threading.Tasks;

namespace PaymentGateway.Application.Interfaces.Storage.Read
{
    public interface IPaymentResponseReadOnlyRepository : IReadRepository<PaymentResponse>
    {
        public Task<PaymentResponse> GetByMerchantIdAndMerchantUniqueIdAsync(int merchantId, string merchantUniqueRequestId);
    }
}
