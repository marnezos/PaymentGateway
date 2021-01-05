using PaymentGateway.Domain.Economics;

namespace PaymentGateway.Application.Interfaces.Storage.Write
{
    public interface ICurrencyWriteOnlyRepository : IWriteRepository<Currency>
    {
    }
}
