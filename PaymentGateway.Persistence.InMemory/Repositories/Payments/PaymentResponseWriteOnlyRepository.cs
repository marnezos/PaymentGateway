using PaymentGateway.Application.Interfaces.Storage.Write;
using PaymentGateway.Domain.Payments;
using PaymentGateway.Persistence.InMemory.Context;


namespace PaymentGateway.Persistence.InMemory.Repositories.Payments
{
    /// <summary>
    /// PaymentResponse Write only repository (implements the IPaymentResponseWriteOnlyRepository as specificed in the application layer)
    /// </summary>
    public class PaymentResponseWriteOnlyRepository : BaseWriteRepository<PaymentResponse, DataEntities.Payments.PaymentResponse>, IPaymentResponseWriteOnlyRepository
    {
        public PaymentResponseWriteOnlyRepository(PaymentGatewayContext context) : base(context) { }
    }
}
