
namespace Global.Common.Azure.Models
{
    /// <summary>
    /// Represents an error specific to Azure storage operations, with parsing of OData information.
    /// </summary>
    /// <summary>
    /// Represents an error in an Azure OData service.
    /// </summary>
    public abstract class AzODataResponseError
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AzODataResponseError"/> class with the specified message.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="message"/> is null.</exception>
        public AzODataResponseError(string message)
        {
            AssertHelper.AssertNotNullOrThrow(message, nameof(message));

            Message = message;
        }

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        public string Message { get; set; }
    }
}
