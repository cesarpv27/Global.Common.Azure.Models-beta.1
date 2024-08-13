
namespace Global.Common.Azure.Models
{
    /// <summary>
    /// Represents an Azure response with optional <see cref="global::Azure.Response"/>, <see cref="RequestFailedException"/> and message handling capabilities.
    /// </summary>
    public interface IAzureResponse : IVerboseResponse<Dictionary<string, string>>
    {
        /// <summary>
        /// Gets a value indicating whether the response is considered an error.
        /// </summary>
        bool IsError { get; }

        /// <summary>
        /// Gets the HTTP status code of the response or exception received from the Azure service.
        /// This status code indicates the result of the HTTP request made to the service, providing insight into whether the request was successful or encountered errors.
        /// </summary>
        HttpStatusCode Status { get; }

        /// <summary>
        /// Gets a value indicating whether the instance has received a <see cref="Response"/> from Azure service.
        /// </summary>
        bool HasResponse { get; }

        /// <summary>
        /// Gets the response received from Azure, containing the data returned from the request made to the Azure service.
        /// This response typically includes status codes, headers, and the payload returned by the service.
        /// </summary>
        Response? Response { get; }

        /// <summary>
        /// Gets a value indicating whether the instance has received a <see cref="Exception"/> from Azure service.
        /// </summary>
        bool HasException { get; }

        /// <summary>
        /// Gets the exception received from Azure, containing details about the error returned from the request made to the Azure service.
        /// This exception typically includes error codes, messages, and any additional information provided by the service.
        /// </summary>
        RequestFailedException? Exception { get; }

        /// <summary>
        /// Gets a value indicating whether the instance has a <see cref="Message"/>.
        /// </summary>
        bool HasMessage { get; }

        /// <summary>
        /// Gets the message of the response or exception received from the Azure service.
        /// </summary>
        string? Message { get; }
    }
}
