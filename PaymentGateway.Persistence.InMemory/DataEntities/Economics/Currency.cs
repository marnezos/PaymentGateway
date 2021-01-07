using System.ComponentModel.DataAnnotations;

namespace PaymentGateway.Persistence.InMemory.DataEntities.Economics
{
    /// <summary>
    /// DB Model for Currency
    /// </summary>
    public class Currency: DataEntity<Domain.Economics.Currency>
    {

        [Required, StringLength(3)]
        public virtual string Name { get; set; }


        public static implicit operator Domain.Economics.Currency(Currency currency)
        {
            return new Domain.Economics.Currency(currency.Id, currency.Name);
        }

        public override Domain.Economics.Currency GetDomainObject()
        {
            return this;
        }

        public override void LoadDomainObject(Domain.Economics.Currency domainObject)
        {
            Id = domainObject.Id;
            Name = domainObject.Name;
        }
    }
}
