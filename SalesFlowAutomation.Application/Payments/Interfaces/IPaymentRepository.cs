
using SalesFlowAutomation.Domain.Entities;

namespace SalesFlowAutomation.Application.Payments.Interfaces
{
    public interface IPaymentRepository
    {
        Task AddAsync(Payment payment);
    }
}
