namespace PaymentGateway.Domain.Common
{
    /// <summary>
    /// Validateable classes will provide a Validate method
    /// </summary>
    public interface IValidateable
    {
        ValidationResults Validate();
    }
}
