
namespace SalesFlowAutomation.Application.Common
{
    public class OperationResult<T>
    {
        public bool IsSuccess { get; private set; }
        public string Message { get; private set; } = string.Empty;      
        public T? Data { get; private set; }
        public int? StatusCode { get; private set; }

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

        public static OperationResult<T> Failure(string mensaje, int statusCode)
        {
            return new OperationResult<T>
            {
                IsSuccess = false,
                Message = mensaje,
                StatusCode = statusCode
            };
        }

    }
}
