
using SalesFlowAutomation.Application.Products.Interfaces;
using SalesFlowAutomation.Domain.Entities;

namespace SalesFlowAutomation.Tests.Fakes
{
    internal class FakeProductRepository : IProductRepository
    {
        private readonly List<Product> _products = new();
        public IReadOnlyCollection<Product> Products => _products.AsReadOnly();

        public Task AddAsync(Product product)
        {
            _products.Add(product);
            return Task.CompletedTask;
        }

        public Task<Product?> GetByIdAsync(int id)
        {
            return Task.FromResult(_products.SingleOrDefault(x => x.Id == id));
        }

        public Task<List<Product>> GetAllAsync()
        {
            return Task.FromResult(_products.ToList());
        }

        public Task UpdateAsync(Product product)
        {
            var existingProduct = _products.FirstOrDefault(p => p.Id == product.Id);

            existingProduct.Update(product.Name, product.UnitPrice, product.Stock);

            return Task.CompletedTask;
        }

    }
}
