
using SalesFlowAutomation.Application.Common;
using SalesFlowAutomation.Application.Sales.DTOs;
using SalesFlowAutomation.Application.UseCases.Sales;
using SalesFlowAutomation.Domain.Entities;
using SalesFlowAutomation.Tests.Fakes;

namespace SalesFlowAutomation.Tests.Application.SaleTest
{
    public class GetSaleByIdUseCaseTests
    {
        private readonly FakeSaleRepository _saleRepository;
        private readonly FakeProductRepository _fakeProductRepository;
        private readonly GetSaleByIdUseCase _getSaleByIdUseCase;

        public GetSaleByIdUseCaseTests()
        {
            _saleRepository = new FakeSaleRepository();
            _fakeProductRepository = new FakeProductRepository();
            _getSaleByIdUseCase = new GetSaleByIdUseCase(_saleRepository);
        }

        [Fact]
        public async Task ExecuteAsync_WithValidId_ShouldReturnSuccess()
        {
            var helper = new SaleTestHelper(_saleRepository, _fakeProductRepository);

            List<Sale> sales = await helper.CreateSalesWithPaymentAsync();

            OperationResult<SaleResponse> response = await _getSaleByIdUseCase.ExecuteAsync(1);

            Sale sale1 = sales[0];

            Assert.True(response.IsSuccess);
            Assert.Equal("Sale obtained succesfully", response.Message);
            Assert.NotNull(response.Data);

            SaleResponse data = response.Data;

            // Asserting use case map to SaleResponse
            Assert.Equal(sale1.Id, data.Id);
            Assert.Equal(sale1.CashierId, data.CashierId);
            Assert.Equal(sale1.CustomerId, data.CustomerId);
            Assert.Equal(sale1.TaxAmount, data.TaxAmount);
            Assert.Equal(sale1.DiscountAmount, data.DiscountAmount);
            Assert.Equal(sale1.Total, data.Total);

            // Asserting use case map to PaymentResponse
            Assert.NotNull(data.Payment);

            Assert.Equal(sale1.Payment!.Amount, data.Payment.Amount);
            Assert.Equal(sale1.Payment.PaymentStatus, data.Payment.PaymentStatus);

            // Asserting use case map to SaleDetailResponse
            Assert.Single(data.Details);

            var detail = data.Details[0]; // first SaleDetail from response.Data
            var saleDetail = sale1.Details.First(); // first detail from domain sale

            Assert.Equal(saleDetail.ProductId, detail.ProductId);
            Assert.Equal(saleDetail.Name, detail.ProductName);
            Assert.Equal(saleDetail.Quantity, detail.Quantity);
            Assert.Equal(saleDetail.UnitPrice, detail.UnitPrice);
            Assert.Equal(saleDetail.Subtotal, detail.Subtotal);
            Assert.Equal(saleDetail.TaxAmount, detail.TaxAmount);

        }

    }
}
