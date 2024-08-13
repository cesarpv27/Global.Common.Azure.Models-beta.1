
namespace Global.Common.Azure
{
    /// <summary>
    /// Provides constant values and methods for text resources related to Azure Table Service operations.
    /// </summary>
    internal static class TableConstants
    {
        /// <summary>
        /// Represents the constant name of the partition key.
        /// </summary>
        public static string PartitionKeyName => "PartitionKey";

        /// <summary>
        /// Represents the constant name of the row key.
        /// </summary>
        public static string RowKeyName => "RowKey";

        /// <summary>
        /// Represents the constant name of the timestamp.
        /// </summary>
        public static string TimestampName => "Timestamp";

        /// <summary>
        /// Represents the constant name of the table name.
        /// </summary>
        public static string TableName => "TableName";

        /// <summary>
        /// Represents the constant name of the account name.
        /// </summary>
        public static string AccountName => "AccountName";

        /// <summary>
        /// Message indicating that multiple entities were found.
        /// </summary>
        public static string MultipleEntitiesFound => "Multiple entities found";

        /// <summary>
        /// Message indicating that the table name is null or empty.
        /// </summary>
        public static string TableNameIsNullOrEmptyMessage => $"{TableName.WrapInSingleQuotationMarks()} is null or empty";

        /// <summary>
        /// Message indicating that the account name is null or empty.
        /// </summary>
        public static string AccountNameIsNullOrEmptyMessage => $"{AccountName.WrapInSingleQuotationMarks()} is null or empty";

        /// <summary>
        /// Prefix for exception messages.
        /// </summary>
        public static string ExceptionMessagePrefix => "Exception message: ";

        /// <summary>
        /// Represents the constant of the default take value.
        /// </summary>
        public const int DefaultTake = 1000;
    }
}
