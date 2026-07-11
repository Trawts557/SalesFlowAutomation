using SalesFlowAutomation.Domain.Entities;
using SalesFlowAutomation.Domain.Exceptions;

namespace SalesFlowAutomation.Tests.Domain
{
    public class SaleTests
    {
        [Fact]
        public void AddDetail_WithInvalidQuantity_ShouldThrowDomainException()
        {
            int cashierId = 1;
            int? customerId = null;
            Sale sale = new(cashierId, customerId);

            int productId = 1;
            string productName = "Bateria 220v";
            decimal productPrice = 13000;
            int productStock = 22;
            Product product = new(productId, productName, productPrice, productStock);

            int quantity = 0;
            Assert.Throws<DomainException>(() => sale.AddDetail(product, quantity));

        }

        [Fact]
        public void AddDetail_WithInsufficientStock_ShouldThrowDomainException()
        {
            int cashierId = 1;
            int? customerId = null;
            Sale sale = new(cashierId, customerId);

            int productId = 1;
            string productName = "Bateria 220v";
            decimal productPrice = 13000;
            int productStock = 20;
            Product product = new(productId, productName, productPrice, productStock);

            int quantity = 35;
            Assert.Throws<DomainException>(() => 
                sale.AddDetail(product, quantity));
        }

        [Fact]
        public void AddDetail_WithValidProduct_ShouldAddSaleDetail()
        {
            int cashierId = 1;
            int? customerId = null;
            Sale sale = new(cashierId, customerId);
            
            int productId = 1;
            string productName = "Bateria 220v";
            decimal productPrice = 13000;
            int productStock = 22;
            Product product = new(productId, productName, productPrice, productStock);

            int quantity = 12;
            sale.AddDetail(product, quantity);

            var detail = Assert.Single(sale.Details);

            Assert.Equal(productId, detail.ProductId);
            Assert.Equal(productPrice, detail.UnitPrice);
            Assert.Equal(12, detail.Quantity);
        }

        [Fact]
        public void AddDetail_WithExistingProduct_ShouldIncreaseQuantity()
        {
            int cashierId = 1;
            int? customerId = null;
            Sale sale = new(cashierId, customerId);

            int productId = 1;
            string productName = "Bateria 220v";
            decimal productPrice = 13000;
            int productStock = 20;
            Product product = new(productId, productName, productPrice, productStock);

            int initialQuantity = 12;
            sale.AddDetail(product, initialQuantity);

            int aditionalQuantity = 1;
            sale.AddDetail(product, aditionalQuantity);

            var detail = Assert.Single(sale.Details);

            Assert.Equal(13, detail.Quantity);
        }

        [Fact]
        public void AddDetail_WithExistingProductAndInsufficientStock_ShouldThrowDomainException()
        {
            int cashierId = 1;
            int? customerId = null;
            Sale sale = new(cashierId, customerId);

            int productId = 1;
            string productName = "Bateria 220v";
            decimal productPrice = 13000;
            int productStock = 20;
            Product product = new(productId, productName, productPrice, productStock);

            int initialQuantity = 12;
            sale.AddDetail(product, initialQuantity);

            int aditionalQuantity = 12;
            Assert.Throws<DomainException>(() => 
                sale.AddDetail(product, aditionalQuantity));
        }

        [Fact]
        public void Sale_Subtotal_ShouldReturnSumOfAllDetails()
        {
            Product battery = new(1, "Bateria 220v", 13000m, 20);

            Product charger = new(2, "Cargador 220v", 5000m, 20);

            int cashierId = 1;
            int? customerId = null;
            Sale sale = new(cashierId, customerId);

            int quantity = 1;
            sale.AddDetail(battery, quantity);
            sale.AddDetail(charger, quantity);

            decimal subTotal = 18000m;
            Assert.Equal(subTotal, sale.Subtotal);
        }

        [Fact]
        public void Sale_TaxAmount_ShouldReturnEighteenPercentOfSubtotal()
        {
            Product battery = new(1, "Bateria 220v", 13000m, 20);

            Product charger = new(2, "Cargador 220v", 5000m, 20);

            int cashierId = 1;
            int? customerId = null;
            Sale sale = new(cashierId, customerId);

            int quantity = 1;
            sale.AddDetail(battery, quantity);
            sale.AddDetail(charger, quantity);

            decimal subTotal = 18000;
            decimal expectedTax = subTotal * 0.18m;

            Assert.Equal(expectedTax, sale.TaxAmount);
        }

        [Fact]
        public void Sale_Total_ShouldReturnSubtotalPlusTaxAmountMinusDiscount()
        {
            Product battery = new(1, "Bateria 220v", 13000m, 20);

            Product charger = new(2, "Cargador 220v", 5000m, 20);

            int cashierId = 1;
            int? customerId = null;
            Sale sale = new(cashierId, customerId);

            int quantity = 1;
            sale.AddDetail(battery, quantity);
            sale.AddDetail(charger, quantity);

            decimal subtotal = 18000m;
            decimal taxAmount = subtotal * 0.18m;
            decimal discountAmount = 0m;

            decimal total = subtotal + taxAmount - discountAmount;

            Assert.Equal(total, sale.Total);
        }
    }
}
