using AdminStore.Domain.Entities;
using AdminStore.Domain.Interfaces;
using System.Threading.Tasks;

namespace AdminStore.Application.UseCases.Products
{
    public class AddProductUseCase
    {
        private readonly IProductRepository _productRepository;

        public AddProductUseCase(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task Execute(Product product)
        {
            await _productRepository.AddProduct(product);
        }
    }
}