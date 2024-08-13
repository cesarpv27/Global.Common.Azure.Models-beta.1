
namespace Global.Common.Azure.Tests.Models
{
    /// <summary>
    /// Represents odata content from Azure responses.
    /// </summary>
    internal class ODataTest
    {
        /// <summary>
        /// Gets or sets the content of odata of Azure response.
        /// </summary>
        public string? OData { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        public string? Message { get; set; }

        /// <summary>
        /// Gets or sets the table error code.
        /// </summary>
        public TableErrorCode ErrorCode { get; set; }
    }
}
