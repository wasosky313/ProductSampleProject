using AdminStore.Domain.Interfaces;
using System.Threading.Tasks;

namespace AdminStore.Application.UseCases.Products
{
    public class DeleteProductUseCase
    {
        private readonly IProductRepository _productRepository;

        public DeleteProductUseCase(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task Execute(int id)
        {
            await _productRepository.DeleteProduct(id);
        }
    }
}