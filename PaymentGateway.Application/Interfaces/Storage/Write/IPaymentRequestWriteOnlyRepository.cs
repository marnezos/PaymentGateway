using PaymentGateway.Domain.Payments;

namespace PaymentGateway.Application.Interfaces.Storage.Write
{
    /// <summary>
    /// Described a write-only repository for payment requests
    /// </summary>
    public interface IPaymentRequestWriteOnlyRepository:IWriteRepository<PaymentRequest>
    {        
    }
}
