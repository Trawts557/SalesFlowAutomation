using SalesFlowAutomation.Domain.Exceptions;

namespace SalesFlowAutomation.Domain.Entities
{
    public class SaleDetail
    {
        public int ProductId { get; private set; }
        public string Name { get; private set; }
        public decimal UnitPrice { get; private set; }
        public int Quantity { get; private set; }
        public decimal Subtotal => UnitPrice * Quantity;
        private const decimal TaxRate = 0.18m;
        public decimal TaxAmount => Subtotal * TaxRate;

        internal SaleDetail(int productId, string name, decimal unitPrice, int quantity)
        {
            if (productId <= 0)
                throw new DomainException("Product id must be greater than zero");

            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Name cant be null or empty");

            if (unitPrice <= 0)
                throw new DomainException("Unit price must be greater than zero");

            ProductId = productId;
            Name = name;
            UnitPrice = unitPrice;
            Quantity = quantity;
        } 
        
        public void IncreaseQuantity(int quantity)
        {
            if (quantity <= 0)
                throw new DomainException("Quantity must be greater than zero");

            Quantity += quantity;
        }
    }
}
