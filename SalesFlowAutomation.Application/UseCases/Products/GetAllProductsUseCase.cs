
using SalesFlowAutomation.Application.Products.DTOs;
using SalesFlowAutomation.Application.Products.Interfaces;

namespace SalesFlowAutomation.Application.UseCases.Products
{
    public class GetAllProductsUseCase
    {
        private readonly IProductRepository _productRepository;

        public GetAllProductsUseCase(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<List<ProductResponse>> ExecuteAsync()
        {
            var products = await _productRepository.GetAllAsync();

            List<ProductResponse> productResponseList = new();

            foreach (var item in products)
            {
                var itemMapped = new ProductResponse
                {
                    Id = item.Id,
                    Name = item.Name,
                    UnitPrice = item.UnitPrice,
                    Stock = item.Stock

                };

                productResponseList.Add(itemMapped);
            }
            
            return productResponseList;
        }
    }
}
