
namespace Global.Common.Azure.Models
{
    /// <summary>
    /// Represents a response with optional Azure response, exception and messages handling capabilities specific to Azure.
    /// </summary>
    /// <typeparam name="TAzureResponse">The type of Azure response.</typeparam>
    /// <typeparam name="TEx">The type of exception associated with the response.</typeparam>
    public interface IAzGlobalResponse<TAzureResponse, TEx> : IGlobalResponse<TEx>
        where TAzureResponse : IAzureResponse
        where TEx : Exception
    {
        /// <summary>
        /// Gets a value indicating whether the instance has an <see cref="AzureResponse"/>.
        /// </summary>
        bool HasAzureResponse { get; }

        /// <summary>
        /// Gets the Azure response associated.
        /// </summary>
        TAzureResponse? AzureResponse { get; }

        /// <summary>
        /// If set to <c>true</c>, includes AzureResponse verbose information when <see cref="IVerboseResponse{T}.BuildVerbose"/> is called;
        /// otherwise, AzureResponse verbose information will not be included. Default value is <c>true</c>.
        /// </summary>
        bool IncludeAzureResponseVerbose { get; set; }
    }
}
