using SalesFlowAutomation.Application.Common;
using SalesFlowAutomation.Application.Products.DTOs;
using SalesFlowAutomation.Application.Products.Interfaces;
using SalesFlowAutomation.Domain.Exceptions;

namespace SalesFlowAutomation.Application.UseCases.Products
{
    public class UpdateProductUseCase
    {
        private readonly IProductRepository _productRepository;

        public UpdateProductUseCase(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<OperationResult<ProductResponse>> ExecuteAsync(int id, UpdateProductRequest request)
        {
            if (request is null)
            {
                return OperationResult<ProductResponse>
                    .Failure("Request can't be null", 400);
            }

            var product = await _productRepository.GetByIdAsync(id);

            if (product == null)
            {
                return OperationResult<ProductResponse>.Failure($"Product with id [{id}] not found", 404);
            }

            try
            {
                product.Update(request.Name, request.UnitPrice, request.Stock);
            }
            catch(DomainException ex)
            {
                return OperationResult<ProductResponse>.Failure(ex.Message);
            }


            await _productRepository.SaveChangesAsync();

            var response = new ProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                UnitPrice = product.UnitPrice,
                Stock = product.Stock
            };

            return OperationResult<ProductResponse>.Success(response, "Product updated successfully");
        }
    }
}