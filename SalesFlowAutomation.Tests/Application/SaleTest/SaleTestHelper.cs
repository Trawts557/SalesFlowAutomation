
using SalesFlowAutomation.Domain.Entities;
using SalesFlowAutomation.Tests.Fakes;

namespace SalesFlowAutomation.Tests.Application.SaleTest
{
    internal class SaleTestHelper
    {
        private readonly FakeProductRepository _fakeProductRepository;
        private readonly FakeSaleRepository _fakeSaleRepository;
        public SaleTestHelper(FakeSaleRepository fakeSaleRepository, FakeProductRepository fakeProductRepository)
        {
            _fakeProductRepository = fakeProductRepository;
            _fakeSaleRepository = fakeSaleRepository;
        }
        public async Task<List<Sale>> CreateSalesWithPaymentAsync()
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

            return new List<Sale> { sale1, sale2 };
        }
    }
}
