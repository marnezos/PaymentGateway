using PaymentGateway.Application.Interfaces.Storage.Write;
using PaymentGateway.Domain.Payments;
using PaymentGateway.Persistence.InMemory.Context;

namespace PaymentGateway.Persistence.InMemory.Repositories.Payments
{
    /// <summary>
    /// PaymentRequest Write only repository (implements the IPaymentRequestWriteOnlyRepository as specificed in the application layer)
    /// </summary>
    public class PaymentRequestWriteOnlyRepository : BaseWriteRepository<PaymentRequest, DataEntities.Payments.PaymentRequest>, IPaymentRequestWriteOnlyRepository
    {
        public PaymentRequestWriteOnlyRepository(PaymentGatewayContext context) : base(context) { }
    }
}
