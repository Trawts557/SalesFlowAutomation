using SalesFlowAutomation.Application.Sales.Interfaces;
using SalesFlowAutomation.Domain.Entities;

namespace SalesFlowAutomation.Tests.Fakes
{
    internal class FakeSaleRepository : ISaleRepository
    {
        private readonly List<Sale> _sales = new();
        public IReadOnlyCollection<Sale> Sales => _sales.AsReadOnly();
        public Task AddAsync(Sale sale)
        {
            _sales.Add(sale);

            return Task.CompletedTask;
        }

        public Task<List<Sale>> GetAllAsync()
        {
            return Task.FromResult(Sales.ToList());
        }

        public Task<Sale?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
