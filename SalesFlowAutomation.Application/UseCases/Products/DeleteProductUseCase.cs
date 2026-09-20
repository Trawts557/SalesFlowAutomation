
using SalesFlowAutomation.Application.Common;
using SalesFlowAutomation.Application.Products.DTOs;
using SalesFlowAutomation.Application.Products.Interfaces;

namespace SalesFlowAutomation.Application.UseCases.Products
{
    public class DeleteProductUseCase
    {
        private readonly IProductRepository _productRepository;

        public DeleteProductUseCase(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<OperationResult<ProductResponse>> ExecuteAsync(int id)
        {
            if (id <= 0)
                return OperationResult<ProductResponse>.Failure("Id must be greater than zero", 400);

            var product = await _productRepository.GetByIdAsync(id);

            if (product is null)
                return OperationResult<ProductResponse>.Failure($"Product with id [{id}] not found", 404);

            await _productRepository.DeleteAsync(product);

            var response = new ProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                UnitPrice = product.UnitPrice,
                Stock = product.Stock
            };

            return OperationResult<ProductResponse>.Success(response, "Product deleted successfully");
        }
    }
}
