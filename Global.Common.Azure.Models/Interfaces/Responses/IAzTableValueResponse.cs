
namespace Global.Common.Azure.Models
{
    /// <summary>
    /// Represents a response from Azure Table Service operation with value, <see cref="AzureTableServiceResponse"/>, exception and messages handling capabilities specific to Azure table.
    /// </summary>
    /// <typeparam name="T">The type of value associated with the response.</typeparam>
    /// <typeparam name="TEx">The type of exception associated with the response.</typeparam>
    public interface IAzTableValueResponse<T, TEx> : IAzGlobalValueResponse<T, AzureTableServiceResponse, TEx> where TEx : Exception { }

    /// <summary>
    /// Represents a response from Azure Table Service operation with value, <see cref="AzureTableServiceResponse"/>, <see cref="Exception"/> and messages handling capabilities specific to Azure table.
    /// </summary>
    /// <typeparam name="T">The type of value associated with the response.</typeparam>
    public interface IAzTableValueResponse<T> : IAzGlobalValueResponse<T, AzureTableServiceResponse, Exception> { }
}
