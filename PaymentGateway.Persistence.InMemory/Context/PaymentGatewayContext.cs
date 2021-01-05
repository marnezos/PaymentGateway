using Microsoft.EntityFrameworkCore;
using PaymentGateway.Persistence.InMemory.DataEntities.Economics;
using PaymentGateway.Persistence.InMemory.DataEntities.Merchants;
using PaymentGateway.Persistence.InMemory.DataEntities.Payments;

namespace PaymentGateway.Persistence.InMemory.Context
{
    public class PaymentGatewayContext : DbContext
    {
        public DbSet<PaymentRequest> PaymentRequest { get; set; }
        public DbSet<PaymentResponse> PaymentResponse { get; set; }
        public DbSet<Merchant> Merchant { get; set; }
        public DbSet<Currency> Currency { get; set; }

        private readonly InMemoryPersistenceOptions _options;

        public PaymentGatewayContext(InMemoryPersistenceOptions options)
        {
            _options = options;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(_options.InMemoryDbName);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Enforce unique request id per merchant
            modelBuilder.Entity<PaymentRequest>()
                .HasAlternateKey(pr => new { pr.MerchantUniqueRequestId, pr.MerchantId });

            //Enforce unique gateway request id (hash)
            modelBuilder.Entity<PaymentRequest>()
                .HasAlternateKey(pr => pr.GatewayUniqueRequestId);

            //Seed sample data
            modelBuilder.Entity<Currency>().HasData(
                new Currency() { Id = 1, Name = "EUR" },
                new Currency() { Id = 2, Name = "USD" },
                new Currency() { Id = 3, Name = "GBP" }
            );

            modelBuilder.Entity<Merchant>().HasData(
                new Merchant() { Id = 1, Name = "merchant1", Email = "merchant1@example.com" },
                new Merchant() { Id = 2, Name = "merchant2", Email = "merchant2@example.com" },
                new Merchant() { Id = 3, Name = "merchant3", Email = "merchant3@example.com" }
            );
        }
    }
}
