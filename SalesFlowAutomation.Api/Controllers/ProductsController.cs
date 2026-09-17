using Microsoft.AspNetCore.Mvc;
using SalesFlowAutomation.Application.Products.DTOs;
using SalesFlowAutomation.Application.UseCases.Products;

namespace SalesFlowAutomation.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly GetProductByIdUseCase _getProductByIdUseCase;
        private readonly CreateProductUseCase _createProductUseCase;
        private readonly GetAllProductsUseCase _getAllProductsUseCase;

        public ProductsController(
            GetProductByIdUseCase getProductByIdUseCase,
            CreateProductUseCase  createProductUseCase,
            GetAllProductsUseCase getAllProductsUseCase
            )
        {
            _getProductByIdUseCase = getProductByIdUseCase;
            _createProductUseCase = createProductUseCase;
            _getAllProductsUseCase = getAllProductsUseCase;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var products = await _getAllProductsUseCase.ExecuteAsync();

            return Ok(products);
        }

        [HttpGet("{id}", Name = "GetProductById")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var response = await _getProductByIdUseCase.ExecuteAsync(id);

            if (!response.IsSuccess)
            {
                return NotFound(response.Message);
            }

            return Ok(response.Data);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(CreateProductRequest request)
        {
            var response = await _createProductUseCase.ExecuteAsync(request);

            if (!response.IsSuccess)
            {
                return BadRequest(response.Message);
            }

            return CreatedAtRoute(
                "GetProductById", 
                new { id = response.Data }, 
                new
                {
                    message = response.Message,
                    id = response.Data
                });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(UpdateProductRequest request)
        {
            return BadRequest();
        }
    }
}
