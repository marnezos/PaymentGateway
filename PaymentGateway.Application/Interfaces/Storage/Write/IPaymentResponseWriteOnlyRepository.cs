using PaymentGateway.Domain.Payments;

namespace PaymentGateway.Application.Interfaces.Storage.Write
{
    public interface IPaymentResponseWriteOnlyRepository:IWriteRepository<PaymentResponse>
    {
    }
}
