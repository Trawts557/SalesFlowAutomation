
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

            List<ProductListItemResponse> productList = await _getAllProductsUseCase.ExecuteAsync();

            var getBattery = productList.Single(p => p.Id == battery.Id);
            var getCharger = productList.Single(p => p.Id == charger.Id);
            var getWire = productList.Single(p => p.Id == wire.Id);

            Assert.NotEmpty(productList);

            Assert.Equal(battery.Name, getBattery.Name);
            Assert.Equal(battery.UnitPrice, getBattery.UnitPrice);

            Assert.Equal(charger.Name, getCharger.Name);
            Assert.Equal(charger.UnitPrice, getCharger.UnitPrice);

            Assert.Equal(wire.Name, getWire.Name);
            Assert.Equal(wire.UnitPrice, getWire.UnitPrice);

        }

        [Fact]
        public async Task ExecuteAsync_WithNoProducts_ShouldReturnEmptyList()
        {
            List<ProductListItemResponse> productList = await _getAllProductsUseCase.ExecuteAsync();

            Assert.Empty(productList);
        }
    }
}
