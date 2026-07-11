
using SalesFlowAutomation.Domain.Entities;

namespace SalesFlowAutomation.Application.Sales.Interfaces
{
    public interface ISaleRepository
    {
        Task AddAsync(Sale sale);
    }
}
