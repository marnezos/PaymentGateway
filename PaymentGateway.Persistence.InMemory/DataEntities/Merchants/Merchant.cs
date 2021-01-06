using System.ComponentModel.DataAnnotations;

namespace PaymentGateway.Persistence.InMemory.DataEntities.Merchants
{
    public class Merchant: DataEntity<Domain.Merchants.Merchant>
    {
        [Required, StringLength(50)]
        public virtual string Name { get; set; }

        [Required, StringLength(255)]
        public virtual string Email { get; set; }

        public static implicit operator Domain.Merchants.Merchant(Merchant merchant)
        {
            return new Domain.Merchants.Merchant(merchant.Id, merchant.Name, merchant.Email);
        }

        public override Domain.Merchants.Merchant GetDomainObject()
        {
            return this;
        }

        public override void LoadDomainObject(Domain.Merchants.Merchant domainObject)
        {
            Id = domainObject.Id;
            Name = domainObject.Name;
            Email = domainObject.Email;
        }
    }
}
