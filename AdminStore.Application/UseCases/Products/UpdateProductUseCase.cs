using AdminStore.Domain.Entities;
using AdminStore.Domain.Interfaces;
using System.Threading.Tasks;

namespace AdminStore.Application.UseCases.Products
{
    public class UpdateProductUseCase
    {
        private readonly IProductRepository _productRepository;

        public UpdateProductUseCase(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task Execute(Product product)
        {
            await _productRepository.UpdateProduct(product);
        }
    }
}