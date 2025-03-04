using AdminStore.Application.DTOs.Products;
using AdminStore.Application.UseCases.Products;
using AdminStore.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AdminStore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly GetProductsUseCase _getProductsUseCase;
        private readonly GetProductByIdUseCase _getProductByIdUseCase;
        private readonly AddProductUseCase _addProductUseCase;
        private readonly UpdateProductUseCase _updateProductUseCase;
        private readonly DeleteProductUseCase _deleteProductUseCase;

        public ProductsController(
            GetProductsUseCase getProductsUseCase,
            GetProductByIdUseCase getProductByIdUseCase,
            AddProductUseCase addProductUseCase,
            UpdateProductUseCase updateProductUseCase,
            DeleteProductUseCase deleteProductUseCase)
        {
            _getProductsUseCase = getProductsUseCase;
            _getProductByIdUseCase = getProductByIdUseCase;
            _addProductUseCase = addProductUseCase;
            _updateProductUseCase = updateProductUseCase;
            _deleteProductUseCase = deleteProductUseCase;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductOutput>>> GetAllProducts()
        {
            var products = await _getProductsUseCase.Execute();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductOutput>> GetProductById(int id)
        {
            var productOutput = await _getProductByIdUseCase.Execute(id);
            if (productOutput == null)
            {
                return NotFound();
            }
            return Ok(productOutput);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] ProductInput productInput)
        {
            var result = await _addProductUseCase.Execute(productInput);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductInput productInput)
        {
            var updatedProduct = await _updateProductUseCase.Execute(id, productInput);
            return Ok(updatedProduct);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _deleteProductUseCase.Execute(id);
            return NoContent();
        }
    }
}