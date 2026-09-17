
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
            _updateProductUseCase = new UpdateProductUseCase();
        }

        [Fact]
        public async Task ExecuteAsync_WithInvalidRequest_ShouldReturnFailure()
        {
            var battery = new Product(1, "Battery", 15200, 16);
            
        }

        [Fact]
        public async Task ExecuteAsync_WithValidRequest_ShouldReturnSuccess()
        {
            
        }
    }
}