using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AdminStore.Infrastructure.Data;
using System.Linq;
using Microsoft.AspNetCore.Hosting;

namespace AdminStore.API.Tests
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Remove a configuração do DbContext padrão
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<AdminStoreContext>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                // Adiciona o DbContext com a connection string de teste
                services.AddDbContext<AdminStoreContext>(options =>
                {
                    options.UseNpgsql("Host=localhost;Database=admin_store_test;Username=solfacil;Password=solfacil");
                });

                // Cria o banco de dados e aplica as migrations
                var sp = services.BuildServiceProvider();
                using (var scope = sp.CreateScope())
                {
                    var db = scope.ServiceProvider.GetRequiredService<AdminStoreContext>();
                    db.Database.EnsureDeleted(); // Limpa o banco de dados antes dos testes
                    db.Database.EnsureCreated(); // Cria o banco de dados e aplica as migrations
                }
            });
        }
    }
}