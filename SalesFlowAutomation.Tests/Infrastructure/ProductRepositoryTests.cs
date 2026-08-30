
using Microsoft.EntityFrameworkCore;
using SalesFlowAutomation.Domain.Entities;
using SalesFlowAutomation.Infrastructure.Repositories;
using SalesFlowAutomation.Tests.Infrastructure.TestDatabase;

namespace SalesFlowAutomation.Tests.Infrastructure
{
    public class ProductRepositoryTests
    {

        [Fact]
        public async Task GetByIdAsync_WithExistingProduct_ShouldReturnProduct()
        {
            await TestDatabaseInitializer.InitializeAsync();

            await using var context = TestDbContextFactory.Create();

            var batery = new Product("Bateria 220v", 17000, 15);

            await context.Products.AddAsync(batery);

            await context.SaveChangesAsync();

            await using var context2 = TestDbContextFactory.Create();

            var productRepository = new ProductRepository(context2);

            var product = await productRepository.GetByIdAsync(batery.Id);

            Assert.NotNull(product);

            Assert.Equal(batery.Name, product.Name);
            Assert.Equal(batery.UnitPrice, product.UnitPrice);
        }

        [Fact]
        public async Task GetByIdAsync_WithNonExistentProduct_ShouldReturnNull()
        {
            await TestDatabaseInitializer.InitializeAsync();

            await using var context = TestDbContextFactory.Create();

            var productRepository = new ProductRepository(context);

            var product = await productRepository.GetByIdAsync(999);

            Assert.Null(product);

        }

        [Fact]
        public async Task AddAsync_WithValidProduct_ShouldPersistProduct()
        {
            await TestDatabaseInitializer.InitializeAsync();

            await using var context = TestDbContextFactory.Create();

            var productRepository = new ProductRepository(context);

            var charger = new Product("Charger 33w", 200, 10);

            await productRepository.AddAsync(charger);

            await using var context2 = TestDbContextFactory.Create();

            var product = await context2.Products.FirstOrDefaultAsync(p => p.Id == charger.Id);

            Assert.NotNull(product);
            Assert.Equal(charger.Name, product.Name);
            Assert.Equal(charger.UnitPrice, product.UnitPrice);
            Assert.Equal(charger.Stock, product.Stock);
        }

    }
}
