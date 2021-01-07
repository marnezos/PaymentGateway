using PaymentGateway.Domain.Common;
using System.Threading.Tasks;

namespace PaymentGateway.Application.Interfaces.Storage.Read
{
    /// <summary>
    /// Every Read repository must provide this
    /// </summary>
    public interface IReadRepository<T> where T:Entity
    {
        public Task<T> GetByIdAsync(int id);
    }
}
