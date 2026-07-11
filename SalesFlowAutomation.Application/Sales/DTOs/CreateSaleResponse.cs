
using SalesFlowAutomation.Domain.Enums;

namespace SalesFlowAutomation.Application.Sales.DTOs
{
    public class CreateSaleResponse
    {
        public int? SaleId { get; set; }
        public decimal Subtotal { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal Total { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
    }
}
