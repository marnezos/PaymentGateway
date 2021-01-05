namespace PaymentGateway.Application.Interfaces.Storage
{
    public interface IPersistentInfrastructure<T>
    {
        T GetContext();
    }
}
