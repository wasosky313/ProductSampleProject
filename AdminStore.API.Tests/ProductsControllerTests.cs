using System.Net;
using System.Net.Http.Json;
using AdminStore.Application.DTOs.Products;
using AdminStore.Domain.Entities;
using AdminStore.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;

namespace AdminStore.API.Tests
{
    public class ProductsControllerTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly CustomWebApplicationFactory _factory;
        private readonly HttpClient _client;

        public ProductsControllerTests(CustomWebApplicationFactory factory)
        {
            _factory = factory;
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task AddProduct_ShouldReturnCreated_WhenInputIsValid()
        {
            // Arrange
            var payload = new ProductInput { Name = "Product 1", Price = 10.0m };

            // Act
            var response = await _client.PostAsJsonAsync("/api/products", payload);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode); // TODO mudar response pra Created

            var productOutput = await response.Content.ReadFromJsonAsync<ProductOutput>();
            Assert.NotNull(productOutput);
            Assert.Equal("Product 1", productOutput.Name);
            Assert.Equal(10.0m, productOutput.Price);
        }
        
        [Fact]
        public async Task GetProductById_ShouldReturnProduct_WhenProductExists()
        {
            // Arrange
            using (var scope = _factory.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AdminStoreContext>();

                // Criar o produto diretamente no banco de dados
                var product = new Product
                {
                    Name = "Product Sale",
                    Price = 112.0m
                };
                dbContext.Products.Add(product);
                await dbContext.SaveChangesAsync();

                // Act
                var getResponse = await _client.GetAsync($"/api/products/{product.Id}");

                // Assert
                getResponse.EnsureSuccessStatusCode();
                Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);

                var productOutput = await getResponse.Content.ReadFromJsonAsync<ProductOutput>();
                Assert.NotNull(productOutput);
                Assert.Equal(product.Id, productOutput.Id);
                Assert.Equal("Product Sale", productOutput.Name);
                Assert.Equal(112.0m, productOutput.Price);

                // Limpar o banco de dados após o teste 
                // TODO tentar botar no factory
                dbContext.Products.Remove(product);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}