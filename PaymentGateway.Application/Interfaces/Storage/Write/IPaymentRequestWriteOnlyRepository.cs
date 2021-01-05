using PaymentGateway.Domain.Payments;

namespace PaymentGateway.Application.Interfaces.Storage.Write
{
    public interface IPaymentRequestWriteOnlyRepository:IWriteRepository<PaymentRequest>
    {        
    }
}
