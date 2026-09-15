
using SalesFlowAutomation.Application.Common;
using SalesFlowAutomation.Application.Products.DTOs;
using SalesFlowAutomation.Application.Products.Interfaces;
using SalesFlowAutomation.Domain.Entities;
using SalesFlowAutomation.Domain.Exceptions;

namespace SalesFlowAutomation.Application.UseCases.Products
{
    public class CreateProductUseCase
    {
        private readonly IProductRepository _productRepository;

        public CreateProductUseCase(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<OperationResult<int>> ExecuteAsync(CreateProductRequest request) 
        {
            if (request is null)
            {
                return OperationResult<int>.Failure("Request cant be null");
            }

            try
            {
                var product = new Product(request.Name, request.UnitPrice, request.Stock);
                await _productRepository.AddAsync(product);

                return OperationResult<int>.Success(product.Id, "Product created succesfully");
            }
            catch(DomainException ex)
            {
                return OperationResult<int>.Failure(ex.Message);
            }


        }
    }
}
