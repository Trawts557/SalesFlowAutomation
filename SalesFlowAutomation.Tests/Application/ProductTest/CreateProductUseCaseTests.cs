
using SalesFlowAutomation.Application.Common;
using SalesFlowAutomation.Application.Products.DTOs;
using SalesFlowAutomation.Application.UseCases.Products;
using SalesFlowAutomation.Tests.Fakes;

namespace SalesFlowAutomation.Tests.Application.ProductTest
{
    public class CreateProductUseCaseTests
    {
        private readonly CreateProductUseCase _createProductUseCase;
        private readonly FakeProductRepository _fakeProductRepository;

        public CreateProductUseCaseTests()
        {
            _fakeProductRepository = new FakeProductRepository();
            _createProductUseCase = new CreateProductUseCase(_fakeProductRepository);
        }

        [Fact]
        public async Task ExecuteAsync_WithValidRequest_ShouldReturnSuccess()
        {
            var request = new CreateProductRequest("Power Supply", 13500, 12);

            OperationResult<int> response = await _createProductUseCase.ExecuteAsync(request);
            
            Assert.NotNull(response);
            Assert.True(response.IsSuccess);
            Assert.Equal("Product created succesfully", response.Message);

            var savedProduct = Assert.Single(_fakeProductRepository.Products);

            Assert.Equal(request.Name, savedProduct.Name);
            Assert.Equal(request.UnitPrice, savedProduct.UnitPrice);
            Assert.Equal(request.Stock, savedProduct.Stock);

        }
        [Fact]
        public async Task ExecuteAsync_WithInvalidRequest_ShouldReturnFailure()
        {
            var request = new CreateProductRequest("", -13500, -1);

            OperationResult<int> response = await _createProductUseCase.ExecuteAsync(request);
            
            Assert.False(response.IsSuccess);
            Assert.NotNull(response);

        }

        [Fact]
        public async Task ExecuteAsync_WithNullRequest_ShouldReturnFailure()
        {
            CreateProductRequest request = null;

            OperationResult<int> response = await _createProductUseCase.ExecuteAsync(request);

            Assert.False(response.IsSuccess);
            Assert.Equal("Request cant be null", response.Message);
        }
    }
}
