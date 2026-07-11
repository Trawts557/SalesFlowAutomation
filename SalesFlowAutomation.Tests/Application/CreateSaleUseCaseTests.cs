using SalesFlowAutomation.Application.Payments.Interfaces;
using SalesFlowAutomation.Application.Products.Interfaces;
using SalesFlowAutomation.Application.Sales.DTOs;
using SalesFlowAutomation.Application.Sales.Interfaces;
using SalesFlowAutomation.Application.UseCases.Sales;
using SalesFlowAutomation.Domain.Entities;
using SalesFlowAutomation.Domain.Enums;

namespace SalesFlowAutomation.Tests.Application
{
    public class CreateSaleUseCaseTests
    {
        private readonly FakeSaleRepository _saleRepository;
        private readonly FakeProductRepository _productRepository;
        private readonly FakePaymentRepository _paymentRepository;
        private readonly CreateSaleUseCase _createSaleUseCase;

        public CreateSaleUseCaseTests()
        {
            _saleRepository = new FakeSaleRepository();
            _productRepository = new FakeProductRepository();
            _paymentRepository = new FakePaymentRepository();

            _createSaleUseCase = new CreateSaleUseCase(
                _productRepository,
                _paymentRepository,
                _saleRepository
            );
        }

        [Fact]
        public async Task ExecuteAsync_WithNullRequest_ShouldReturnFailure()
        {
            CreateSaleRequest? request = null;

            var result = await _createSaleUseCase.ExecuteAsync(request);

            Assert.False(result.IsSuccess);
            Assert.Equal("Create sale request cant be null", result.Message);

        }

        [Fact]
        public async Task ExecuteAsync_WithInvalidCashierId_ShouldReturnFailure()
        {
            var createSaleRequest = new CreateSaleRequest
            {
                CashierId = 0,
                CustomerId = null,
                PaymentMethod = PaymentMethod.Cash,
                Items =
                [
                    new CreateSaleItemRequest{ ProductId = 1, Quantity = 1 }
                ]
            };

            _productRepository.AddProduct(new Product(1, "New product", 100m, 12));

            var result = await _createSaleUseCase.ExecuteAsync(createSaleRequest);

            Assert.False(result.IsSuccess);
            Assert.Equal("CashierId must be greater than zero", result.Message);
        }

        [Fact]
        public async Task ExecuteAsync_WithProductNotFound_ShouldReturnFailure()
        {
            var request = new CreateSaleRequest
            {
                CustomerId = null,
                CashierId = 1,
                PaymentMethod = PaymentMethod.Card,
                Items =
                [
                    new CreateSaleItemRequest{ ProductId = 999, Quantity = 1 }
                ]
            };

            var result = await _createSaleUseCase.ExecuteAsync(request);

            Assert.False(result.IsSuccess);
            Assert.Equal("Product with id [999] not found", result.Message);
        }

        [Fact]
        public async Task ExecuteAsync_WithValidRequest_ShouldReturnSuccess()
        {
            CreateProductForTest(22);
            CreateSaleRequest request = CreateSaleRequestForTest();

            var result = await _createSaleUseCase.ExecuteAsync(request);

            Assert.NotNull(result.Data);

            CreateSaleResponse response = result.Data;

            Assert.Equal(PaymentStatus.Paid, response.PaymentStatus);
            Assert.Equal(18000m * 0.18m, response.TaxAmount);
            Assert.Equal(18000m, response.Subtotal);
            Assert.Equal(21240m, response.Total);

            Assert.Single(_saleRepository.Sales);
            Assert.Single(_paymentRepository.Payments);

            Assert.True(result.IsSuccess);
            Assert.Equal("Sale completed successfully", result.Message);
        }

        [Fact]
        public async Task ExecuteAsync_WithInsufficientStock_ShouldReturnFailure()
        {
            CreateProductForTest(1);
            CreateSaleRequest request = CreateSaleRequestForTest();

            request.Items[0].Quantity = 2;
            
            var response = await _createSaleUseCase.ExecuteAsync(request);

            Assert.False(response.IsSuccess);
            Assert.Equal("There is not enough stock, the actual stock is: [1]", response.Message);
        }

        private void CreateProductForTest(int stock)
        {
            Product product = new(1, "Monitor 144hz", 18000m, stock);

            _productRepository.AddProduct(product);
        }

        private CreateSaleRequest CreateSaleRequestForTest()
        {
            return new CreateSaleRequest
            {
                CashierId = 1,
                CustomerId = null,
                PaymentMethod = PaymentMethod.Card,
                Items =
                [
                    new CreateSaleItemRequest{ ProductId = 1, Quantity = 1 }
                ]
            };
        }

        private class FakeProductRepository : IProductRepository
        {
            private readonly List<Product> _products = new();

            public void AddProduct(Product product)
            {
                _products.Add(product);
            }

            public Task<Product?> GetByIdAsync(int id)
            {
                return Task.FromResult(_products.SingleOrDefault(x => x.Id == id));
            }
        }

        private class FakePaymentRepository : IPaymentRepository
        {
            private readonly List<Payment> _payments = new();
            public IReadOnlyCollection<Payment> Payments => _payments.AsReadOnly();

            public Task AddAsync(Payment payment)
            {
                _payments.Add(payment);

                return Task.CompletedTask;
            }
        }

        private class FakeSaleRepository : ISaleRepository
        {
            private readonly List<Sale> _sales = new();
            public IReadOnlyCollection<Sale> Sales => _sales.AsReadOnly();
            public Task AddAsync(Sale sale)
            {
                _sales.Add(sale);

                return Task.CompletedTask;
            }
        }

    }
}
