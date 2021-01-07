using PaymentGateway.Domain.Payments;

namespace PaymentGateway.Application.Interfaces.Storage.Write
{
    /// <summary>
    /// Described a write-only repository for payment responses
    /// </summary>
    public interface IPaymentResponseWriteOnlyRepository:IWriteRepository<PaymentResponse>
    {
    }
}
