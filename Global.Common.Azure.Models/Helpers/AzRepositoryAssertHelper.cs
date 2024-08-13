
namespace Global.Common.Azure.Models.Helpers
{
    /// <summary>
    /// Helper class for assertion operations in azure repositories. 
    /// </summary>
    public static class AzRepositoryAssertHelper
    {
        /// <summary>
        /// Asserts whether the provided <paramref name="partitionKey"/> and <paramref name="rowKey"/> are not null or empty; otherwise, throws an exception.
        /// </summary>
        /// <param name="partitionKey">The partition key to assert.</param>
        /// <param name="rowKey">The row key to assert.</param>
        /// <param name="partitionKeyParamName">The parameter name for the <paramref name="partitionKey"/> (optional).</param>
        /// <param name="rowKeyParamName">The parameter name for the <paramref name="rowKey"/> (optional).</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="partitionKey"/> or <paramref name="rowKey"/> are null or empty.</exception>
        public static void AssertPartitionKeyRowKeyOrThrow(
            string partitionKey,
            string rowKey,
            string? partitionKeyParamName = null,
            string? rowKeyParamName = null)
        {
            AssertHelper.AssertNotNullNotEmptyOrThrow(partitionKey, partitionKeyParamName ?? nameof(partitionKey));
            AssertHelper.AssertNotNullNotEmptyOrThrow(rowKey, rowKeyParamName ?? nameof(rowKey));
        }

        /// <summary>
        /// Asserts whether the provided <paramref name="entity"/> are not null, and the PartitionKey or the RowKey are not null or empty in <paramref name="entity"/>; otherwise, throws an exception.
        /// </summary>
        /// <param name="entity">The entity to assert.</param>
        /// <param name="entityParamName">The parameter name for the <paramref name="entity"/> (optional).</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="entity"/> ir null, the PartitionKey, or the RowKey are null or empty in <paramref name="entity"/>.</exception>
        public static void AssertEntityOrThrow<T>(T entity, string? entityParamName = null) where T : ITableEntity
        {
            AssertHelper.AssertNotNullOrThrow(entity, entityParamName ?? nameof(entity));

            AssertPartitionKeyRowKeyOrThrow(entity.PartitionKey, entity.RowKey, nameof(entity.PartitionKey), nameof(entity.RowKey));
        }

        /// <summary>
        /// Asserts whether the provided <paramref name="blobContainerName"/> and <paramref name="blobName"/> are in the correct format for Azure Storage; 
        /// otherwise, throws an exception.
        /// </summary>
        /// <param name="blobContainerName">The blob container name to assert.</param>
        /// <param name="blobName">The blob name to assert. Must include the directory path of the blob.</param>
        /// <param name="blobContainerNameParamName">The parameter name for the <paramref name="blobContainerName"/> (optional).</param>
        /// <param name="blobNameParamName">The parameter name for the <paramref name="blobName"/> (optional).</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="blobContainerName"/> or <paramref name="blobName"/> have an incorrect format for Azure Blob Storage.</exception>
        public static void AssertBlobContainerNameBlobNameOrThrow(
            string blobContainerName,
            string blobName,
            string? blobContainerNameParamName = null,
            string? blobNameParamName = null)
        {
            AzAssertHelper.AssertBlobContainerNameOrThrow(blobContainerName, blobContainerNameParamName ?? nameof(blobContainerName));
            AzAssertHelper.AssertDirectoryAndBlobNameOrThrow(blobName, blobNameParamName ?? nameof(blobName));
        }
    }
}
