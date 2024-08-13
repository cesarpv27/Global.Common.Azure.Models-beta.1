
namespace Global.Common.Azure.Models
{
    /// <summary>
    /// Represents a response from Azure Blob Storage operation with <see cref="AzureBlobStorageResponse"/>, exception and messages handling capabilities specific to Azure blob.
    /// </summary>
    /// <typeparam name="TEx">The type of exception associated with the response.</typeparam>
    public interface IAzBlobResponse<TEx> : IAzGlobalResponse<AzureBlobStorageResponse, TEx> where TEx : Exception { }

    /// <summary>
    /// Represents a response from Azure Blob Storage operation with <see cref="IAzureBlobStorageResponse"/>, <see cref="Exception"/> and messages handling capabilities specific to Azure blob.
    /// </summary>
    public interface IAzBlobResponse : IAzBlobResponse<Exception> { }
}
