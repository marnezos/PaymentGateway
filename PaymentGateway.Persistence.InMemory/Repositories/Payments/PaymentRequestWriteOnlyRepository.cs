using PaymentGateway.Application.Interfaces.Storage.Write;
using PaymentGateway.Domain.Payments;
using PaymentGateway.Persistence.InMemory.Context;

namespace PaymentGateway.Persistence.InMemory.Repositories.Payments
{
    public class PaymentRequestWriteOnlyRepository : BaseWriteRepository<PaymentRequest, DataEntities.Payments.PaymentRequest>, IPaymentRequestWriteOnlyRepository
    {
        public PaymentRequestWriteOnlyRepository(PaymentGatewayContext context) : base(context) { }
    }
}
