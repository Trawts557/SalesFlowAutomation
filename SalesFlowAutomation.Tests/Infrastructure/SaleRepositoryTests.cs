
using SalesFlowAutomation.Domain.Entities;
using SalesFlowAutomation.Infrastructure.Repositories;
using SalesFlowAutomation.Tests.Infrastructure.TestDatabase;
using System.Net.Mime;

namespace SalesFlowAutomation.Tests.Infrastructure
{
    public class SaleRepositoryTests
    {
        [Fact]
        public async Task AddAsync_WithValidSale_ShouldPersistSaleAndDetails()
        {
            await TestDatabaseInitializer.InitializeAsync();

            await using var context = TestDbContextFactory.Create();

            var saleRepository = new SaleRepository(context);

            var battery = new Product("Battery 220v", 15000, 20);
            var charger = new Product("Charger 220v", 2500, 20);

            await context.Products.AddAsync(battery);
            await context.Products.AddAsync(charger);

            await context.SaveChangesAsync();

            var sale = new Sale(1224, null);

            sale.AddDetail(battery, 1);
            sale.AddDetail(charger, 1);

            Payment payment = new(sale.Total, SalesFlowAutomation.Domain.Enums.PaymentMethod.Cash);
            payment.MarkAsPaid();
            
            sale.AddPayment(payment);

            await saleRepository.AddAsync(sale);

            await using var context2 = TestDbContextFactory.Create();

            var saleRepository2 = new SaleRepository(context2);

            var persistedSale = await saleRepository2.GetByIdAsync(sale.Id);

            Assert.NotNull(persistedSale);
            Assert.Equal(sale.CashierId, persistedSale.CashierId);
            Assert.Equal(sale.TaxAmount, persistedSale.TaxAmount);
            Assert.Equal(sale.Subtotal, persistedSale.Subtotal);
            Assert.Equal(sale.Total, persistedSale.Total);
        }
    }
}
