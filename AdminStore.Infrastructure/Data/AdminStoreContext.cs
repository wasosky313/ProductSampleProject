using Microsoft.EntityFrameworkCore;
using AdminStore.Domain.Entities;

namespace AdminStore.Infrastructure.Data
{
    public class AdminStoreContext : DbContext
    {
        public AdminStoreContext(DbContextOptions<AdminStoreContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configura o relacionamento entre Product e Category
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category) // Um produto tem uma categoria
                .WithMany(c => c.Products) // Uma categoria tem muitos produtos
                .HasForeignKey(p => p.CategoryId); // Chave estrangeira

            base.OnModelCreating(modelBuilder);
        }
    }
}