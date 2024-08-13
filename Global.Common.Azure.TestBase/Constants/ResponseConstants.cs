
namespace Global.Common.Azure.TestBase
{
    /// <summary>
    /// Provides constants related to responses.
    /// </summary>
    public static class ResponseConstants
    {
        /// <summary>
        /// Gets the key for serializer errors in the response messages.
        /// </summary>
        public static string SerializerErrorMessageKey => "SerializerError";

        /// <summary>
        /// Gets the key for deserializer errors in the response messages.
        /// </summary>
        public static string DeserializerErrorMessageKey => "DeserializerError";

        /// <summary>
        /// Gets the key for the table name in the response messages.
        /// </summary>
        public static string TableNameMessageKey => "TableName";

        /// <summary>
        /// Gets the key for the partition key in the response messages.
        /// </summary>
        public static string PartitionKeyMessageKey => "PartitionKey";

        /// <summary>
        /// Gets the key for the row key in the response messages.
        /// </summary>
        public static string RowKeyMessageKey => "RowKey";

        /// <summary>
        /// Gets the key for the blob container name in the response messages.
        /// </summary>
        public static string BlobContainerNameMessageKey => "BlobContainerName";

        /// <summary>
        /// Gets the key for the blob name in the response messages.
        /// </summary>
        public static string BlobNameMessageKey => "BlobName";
    }
}
