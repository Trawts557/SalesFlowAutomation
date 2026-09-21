
using SalesFlowAutomation.Application.Products.DTOs;
using SalesFlowAutomation.Application.UseCases.Products;
using SalesFlowAutomation.Domain.Entities;
using SalesFlowAutomation.Tests.Fakes;

namespace SalesFlowAutomation.Tests.Application.ProductTest
{
    public class GetAllProductsUseCaseTests
    {
        private readonly FakeProductRepository _fakeProductRepository;
        private readonly GetAllProductsUseCase _getAllProductsUseCase;
        public GetAllProductsUseCaseTests()
        {
            _fakeProductRepository = new FakeProductRepository();
            _getAllProductsUseCase = new GetAllProductsUseCase(_fakeProductRepository);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldReturnAllProducts()
        {
            var battery = new Product(1, "Battery 220v", 17000, 12);
            var charger = new Product(2, "Charger 220v", 1760.17m, 12);
            var wire = new Product(3, "Copper Wire", 203.76m, 12);

            await _fakeProductRepository.AddAsync(charger);
            await _fakeProductRepository.AddAsync(battery);
            await _fakeProductRepository.AddAsync(wire);

            List<ProductResponse> productList = await _getAllProductsUseCase.ExecuteAsync();

            Assert.Equal(3, productList.Count);

            Assert.Contains(productList, p => p.Id == battery.Id
            && p.Name == battery.Name && p.UnitPrice == battery.UnitPrice);

            Assert.Contains(productList, p => p.Id == charger.Id
            && p.Name == charger.Name && p.UnitPrice == charger.UnitPrice);

            Assert.Contains(productList, p => p.Id == wire.Id
            && p.Name == wire.Name && p.UnitPrice == wire.UnitPrice);
        }

        [Fact]
        public async Task ExecuteAsync_WithNoProducts_ShouldReturnEmptyList()
        {
            List<ProductResponse> productList = await _getAllProductsUseCase.ExecuteAsync();

            Assert.Empty(productList);
        }
    }
}
