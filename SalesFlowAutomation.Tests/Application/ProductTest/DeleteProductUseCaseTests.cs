

using SalesFlowAutomation.Application.Common;
using SalesFlowAutomation.Application.Products.DTOs;
using SalesFlowAutomation.Application.UseCases.Products;
using SalesFlowAutomation.Domain.Entities;
using SalesFlowAutomation.Tests.Fakes;

namespace SalesFlowAutomation.Tests.Application.ProductTest
{
    public class DeleteProductUseCaseTests
    {
        private readonly FakeProductRepository _fakeProductRepository;
        private readonly DeleteProductUseCase _deleteProductUseCase;

        public DeleteProductUseCaseTests()
        {
            _fakeProductRepository = new FakeProductRepository();
            _deleteProductUseCase = new DeleteProductUseCase(_fakeProductRepository);
        }

        [Fact]
        public async Task ExecuteAsync_WithProductNotFound_ShouldReturnFailure()
        {
            int id = 999;
            OperationResult<ProductResponse> response = await _deleteProductUseCase.ExecuteAsync(id);

            Assert.False(response.IsSuccess);
            Assert.Equal($"Product with id [{id}] not found", response.Message);
            Assert.Equal(404, response.StatusCode);
        }

        [Fact]
        public async Task ExecuteAsync_WithValidId_ShouldReturnSuccess()
        {
            var battery = new Product(1, "Battery", 15300, 12);
            await _fakeProductRepository.AddAsync(battery);

            OperationResult<ProductResponse> response = await _deleteProductUseCase.ExecuteAsync(battery.Id);

            Assert.True(response.IsSuccess);
            Assert.Equal("Product deleted successfully", response.Message);

            Assert.NotNull(response.Data);

            Assert.Equal(battery.Id, response.Data.Id);
            Assert.Equal(battery.Name, response.Data.Name);
        }
    }
}
