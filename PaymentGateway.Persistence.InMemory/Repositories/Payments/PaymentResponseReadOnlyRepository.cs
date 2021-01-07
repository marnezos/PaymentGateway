using Microsoft.EntityFrameworkCore;
using PaymentGateway.Application.Interfaces.Storage.Read;
using PaymentGateway.Domain.Payments;
using PaymentGateway.Persistence.InMemory.Context;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGateway.Persistence.InMemory.Repositories.Payments
{
    /// <summary>
    /// PaymentRespons read only repository (implements the IPaymentResponseReadOnlyRepository as specificed in the application layer)
    /// </summary>
    public class PaymentResponseReadOnlyRepository : BaseReadRepository<PaymentResponse, DataEntities.Payments.PaymentResponse>, IPaymentResponseReadOnlyRepository
    {
        public PaymentResponseReadOnlyRepository(PaymentGatewayContext context) : base(context) { }

        public override IQueryable<DataEntities.Payments.PaymentResponse> Aggregate => _db.PaymentResponse
                                                                                            .Include(pr => pr.PaymentRequest)
                                                                                                .ThenInclude(pr => pr.Merchant)
                                                                                            .Include(pr => pr.PaymentRequest)
                                                                                                .ThenInclude(pr => pr.Currency)
                                                                                            .Include(pr => pr.PaymentRequest);

        public async Task<PaymentResponse> GetByMerchantIdAndMerchantUniqueIdAsync(int merchantId, string merchantUniqueRequestId)
        {
            return await Aggregate.FirstOrDefaultAsync(p => p.PaymentRequest.MerchantId == merchantId && p.PaymentRequest.MerchantUniqueRequestId == merchantUniqueRequestId);
        }
    }
}
