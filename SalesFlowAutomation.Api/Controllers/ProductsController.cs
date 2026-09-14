using Microsoft.AspNetCore.Mvc;
using SalesFlowAutomation.Application.UseCases.Product;

namespace SalesFlowAutomation.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly GetProductByIdUseCase _getProductByIdUseCase;

        public ProductsController(GetProductByIdUseCase getProductByIdUseCase)
        {
            _getProductByIdUseCase = getProductByIdUseCase;
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
    }
}
