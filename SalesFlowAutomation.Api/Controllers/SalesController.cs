using Microsoft.AspNetCore.Mvc;
using SalesFlowAutomation.Application.Common;
using SalesFlowAutomation.Application.Sales.DTOs;
using SalesFlowAutomation.Application.UseCases.Sales;

namespace SalesFlowAutomation.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController(
        GetAllSalesUseCase getAllSalesUseCase,
        GetSaleByIdUseCase getSaleByIdUseCase,
        CreateSaleUseCase createSaleUseCase
        ) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            List<SaleListResponse> saleListResponse = await getAllSalesUseCase.ExecuteAsync();

            return Ok(saleListResponse);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(CreateSaleRequest request)
        {
            if (request is null)
                return BadRequest("Request cant be null");

            OperationResult<CreateSaleResponse> response = await createSaleUseCase.ExecuteAsync(request);

            if (!response.IsSuccess)
            {
                return BadRequest(response.Message);
            }

            return Ok(new
            {
                response.Message,
                response.Data
            });
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            //if (id <= 0)
            //    return BadRequest("Id must be greater than zero");

            OperationResult<SaleResponse> response = await getSaleByIdUseCase.ExecuteAsync(id);

            if (!response.IsSuccess)
            {
                if (response.StatusCode == 400)
                    return BadRequest(response.Message);

                if (response.StatusCode == 404)
                    return NotFound(response.Message);
            }

            return Ok(response.Data);
        }
    }
}
