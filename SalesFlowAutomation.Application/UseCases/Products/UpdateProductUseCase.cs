using SalesFlowAutomation.Application.Common;
using SalesFlowAutomation.Application.Products.DTOs;
using SalesFlowAutomation.Application.Products.Interfaces;

namespace SalesFlowAutomation.Application.UseCases.Products
{
    public class UpdateProductsUseCase
    {
        private readonly IProductRepository _productRepository;

        public UpdateProductsUseCase(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<OperationResult<ProductResponse>> ExecuteAsync(UpdateProductRequest request)
        {
            throw new NotImplementedException();
        }
    }
}