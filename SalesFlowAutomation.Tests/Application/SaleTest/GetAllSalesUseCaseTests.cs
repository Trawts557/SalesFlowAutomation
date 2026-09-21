using SalesFlowAutomation.Application.Sales.DTOs;
using SalesFlowAutomation.Application.UseCases.Sales;
using SalesFlowAutomation.Domain.Entities;
using SalesFlowAutomation.Tests.Fakes;

namespace SalesFlowAutomation.Tests.Application.SaleTest
{
    public class GetAllSalesUseCaseTests
    {
        private readonly GetAllSalesUseCase _getAllSalesUseCase;
        private readonly FakeSaleRepository _fakeSaleRepository;
        private readonly FakeProductRepository _fakeProductRepository;

        public GetAllSalesUseCaseTests()
        {
            _fakeSaleRepository = new FakeSaleRepository();
            _fakeProductRepository = new FakeProductRepository();
            _getAllSalesUseCase = new GetAllSalesUseCase(_fakeSaleRepository);
        }

        [Fact]
        public async Task ExecuteAsync_WithNoSales_ShouldReturnEmptyList()
        {
            var saleResponseList = await _getAllSalesUseCase.ExecuteAsync();

            Assert.Empty(saleResponseList);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldReturnAllSales()
        {
            // Adding products
            var battery = new Product(1, "Battery 220v", 17000, 12);
            var charger = new Product(2, "Charger 220v", 1760.17m, 12);

            await _fakeProductRepository.AddAsync(charger);
            await _fakeProductRepository.AddAsync(battery);

            // Creating sales
            var sale1 = new Sale(1, null);
            sale1.AddDetail(battery, 12);

            var sale2 = new Sale(2, null);
            sale2.AddDetail(charger, 7);

            // Creating payments and adding payments
            var payment1 = new Payment(sale1.Total, SalesFlowAutomation.Domain.Enums.PaymentMethod.Card);
            payment1.MarkAsPaid();
            sale1.AddPayment(payment1);
            
            var payment2 = new Payment(sale2.Total, SalesFlowAutomation.Domain.Enums.PaymentMethod.Cash);
            payment2.MarkAsPaid();
            sale2.AddPayment(payment2);
            
            // Adding sales
            await _fakeSaleRepository.AddAsync(sale1);
            await _fakeSaleRepository.AddAsync(sale2);

            List<SaleResponse> saleList = await _getAllSalesUseCase.ExecuteAsync();

            Assert.Equal(2, saleList.Count);

            Assert.Contains(saleList, s =>
            s.Id == sale1.Id &&
            s.Payment!.Amount == payment1.Amount &&
            s.Payment.PaymentStatus == payment1.PaymentStatus &&
            s.Details.Count == 1 &&
            s.Details[0].ProductId == battery.Id &&
            s.Details[0].ProductName == battery.Name &&
            s.Details[0].Quantity == 12
            );

            Assert.Contains(saleList, s =>
            s.Id == sale2.Id &&
            s.Payment!.Amount == payment2.Amount &&
            s.Payment.PaymentStatus == payment2.PaymentStatus &&
            s.Details.Count == 1 &&
            s.Details[0].ProductId == charger.Id &&
            s.Details[0].ProductName == charger.Name &&
            s.Details[0].Quantity == 7
            );

        }
    }
}
