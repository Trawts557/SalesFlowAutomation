using Microsoft.AspNetCore.Mvc;
using SalesFlowAutomation.Application.Sales.DTOs;
using SalesFlowAutomation.Application.UseCases.Sales;

namespace SalesFlowAutomation.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController(GetAllSalesUseCase getAllSalesUseCase) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            List<SaleResponse> saleResponses = await getAllSalesUseCase.ExecuteAsync();

            return Ok(saleResponses);
        }
    }
}
