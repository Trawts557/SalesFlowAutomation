
using SalesFlowAutomation.Application.Common;
using SalesFlowAutomation.Application.Products.DTOs;
using SalesFlowAutomation.Application.Products.Interfaces;

namespace SalesFlowAutomation.Application.UseCases.Product
{
    public class GetProductByIdUseCase
    {
        private readonly IProductRepository _productRepository;

        public GetProductByIdUseCase(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<OperationResult<GetProductByIdResponse>> ExecuteAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product == null)
            {
                return OperationResult<GetProductByIdResponse>.Failure($"Product with id [{id}] not found");
            }

            var getProductResponse =
                new GetProductByIdResponse
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.UnitPrice,
                    Stock = product.Stock
                };

            return OperationResult<GetProductByIdResponse>.Success(getProductResponse, "Product obtained succesfully");
        }
    }
}
