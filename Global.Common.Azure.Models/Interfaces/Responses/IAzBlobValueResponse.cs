
namespace Global.Common.Azure.Models
{
    /// <summary>
    /// Represents a response from Azure Blob Storage operation with value, <see cref="AzureBlobStorageResponse"/>, exception and messages handling capabilities specific to Azure blob.
    /// </summary>
    /// <typeparam name="T">The type of value associated with the response.</typeparam>
    /// <typeparam name="TEx">The type of exception associated with the response.</typeparam>
    public interface IAzBlobValueResponse<T, TEx> : IAzGlobalValueResponse<T, AzureBlobStorageResponse, TEx> where TEx : Exception { }

    /// <summary>
    /// Represents a response from Azure Blob Storage operation with value, <see cref="AzureBlobStorageResponse"/>, <see cref="Exception"/> and messages handling capabilities specific to Azure blob.
    /// </summary>
    /// <typeparam name="T">The type of value associated with the response.</typeparam>
    public interface IAzBlobValueResponse<T> : IAzGlobalValueResponse<T, AzureBlobStorageResponse, Exception> { }
}
