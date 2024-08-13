
namespace Global.Common.Azure.Tests.Models
{
    internal class AzODataErrorTestResult<T> where T : AzODataResponseError
    {
        public AzODataErrorTestResult(bool status, T? azODataError)
        {
            Status = status;
            AzODataError = azODataError;
        }

        public bool Status { get; set; }
        public T? AzODataError { get; set; }

        public static AzODataErrorTestResult<T> Create(bool status, T? azODataError)
        {
            return new AzODataErrorTestResult<T>(status, azODataError);
        }
    }
}
