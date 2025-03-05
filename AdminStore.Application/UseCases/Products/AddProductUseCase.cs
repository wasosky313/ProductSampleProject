using AdminStore.Domain.Entities;
using AdminStore.Domain.Interfaces;
using AdminStore.Application.DTOs.Products;

namespace AdminStore.Application.UseCases.Products
{
    public class AddProductUseCase
    {
        private readonly IProductRepository _productRepository;

        public AddProductUseCase(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductOutput> Execute(ProductInput productInput)
        {
            var product = new Product
            {
                Name = productInput.Name,
                Price = productInput.Price,
                CategoryId = 1, // this is only for test, make a get for a real situation
            };
            
            var productSaved = await _productRepository.AddProduct(product);
            
            var productOutput = new ProductOutput
            {
                Id = productSaved.Id,
                Name = productSaved.Name,
                Price = productSaved.Price,
                CategoryName = productSaved.Category.Name,
            };
            
            return productOutput;
        }
    }
}