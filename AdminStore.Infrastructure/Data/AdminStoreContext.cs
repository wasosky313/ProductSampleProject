using AdminStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AdminStore.Infrastructure.Data
{
    public class AdminStoreContext : DbContext
    {
        public AdminStoreContext(DbContextOptions<AdminStoreContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configurações de modelo, se necessário
        }
    }
}