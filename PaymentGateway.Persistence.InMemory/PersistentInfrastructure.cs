using PaymentGateway.Application.Interfaces.Storage;
using PaymentGateway.Persistence.InMemory.Context;

namespace PaymentGateway.Persistence.InMemory
{
    public class PersistentInfrastructure : IPersistentInfrastructure<PaymentGatewayContext>
    {
        private readonly InMemoryPersistenceOptions _options;

        public PersistentInfrastructure(InMemoryPersistenceOptions options)
        {
            _options = options;
        }

        public PaymentGatewayContext GetContext()
        {
            return new PaymentGatewayContext(_options);
        }
    }
}
