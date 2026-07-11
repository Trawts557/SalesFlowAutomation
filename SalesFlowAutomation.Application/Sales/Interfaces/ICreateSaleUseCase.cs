
using SalesFlowAutomation.Application.Common;
using SalesFlowAutomation.Application.Sales.DTOs;

namespace SalesFlowAutomation.Application.Sales.Interfaces
{
    public interface ICreateSaleUseCase
    {
        Task<OperationResult<CreateSaleResponse>> ExecuteAsync(CreateSaleRequest request);

    }
}
