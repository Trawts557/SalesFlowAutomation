
namespace SalesFlowAutomation.Application.Common
{
    public class OperationResult<T>
    {
        public bool IsSuccess { get; private set; }
        public string Message { get; private set; } = string.Empty;      
        public T? Data { get; private set; }

        private OperationResult() { }

        public static OperationResult<T> Success(T data, string mensaje)
        {
            return new OperationResult<T>
            {
                IsSuccess = true,
                Message = mensaje,
                Data = data
            };
        }

        public static OperationResult<T> Failure(string mensaje)
        {
            return new OperationResult<T>
            {
                IsSuccess = false,
                Message = mensaje,
            };
        }

    }
}
