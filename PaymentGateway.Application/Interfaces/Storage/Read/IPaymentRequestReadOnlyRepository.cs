using PaymentGateway.Domain.Payments;
using System.Threading.Tasks;

namespace PaymentGateway.Application.Interfaces.Storage.Read
{
    public interface IPaymentRequestReadOnlyRepository:IReadRepository<PaymentRequest>
    {
        public Task<PaymentRequest> GetByMerchantIdAndMerchantUniqueIdAsync(int merchantId, string merchantUniqueRequestId);
    }
}
