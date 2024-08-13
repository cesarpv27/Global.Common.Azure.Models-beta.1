
namespace Global.Common.Azure.Models
{
    /// <summary>
    /// Represents a response with optional value, Azure response, exception and messages handling capabilities specific to Azure.
    /// </summary>
    /// <typeparam name="T">The type of value associated with the response.</typeparam>
    /// <typeparam name="TAzureResponse">The type of Azure response.</typeparam>
    /// <typeparam name="TEx">The type of exception associated with the response.</typeparam>
    public interface IAzGlobalValueResponse<T, TAzureResponse, TEx> : IAzGlobalResponse<TAzureResponse, TEx>, IGlobalValueResponse<T, TEx>
        where TAzureResponse : IAzureResponse
        where TEx : Exception
    { }
}
