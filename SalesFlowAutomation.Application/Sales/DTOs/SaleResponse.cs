
namespace SalesFlowAutomation.Application.Sales.DTOs
{
    public class SaleResponse
    {
        public int Id { get; set; }
        public PaymentResponse? Payment { get; set; }
        public int CashierId { get; set; }
        public int? CustomerId { get; set; }

        public List<SaleDetailResponse> Details { get; set; } = [];

        public decimal Subtotal { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal DiscountAmount { get; set; } 
        public decimal Total { get; set; }

    }
}
