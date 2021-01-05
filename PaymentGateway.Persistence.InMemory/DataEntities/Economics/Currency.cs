using System.ComponentModel.DataAnnotations;

namespace PaymentGateway.Persistence.InMemory.DataEntities.Economics
{
    public class Currency: DataEntity<Domain.Economics.Currency>
    {

        [Required, StringLength(3)]
        public virtual string Name { get; set; }

        public static implicit operator Domain.Economics.Currency (Currency currency)
        {
            return new Domain.Economics.Currency(currency.Id, currency.Name);
        }

    }
}
