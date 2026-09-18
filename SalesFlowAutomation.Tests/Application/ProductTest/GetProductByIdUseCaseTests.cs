
using SalesFlowAutomation.Application.Common;
using SalesFlowAutomation.Application.Products.DTOs;
using SalesFlowAutomation.Application.UseCases.Products;
using SalesFlowAutomation.Domain.Entities;
using SalesFlowAutomation.Tests.Fakes;

namespace SalesFlowAutomation.Tests.Application.ProductTest
{
    public class GetProductByIdUseCaseTests
    {
        private readonly FakeProductRepository _fakeProductRepository;
        private readonly GetProductByIdUseCase _getProductByIdUseCase;
        public GetProductByIdUseCaseTests()
        {
            _fakeProductRepository = new FakeProductRepository();
            _getProductByIdUseCase = new GetProductByIdUseCase(_fakeProductRepository);
        }
        [Fact]
        public async Task ExecuteAsync_WithNonExistentProduct_ShouldReturnFailure()
        {
            int id = 99;
            OperationResult<ProductResponse> response = await _getProductByIdUseCase.ExecuteAsync(id);

            Assert.NotNull(response);
            Assert.False(response.IsSuccess);
            Assert.Equal($"Product with id [{id}] not found", response.Message);
        }

        [Fact]
        public async Task ExecuteAsync_WithExistingProduct_ShouldReturnSuccess()
        {
            var product = new Product(1, "Bateria", 15000, 12);
            await _fakeProductRepository.AddAsync(product);

            OperationResult<ProductResponse> response = await _getProductByIdUseCase.ExecuteAsync(product.Id);

            Assert.NotNull(response);
            Assert.NotNull(response.Data);
            Assert.True(response.IsSuccess);

            Assert.Equal("Product obtained succesfully", response.Message);
            Assert.Equal(product.Id, response.Data.Id);
            Assert.Equal(product.Name, response.Data.Name);
            Assert.Equal(product.UnitPrice, response.Data.UnitPrice);
            Assert.Equal(product.Stock, response.Data.Stock);

        }
    }
}
