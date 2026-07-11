
using SalesFlowAutomation.Domain.Entities;

namespace SalesFlowAutomation.Application.Products.Interfaces
{
    public interface IProductRepository
    {
        Task<Product?> GetByIdAsync(int id);
    }
}
