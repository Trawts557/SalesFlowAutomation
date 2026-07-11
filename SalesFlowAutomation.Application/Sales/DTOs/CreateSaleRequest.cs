
using SalesFlowAutomation.Domain.Enums;

namespace SalesFlowAutomation.Application.Sales.DTOs
{
    public class CreateSaleRequest
    {
        public int CashierId { get; set; }
        public int? CustomerId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }

        public List<CreateSaleItemRequest> Items { get; set; } = new();
    }   
}
