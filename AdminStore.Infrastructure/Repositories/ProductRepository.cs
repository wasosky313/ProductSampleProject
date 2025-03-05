using AdminStore.Domain.Entities;
using AdminStore.Domain.Interfaces;
using AdminStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AdminStore.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AdminStoreContext _context;

        public ProductRepository(AdminStoreContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            var products = await _context.Products
                .Include(p => p.Category) // carrega a categoria pra cada produto (por isso não funcionou com Reference que é pra um objeto só) 
                .ToListAsync(); // Converte a consulta em uma lista

            return products;
        }

        public async Task<Product> GetProductById(int id)
        {
            var product = await _context.Products.FindAsync(id);
            await _context.Entry(product).Reference(p => p.Category).LoadAsync();
            return product;
        }

        public async Task<Product> AddProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            
            // Carrega a propriedade de navegação Category manualmente
            await _context.Entry(product).Reference(p => p.Category).LoadAsync();
            return product;
        }

        public async Task<Product> UpdateProduct(Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }
    }
}