
using SalesFlowAutomation.Application.Common;
using SalesFlowAutomation.Application.Products.DTOs;
using SalesFlowAutomation.Application.UseCases.Products;
using SalesFlowAutomation.Domain.Entities;
using SalesFlowAutomation.Tests.Fakes;

namespace SalesFlowAutomation.Tests.Application.ProductTest
{
    public class UpdateProductUseCaseTests
    {
        private readonly FakeProductRepository _fakeProductRepository;
        private readonly UpdateProductUseCase _updateProductUseCase;
        public UpdateProductUseCaseTests()
        {
            _fakeProductRepository = new FakeProductRepository();
            _updateProductUseCase = new UpdateProductUseCase(_fakeProductRepository);
        }

        [Fact]
        public async Task ExecuteAsync_WithValidRequest_ShouldReturnSuccess()
        {
            var battery = new Product(1, "Battery", 15200, 16);

            await _fakeProductRepository.AddAsync(battery);

            var request = new UpdateProductRequest
            {
                Name = "Battery",
                UnitPrice = 15000,
                Stock = 12
            };

            OperationResult<ProductResponse> response = await _updateProductUseCase.ExecuteAsync(battery.Id, request);

            Assert.True(response.IsSuccess);
            Assert.Equal("Product updated successfully", response.Message);

            Assert.NotNull(response.Data);

            Assert.Equal(battery.Id, response.Data.Id);
            Assert.Equal(request.Name, response.Data.Name);
            Assert.Equal(request.UnitPrice, response.Data.UnitPrice);
            Assert.Equal(request.Stock, response.Data.Stock);
        }

        [Fact]
        public async Task ExecuteAsync_WithInvalidRequest_ShouldReturnFailure()
        {
            var battery = new Product(1, "Battery", 15200, 16);

            await _fakeProductRepository.AddAsync(battery);

            var request = new UpdateProductRequest
            {
                Name = "",
                UnitPrice = 15000,
                Stock = 12
            };

            var request2 = new UpdateProductRequest
            {
                Name = "New product",
                UnitPrice = 0,
                Stock = 12
            };

            var request3 = new UpdateProductRequest
            {
                Name = "New product",
                UnitPrice = 13500,
                Stock = -1
            };

            OperationResult<ProductResponse> response1 = await _updateProductUseCase.ExecuteAsync(battery.Id, request);
            OperationResult<ProductResponse> response2 = await _updateProductUseCase.ExecuteAsync(battery.Id, request2);
            OperationResult<ProductResponse> response3 = await _updateProductUseCase.ExecuteAsync(battery.Id, request3);

            Assert.False(response1.IsSuccess);
            Assert.Equal("Name cant be null or empty", response1.Message);

            Assert.False(response2.IsSuccess);
            Assert.Equal("Price must be greater than zero", response2.Message);

            Assert.False(response3.IsSuccess);
            Assert.Equal("Stock must be greater than zero", response3.Message);
        }
    }
}