
using SalesFlowAutomation.Domain.Enums;

namespace SalesFlowAutomation.Application.Sales.DTOs
{
    public class PaymentResponse
    {
        public int Id { get; set; }
        public int SaleId { get; set; }
        public decimal Amount { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
    }
}
