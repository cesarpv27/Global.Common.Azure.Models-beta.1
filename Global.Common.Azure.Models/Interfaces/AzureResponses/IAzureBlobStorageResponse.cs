
namespace Global.Common.Azure.Models
{
    /// <summary>
    /// Represents an response specific to Azure Blob storage with optional <see cref="Response"/>, <see cref="RequestFailedException"/>, message, and <see cref="BlobErrorCode"/> handling capabilities.
    /// </summary>
    public interface IAzureBlobStorageResponse : IAzureResponse, IAzStorageError<BlobErrorCode>
    {
    }
}
