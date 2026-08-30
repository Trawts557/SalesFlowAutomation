
using SalesFlowAutomation.Application.Common;
using SalesFlowAutomation.Application.Payments.Interfaces;
using SalesFlowAutomation.Application.Products.Interfaces;
using SalesFlowAutomation.Application.Sales.DTOs;
using SalesFlowAutomation.Application.Sales.Interfaces;
using SalesFlowAutomation.Domain.Entities;
using SalesFlowAutomation.Domain.Exceptions;

namespace SalesFlowAutomation.Application.UseCases.Sales
{
    public class CreateSaleUseCase : ICreateSaleUseCase
    {
        private readonly IProductRepository _productRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly ISaleRepository _saleRepository;

        public CreateSaleUseCase(IProductRepository productRepository,
            IPaymentRepository paymentRepository,
            ISaleRepository saleRepository)
        {
            _productRepository = productRepository;
            _paymentRepository = paymentRepository;
            _saleRepository = saleRepository;
        }

        public async Task<OperationResult<CreateSaleResponse>> ExecuteAsync(CreateSaleRequest? request)
        {
            // Validations
            string? validationError = ValidateRequest(request);
                
            if (validationError is not null)
            {
                return OperationResult<CreateSaleResponse>.Failure(validationError);
            }

            Sale sale;
            Payment payment;

            try
            {
                sale = new(request!.CashierId, request.CustomerId);

                await AddSaleDetailsAsync(request, sale);

                payment = new Payment(sale.Total, request.PaymentMethod);

                payment.MarkAsPaid();
            }
            catch (DomainException ex)
            {
                return OperationResult<CreateSaleResponse>.Failure(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return OperationResult<CreateSaleResponse>.Failure(ex.Message);
            }

            await _saleRepository.AddAsync(sale);
            await _paymentRepository.AddAsync(payment);

            var response = new CreateSaleResponse
            {
                PaymentStatus = payment.PaymentStatus,
                DiscountAmount = sale.DiscountAmount,
                Subtotal = sale.Subtotal,
                TaxAmount = sale.TaxAmount,
                Total = sale.Total
            };

            return OperationResult<CreateSaleResponse>.Success(response, "Sale completed successfully");

        }

        private static string? ValidateRequest(CreateSaleRequest? request)
        {
            if (request is null)
                return "Create sale request cant be null";

            if (request.CashierId <= 0)
                return "CashierId must be greater than zero";

            if (request.Items is null || request.Items.Count == 0)
                return"Create sale request must have at least one product";

            return null;
        }

        private async Task AddSaleDetailsAsync(CreateSaleRequest request, Sale sale)
        {

            foreach (var item in request.Items)
            {
                var product = await _productRepository.GetByIdAsync(item.ProductId);

                if (product is null)
                    throw new KeyNotFoundException($"Product with id [{item.ProductId}] not found");

                sale.AddDetail(product, item.Quantity);
            }

        }
    }
}
