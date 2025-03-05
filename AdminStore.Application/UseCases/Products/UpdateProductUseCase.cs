using AdminStore.Domain.Entities;
using AdminStore.Domain.Interfaces;
using System.Threading.Tasks;
using AdminStore.Application.DTOs.Products;

namespace AdminStore.Application.UseCases.Products
{
    public class UpdateProductUseCase
    {
        private readonly IProductRepository _productRepository;

        public UpdateProductUseCase(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductOutput> Execute(int productId, ProductInput productInput)
        {
            var product = new Product
            {
                Id = productId,
                Name = productInput.Name,
                Price = productInput.Price,
            };
            
            var updatedProduct = await _productRepository.UpdateProduct(product);
            var productOutput = new ProductOutput
            {
                Id = updatedProduct.Id,
                Name = updatedProduct.Name,
                Price = updatedProduct.Price,
            };
            
            return productOutput;
        }
    }
}