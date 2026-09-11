
using Microsoft.EntityFrameworkCore;
using SalesFlowAutomation.Domain.Entities;
using SalesFlowAutomation.Infrastructure.Repositories;
using SalesFlowAutomation.Tests.Infrastructure.TestDatabase;

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

            var persistedSale = context.Sales
                .Include(s => s.Details)
                .Include(s => s.Payment)
                .First(s => s.Id == sale.Id);

            Assert.NotNull(persistedSale);
            Assert.NotNull(persistedSale.Payment);
            Assert.Equal(sale.CashierId, persistedSale.CashierId);
            Assert.Equal(sale.TaxAmount, persistedSale.TaxAmount);
            Assert.Equal(sale.Subtotal, persistedSale.Subtotal);
            Assert.Equal(sale.Total, persistedSale.Total);
        }

        [Fact]
        public async Task GetByIdAsync_WithNullSale_ShouldReturnNull()
        {
            await TestDatabaseInitializer.InitializeAsync();

            await using var context = TestDbContextFactory.Create();

            var saleRepository = new SaleRepository(context);

            var sale = await saleRepository.GetByIdAsync(9999999);

            Assert.Null(sale);
        }

        [Fact]
        public async Task GetByIdAsync_WithExistingSale_ShouldReturnSale()
        {
            await TestDatabaseInitializer.InitializeAsync();
            await using var context = TestDbContextFactory.Create();

            var laptop = new Product("Laptop", 53000, 13);
            
            await context.Products.AddAsync(laptop);
            await context.SaveChangesAsync();

            Sale sale = new(1224, null);
            sale.AddDetail(laptop, 1);

            await context.Sales.AddAsync(sale);
            await context.SaveChangesAsync();

            await using var context2 = TestDbContextFactory.Create();
            var saleRepository = new SaleRepository(context2);

            var persistedSale = await saleRepository.GetByIdAsync(sale.Id);

            Assert.NotNull(persistedSale);
            Assert.Equal(sale.CashierId, persistedSale.CashierId);
            Assert.Equal(sale.Subtotal, persistedSale.Subtotal);
            Assert.Equal(sale.Total, persistedSale.Total);
            Assert.Equal(sale.TaxAmount, persistedSale.TaxAmount);
            Assert.Equal(sale.DiscountAmount, persistedSale.DiscountAmount);

            Assert.Single(persistedSale.Details);

            var persistedDetail = persistedSale.Details.First();

            Assert.Equal(laptop.Name, persistedDetail.Name);
            Assert.Equal(laptop.UnitPrice, persistedDetail.UnitPrice);
            Assert.Equal(1, persistedDetail.Quantity);

        }

    }
}
