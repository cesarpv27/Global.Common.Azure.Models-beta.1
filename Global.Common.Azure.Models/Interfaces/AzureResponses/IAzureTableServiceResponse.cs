
namespace Global.Common.Azure.Models
{
    /// <summary>
    /// Represents an response specific to Azure Table service with optional <see cref="Response"/>, <see cref="RequestFailedException"/>, message, and <see cref="TableErrorCode"/> handling capabilities.
    /// </summary>
    public interface IAzureTableServiceResponse : IAzureResponse, IAzStorageError<TableErrorCode>
    {
    }
}
