
using SalesFlowAutomation.Application.Common;
using SalesFlowAutomation.Application.Products.DTOs;
using SalesFlowAutomation.Application.Products.Interfaces;
using SalesFlowAutomation.Application.UseCases.Product;
using SalesFlowAutomation.Domain.Entities;

namespace SalesFlowAutomation.Tests.Application.ProductTest
{
    public class GetProductByIdUseCaseTests
    {
        private readonly FakeRepository _fakeRepository;
        private readonly GetProductByIdUseCase _getProductByIdUseCase;
        public GetProductByIdUseCaseTests()
        {
            _fakeRepository = new FakeRepository();
            _getProductByIdUseCase = new GetProductByIdUseCase(_fakeRepository);
        }
        [Fact]
        public async Task ExecuteAsync_WithNonExistentProduct_ShouldReturnFailure()
        {
            int id = 99;
            OperationResult<GetProductByIdResponse> response = await _getProductByIdUseCase.ExecuteAsync(id);

            Assert.NotNull(response);
            Assert.False(response.IsSuccess);
            Assert.Equal($"Product with id [{id}] not found", response.Message);
        }

        [Fact]
        public async Task ExecuteAsync_WithExistingProduct_ShouldReturnSuccess()
        {
            var product = new Product(1, "Bateria", 15000, 12);
            await _fakeRepository.AddAsync(product);

            OperationResult<GetProductByIdResponse> response = await _getProductByIdUseCase.ExecuteAsync(product.Id);

            Assert.NotNull(response);
            Assert.NotNull(response.Data);
            Assert.True(response.IsSuccess);

            Assert.Equal("Product obtained succesfully", response.Message);
            Assert.Equal(product.Id, response.Data.Id);
            Assert.Equal(product.Name, response.Data.Name);
            Assert.Equal(product.UnitPrice, response.Data.Price);
            Assert.Equal(product.Stock, response.Data.Stock);

        }
    }

    public class FakeRepository : IProductRepository
    {
        private readonly List<Product> Products = new();
        public Task AddAsync(Product product)
        {
            Products.Add(product);
            return Task.CompletedTask;
        }

        public Task<Product?> GetByIdAsync(int id)
        {
            return Task.FromResult(Products.FirstOrDefault(p => p.Id == id));
        }
    }
}
