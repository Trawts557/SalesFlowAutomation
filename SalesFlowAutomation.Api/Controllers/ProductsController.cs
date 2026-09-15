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

        public ProductsController(
            GetProductByIdUseCase getProductByIdUseCase,
            CreateProductUseCase  createProductUseCase
            )
        {
            _getProductByIdUseCase = getProductByIdUseCase;
            _createProductUseCase = createProductUseCase;
        }

        [HttpGet("{id}")]
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
        public async Task<IActionResult> AddProductAsync(CreateProductRequest request)
        {
            var response = await _createProductUseCase.ExecuteAsync(request);

            if (!response.IsSuccess)
            {
                return BadRequest(response.Message);
            }

            return CreatedAtAction(nameof(GetByIdAsync), new { id = response.Data}, response.Data);
        }
    }
}
