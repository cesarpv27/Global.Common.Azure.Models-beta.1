
namespace Global.Common.Azure.Models
{
    /// <summary>
    /// Represents a response from Azure Table Service operation with <see cref="AzureTableServiceResponse"/>, exception and messages handling capabilities specific to Azure table.
    /// </summary>
    /// <typeparam name="TEx">The type of exception associated with the response.</typeparam>
    public interface IAzTableResponse<TEx> : IAzGlobalResponse<AzureTableServiceResponse, TEx> where TEx : Exception { }

    /// <summary>
    /// Represents a response from Azure Table Service operation with <see cref="IAzureTableServiceResponse"/>, <see cref="Exception"/> and messages handling capabilities specific to Azure table.
    /// </summary>
    public interface IAzTableResponse : IAzTableResponse<Exception> { }
}
