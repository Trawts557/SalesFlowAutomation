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
        CreateSaleUseCase createSaleUseCase
        ) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            List<SaleResponse> saleResponses = await getAllSalesUseCase.ExecuteAsync();

            return Ok(saleResponses);
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
                }
            );

        }
    }
}
