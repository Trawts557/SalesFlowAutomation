
using SalesFlowAutomation.Application.Common;
using SalesFlowAutomation.Application.Products.DTOs;
using SalesFlowAutomation.Application.Products.Interfaces;

namespace SalesFlowAutomation.Application.UseCases.Products
{
    public class GetProductByIdUseCase
    {
        private readonly IProductRepository _productRepository;

        public GetProductByIdUseCase(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<OperationResult<ProductResponse>> ExecuteAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product == null)
            {
                return OperationResult<ProductResponse>.Failure($"Product with id [{id}] not found");
            }

            var getProductResponse =
                new ProductResponse
                {
                    Id = product.Id,
                    Name = product.Name,
                    UnitPrice = product.UnitPrice,
                    Stock = product.Stock
                };

            return OperationResult<ProductResponse>.Success(getProductResponse, "Product obtained succesfully");
        }
    }
}
