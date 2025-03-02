using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using AdminStore.Infrastructure.Data;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace AdminStore.API.Tests
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration((context, config) =>
            {
                // Carrega o appsettings.json do projeto de API
                var projectDir = Directory.GetCurrentDirectory();
                var configPath = Path.Combine(projectDir, "../../../AdminStore.API/appsettings.json");

                config.AddJsonFile(configPath);
            });

            builder.ConfigureServices(services =>
            {
                // Substitui o DbContext padrão pelo de teste
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<AdminStoreContext>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                // Adiciona o DbContext com a connection string de teste
                services.AddDbContext<AdminStoreContext>(options =>
                {
                    var configuration = new ConfigurationBuilder()
                        .AddJsonFile("appsettings.json")
                        .Build();

                    var testConnectionString = configuration.GetConnectionString("TestConnection");
                    options.UseNpgsql(testConnectionString);
                });
            });
        }
    }
}