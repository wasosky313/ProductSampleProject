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

        public async Task<Product> Execute(int id)
        {
            return await _productRepository.GetProductById(id);
        }
    }
}