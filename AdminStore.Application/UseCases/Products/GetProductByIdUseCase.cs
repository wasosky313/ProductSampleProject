using AdminStore.Application.DTOs.Products;
using AdminStore.Domain.Entities;
using AdminStore.Domain.Interfaces;

namespace AdminStore.Application.UseCases.Products
{
    public class GetProductByIdUseCase
    {
        private readonly IProductRepository _productRepository;

        public GetProductByIdUseCase(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductOutput> Execute(int id)
        {
            var product = await _productRepository.GetProductById(id);
            var productOutput = new ProductOutput
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                CategoryName = product.Category.Name,
            };
            return productOutput;
        }
    }
}