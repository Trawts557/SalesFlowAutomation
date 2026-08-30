using SalesFlowAutomation.Domain.Exceptions;

namespace SalesFlowAutomation.Domain.Entities
{
    public class Sale
    {
        public int Id { get; private set; }
        public int CashierId { get; private set; }
        public int? CustomerId { get; private set; }
        private readonly List<SaleDetail> _details = new();
        public IReadOnlyCollection<SaleDetail> Details => _details.AsReadOnly();
        public decimal Subtotal => _details.Sum(x => x.Subtotal);
        public decimal TaxAmount => _details.Sum(x => x.TaxAmount);
        public decimal DiscountAmount { get; private set; } = 0m;
        public decimal Total => Subtotal + TaxAmount - DiscountAmount;

        public Sale(int cashierId, int? customerId)
        {
            CashierId = cashierId;
            CustomerId = customerId;
        }

        public void AddDetail(Product product, int quantity)
        {
            ValidateProduct(product);
            ValidateQuantity(quantity);

            var existingDetail = _details.SingleOrDefault(x => x.ProductId == product.Id);

            ValidateStock(product, quantity, existingDetail);

            if (existingDetail is not null)
            {
                existingDetail.IncreaseQuantity(quantity);
                return;
            }

            _details.Add(new SaleDetail(product.Id, product.Name, product.UnitPrice, quantity));
        }

        #region Validaciones
        private void ValidateProduct(Product product)
        {
            if (product is null)
                throw new DomainException("Product cant be null");
        }

        private void ValidateQuantity(int quantity)
        {
            if (quantity <= 0)
                throw new DomainException("Quantity must be greater than zero");
        }

        private void ValidateStock(Product product, int requestedQuantity, SaleDetail? existingDetail)
        {
            var totalRequestedQuantity = requestedQuantity + (existingDetail?.Quantity ?? 0);

            if (totalRequestedQuantity > product.Stock)
                throw new DomainException($"There is not enough stock, the actual stock is: [{product.Stock}]");
        }
        #endregion

    }
}
