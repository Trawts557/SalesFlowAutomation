using SalesFlowAutomation.Application.Sales.DTOs;
using SalesFlowAutomation.Application.Sales.Interfaces;

namespace SalesFlowAutomation.Application.UseCases.Sales
{
    public class GetAllSalesUseCase
    {
        private readonly ISaleRepository _saleRepository;

        public GetAllSalesUseCase(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public async Task<List<SaleResponse>> ExecuteAsync()
        {
            var sales = await _saleRepository.GetAllAsync(); 

            List<SaleResponse> saleResponseList = [];
            
            foreach (var sale in sales)
            {
                var paymentResponse = new PaymentResponse
                {
                    Id = sale.Payment.Id,
                    SaleId = sale.Payment.SaleId,
                    Amount = sale.Payment.Amount,
                    PaymentMethod = sale.Payment.PaymentMethod,
                    PaymentStatus = sale.Payment.PaymentStatus
                };

                List<SaleDetailResponse> saleDetailResponseList = [];

                foreach (var detail in sale.Details)
                {
                    saleDetailResponseList.Add(
                        new SaleDetailResponse
                        {
                            ProductId = detail.ProductId,
                            ProductName = detail.Name,
                            UnitPrice = detail.UnitPrice,
                            Quantity = detail.Quantity,
                            TaxAmount = detail.TaxAmount,
                            Subtotal = detail.Subtotal
                        });
                }

                saleResponseList.Add(
                    new SaleResponse 
                    {
                        Id = sale.Id,
                        Payment = paymentResponse,
                        CashierId = sale.CashierId,
                        CustomerId = sale.CustomerId,
                        Details = saleDetailResponseList,
                        Subtotal = sale.Subtotal,
                        TaxAmount = sale.TaxAmount,
                        DiscountAmount = sale.DiscountAmount,
                        Total = sale.Total
                    });
            }

            return saleResponseList;
        }
    }
}
