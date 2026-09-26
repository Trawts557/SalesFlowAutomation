using SalesFlowAutomation.Application.Sales.DTOs;
using SalesFlowAutomation.Application.Sales.Interfaces;
using SalesFlowAutomation.Domain.Enums;

namespace SalesFlowAutomation.Application.UseCases.Sales
{
    public class GetAllSalesUseCase
    {
        private readonly ISaleRepository _saleRepository;

        public GetAllSalesUseCase(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public async Task<List<SaleListResponse>> ExecuteAsync()
        {
            var sales = await _saleRepository.GetAllAsync(); 

            List<SaleListResponse> saleResponseList = [];
            
            foreach (var sale in sales)
            {
                saleResponseList.Add(
                    new SaleListResponse 
                    {
                        Id = sale.Id,
                        PaymentStatus = sale.Payment!.PaymentStatus,
                        CashierId = sale.CashierId,
                        CustomerId = sale.CustomerId,
                        DetailsCount = sale.Details.Count(),
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
