
using SalesFlowAutomation.Domain.Enums;

namespace SalesFlowAutomation.Application.Sales.DTOs
{
    public class SaleListResponse
    {
        public int Id { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public int CashierId { get; set; }
        public int? CustomerId { get; set; }

        public int DetailsCount { get; set; }

        public decimal Subtotal { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal Total { get; set; }
    }
}
