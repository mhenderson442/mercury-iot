using Mercuryiot.Functions.Models;
using Microsoft.EntityFrameworkCore;

namespace Mercuryiot.Functions.DataAccess
{
    public class ClientContext : DbContext
    {
        public ClientContext(string accountEndpoint, string accountKey, string databaseName, string containerName)
        {
            AccountEndpoint = accountEndpoint;
            AccountKey = accountKey;
            DatabaseName = databaseName;
            ContainerName = containerName;
        }

        public string AccountEndpoint { get; }
        public string AccountKey { get; }
        public DbSet<Client> Clients { get; set; }
        public string ContainerName { get; }
        public string DatabaseName { get; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseCosmos(AccountEndpoint, AccountKey, databaseName: DatabaseName);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultContainer(ContainerName);

            modelBuilder.Entity<Client>().ToContainer(ContainerName);
            modelBuilder.Entity<Client>().HasPartitionKey(o => o.Region);
        }
    }
}