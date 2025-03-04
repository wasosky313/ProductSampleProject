using AdminStore.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        public async Task<IEnumerable<Product>> Execute()
        {
            return await _productRepository.GetAllProducts();
        }
    }
}