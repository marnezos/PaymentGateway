namespace PaymentGateway.Persistence.InMemory
{
    /// <summary>
    /// Options for the database. Composition root will provide.
    /// </summary>
    public class InMemoryPersistenceOptions
    {
        public string InMemoryDbName { get; set; }
    }
}
