namespace PaymentGateway.Domain.Common
{
    /// <summary>
    /// DDD entities
    /// </summary>
    public abstract class Entity : IValidateable
    {
        public int Id { get; protected set; }

        public abstract ValidationResults Validate();

        public bool IsValid
        {
            get
            {
                return !Validate().HasErrors;
            }
        }
    }
}
