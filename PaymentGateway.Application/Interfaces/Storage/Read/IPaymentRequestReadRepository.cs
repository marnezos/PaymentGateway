using PaymentGateway.Domain.Payments;

namespace PaymentGateway.Application.Interfaces.Storage.Read
{
    public interface IPaymentRequestReadRepository:IReadRepository<PaymentRequest>
    {        
    }
}
