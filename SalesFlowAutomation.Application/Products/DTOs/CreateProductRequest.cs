
namespace SalesFlowAutomation.Application.Products.DTOs
{
    public class CreateProductRequest
    {
        public CreateProductRequest(string name, decimal unitPrice, int stock)
        {
            Name = name;
            UnitPrice = unitPrice;
            Stock = stock;
        }

        public string Name { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public int Stock { get; set; }
    }
}
