using SalesFlowAutomation.Application.Sales.Interfaces;
using SalesFlowAutomation.Domain.Entities;

namespace SalesFlowAutomation.Tests.Fakes
{
    internal class FakeSaleRepository : ISaleRepository
    {
        private readonly List<Sale> _sales = new();
        public IReadOnlyCollection<Sale> Sales => _sales.AsReadOnly();
        private int _nextId = 1;
        public Task AddAsync(Sale sale)
        {
            // Simulate database-generated identity
            typeof(Sale)
                .GetProperty(nameof(Sale.Id))!
                .SetValue(sale, _nextId++);

            _sales.Add(sale);

            return Task.CompletedTask;
        }

        public Task<List<Sale>> GetAllAsync()
        {
            return Task.FromResult(Sales.ToList());
        }

        public Task<Sale?> GetByIdAsync(int id)
        {
            return Task.FromResult(_sales.FirstOrDefault(s => s.Id == id));
        }
    }
}
