
using SalesFlowAutomation.Application.Common;
using SalesFlowAutomation.Application.Sales.DTOs;
using SalesFlowAutomation.Application.Sales.Interfaces;

namespace SalesFlowAutomation.Application.UseCases.Sales
{
    public class GetSaleByIdUseCase
    {
        private readonly ISaleRepository _saleRepository;

        public GetSaleByIdUseCase(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public async Task<OperationResult<SaleResponse>> ExecuteAsync(int id)
        {
            // Getting and validating sale
            if (id <= 0)
                return OperationResult<SaleResponse>.Failure("Id must be greater than zero", 400);

            var sale = await _saleRepository.GetByIdAsync(id);

            if (sale is null)
                return OperationResult<SaleResponse>.Failure($"Sale with id [{id}] not found", 404);

            // Mapping Payment to PaymentResponse
            var paymentResponse = new PaymentResponse
            {
                Id = sale.Payment!.Id,
                SaleId = sale.Payment.SaleId,
                Amount = sale.Payment.Amount,
                PaymentStatus = sale.Payment.PaymentStatus,
                PaymentMethod = sale.Payment.PaymentMethod,
            };

            // Mapping SaleDetail to SaleDetailResponse
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

            // Maping Sale to SaleResponse
            SaleResponse saleResponse = new SaleResponse
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
            };

            return OperationResult<SaleResponse>.Success(saleResponse, "Sale obtained succesfully");
        }
    }
}

