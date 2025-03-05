using AdminStore.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using AdminStore.Application.DTOs.Products;
using AdminStore.Domain.Entities;

namespace AdminStore.Application.UseCases.Products
{
    public class GetProductsUseCase
    {
        private readonly IProductRepository _productRepository;

        public GetProductsUseCase(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<ProductOutput>> Execute()
        {
            var products = await _productRepository.GetAllProducts();

            var productOutputs = products.Select(product => new ProductOutput
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                CategoryName = product.Category.Name,
            });

            return productOutputs;
        }
    }
}