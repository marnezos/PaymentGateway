using PaymentGateway.Domain.Common;
using System.Threading.Tasks;

namespace PaymentGateway.Application.Interfaces.Storage.Write
{
    /// <summary>
    /// Every Write repository must provide this
    /// </summary>
    public interface IWriteRepository<in T> where T:Entity
    {
        public Task SaveAsync(T entity);
    }
}
