
namespace Global.Common.Azure
{
    /// <summary>
    /// Provides methods to build <see cref="TableQueryFilter"/> instances for Azure Table Service.
    /// </summary>
    public static class TableQueryFilterBuilder
    {
        /// <summary>
        /// Builds a <see cref="TableQueryFilter"/> for the PartitionKey property using the specified comparison operation and value.
        /// </summary>
        /// <param name="operation">The comparison operation to use.</param>
        /// <param name="value">The value to compare against.</param>
        /// <returns>A <see cref="TableQueryFilter"/> for the PartitionKey property.</returns>
        public static TableQueryFilter BuildPartitionKeyFilter(
            QueryComparison operation,
            string value)
        {
            return TableConstants.PartitionKeyName.CreateTableQueryFilter(operation, value);
        }

        /// <summary>
        /// Builds a <see cref="TableQueryFilter"/> for the RowKey property using the specified comparison operation and value.
        /// </summary>
        /// <param name="operation">The comparison operation to use.</param>
        /// <param name="value">The value to compare against.</param>
        /// <returns>A <see cref="TableQueryFilter"/> for the RowKey property.</returns>
        public static TableQueryFilter BuildRowKeyFilter(
            QueryComparison operation,
            string value)
        {
            return TableConstants.RowKeyName.CreateTableQueryFilter(operation, value);
        }

        /// <summary>
        /// Builds a <see cref="TableQueryFilter"/> for the Timestamp property using the specified comparison operation and value.
        /// </summary>
        /// <param name="operation">The comparison operation to use.</param>
        /// <param name="value">The DateTime value to compare against.</param>
        /// <returns>A <see cref="TableQueryFilter"/> for the Timestamp property.</returns>
        public static TableQueryFilter BuildTimestampFilter(
            QueryComparison operation,
            DateTime value)
        {
            return TableConstants.TimestampName.CreateTableQueryFilter(operation, value);
        }
    }
}
