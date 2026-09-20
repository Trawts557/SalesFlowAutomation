
using SalesFlowAutomation.Domain.Entities;

namespace SalesFlowAutomation.Application.Products.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(int id);
        Task AddAsync(Product product);
        Task DeleteAsync(Product product);
        Task SaveChangesAsync();
    }
}
