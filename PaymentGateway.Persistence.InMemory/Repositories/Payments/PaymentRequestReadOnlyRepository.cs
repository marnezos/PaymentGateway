using Microsoft.EntityFrameworkCore;
using PaymentGateway.Application.Interfaces.Storage.Read;
using PaymentGateway.Domain.Payments;
using PaymentGateway.Persistence.InMemory.Context;
using System.Linq;

namespace PaymentGateway.Persistence.InMemory.Repositories.Payments
{
    public class PaymentRequestReadOnlyRepository : BaseReadRepository<PaymentRequest, DataEntities.Payments.PaymentRequest>, IPaymentRequestReadOnlyRepository
    {
        public PaymentRequestReadOnlyRepository(PaymentGatewayContext context) : base(context) { }

        public override IQueryable<DataEntities.Payments.PaymentRequest> Aggregate => _db.PaymentRequest
                                                                                            .Include(pr => pr.Merchant)
                                                                                            .Include(pr => pr.Currency);
    }
}
