namespace PaymentGateway.Domain.Common
{
    public abstract class Entity: IValidateable
    {
        public int Id { get; protected set; }

        public abstract ValidationResults Validate();
    }
}
