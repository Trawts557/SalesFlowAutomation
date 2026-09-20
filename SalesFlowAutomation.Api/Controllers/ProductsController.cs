using Microsoft.AspNetCore.Mvc;
using SalesFlowAutomation.Application.Products.DTOs;
using SalesFlowAutomation.Application.UseCases.Products;

namespace SalesFlowAutomation.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(
            GetProductByIdUseCase getProductByIdUseCase,
            CreateProductUseCase createProductUseCase,
            GetAllProductsUseCase getAllProductsUseCase,
            UpdateProductUseCase updateProductUseCase,
            DeleteProductUseCase deleteProductUseCase
        ) : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var products = await getAllProductsUseCase.ExecuteAsync();

            return Ok(products);
        }

        [HttpGet("{id:int}", Name = "GetProductById")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var response = await getProductByIdUseCase.ExecuteAsync(id);

            if (!response.IsSuccess)
            {
                return NotFound(response.Message);
            }

            return Ok(response.Data);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(CreateProductRequest request)
        {
            var response = await createProductUseCase.ExecuteAsync(request);

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

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateAsync(int id, UpdateProductRequest request)
        {
            var response = await updateProductUseCase.ExecuteAsync(id, request);

            if (!response.IsSuccess)
            {   
                if (response.StatusCode == 404)
                    return NotFound(response.Message );

                return BadRequest(response.Message);
            }

            return Ok(response.Data);           
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var response = await deleteProductUseCase.ExecuteAsync(id);

            if (!response.IsSuccess)
            {
                if (response.StatusCode == 400)
                    return BadRequest(response.Message);

                if (response.StatusCode == 404)
                    return NotFound(response.Message);
            }

            return Ok(new
            {
                message = response.Message,
                data = response.Data
            });
        }
    }
}
