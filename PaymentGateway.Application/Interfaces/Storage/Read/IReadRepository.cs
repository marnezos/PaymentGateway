using PaymentGateway.Domain.Common;
using System.Threading.Tasks;

namespace PaymentGateway.Application.Interfaces.Storage.Read
{
    public interface IReadRepository<T> where T:Entity
    {
        public Task<T> GetByIdAsync(int id);
    }
}
