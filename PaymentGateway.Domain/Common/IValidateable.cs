namespace PaymentGateway.Domain.Common
{
    public interface IValidateable
    {
        ValidationResults Validate();
    }
}
