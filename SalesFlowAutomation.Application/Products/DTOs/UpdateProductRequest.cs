
namespace SalesFlowAutomation.Application.Products.DTOs
{
    public class UpdateProductRequest
    {
        public string Name { get; set; } = string.Empty;
        public int Stock { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
