
using Microsoft.EntityFrameworkCore;
using SalesFlowAutomation.Application.Products.Interfaces;
using SalesFlowAutomation.Domain.Entities;
using SalesFlowAutomation.Infrastructure.Persistence;

namespace SalesFlowAutomation.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            var products = await _context.Products.ToListAsync();

            return products;
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

            return product;
            
        }

        public async Task AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            var existingProduct = await _context.Products.FirstOrDefaultAsync(p => p.Id == product.Id);

            existingProduct.Update(product.Name, product.UnitPrice, product.Stock);
            
            await _context.SaveChangesAsync();
        }
    }
}
