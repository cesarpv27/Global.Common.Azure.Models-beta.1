
namespace Global.Common.Azure
{
    internal class TableQueryConstants
    {
        /// <summary>
        /// Returns the defualt 'maxPerPage' value.
        /// </summary>
        public const int DefaultMaxPerPage = 1000;

        /// <summary>
        /// Returns an error message indicating that the parameter must be greater than zero.
        /// </summary>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns>A message indicating that the parameter must be greater than zero.</returns>
        public static string GreaterThanZeroMessage(string paramName)
        {
            return $"The parameter {paramName.WrapInSingleQuotationMarks()} must be greater than zero";
        }

        /// <summary>
        /// Returns the message key of the null response value error.
        /// </summary>
        public static string NullResponseValueMessageKey => "NullResponseValue";

        /// <summary>
        /// Returns an error message indicating that the response contains a null value.
        /// </summary>
        /// <returns>A message indicating that the response contains a null value.</returns>
        public static string ResponseContainsNullValue()
        {
            return "The response contains null value.";
        }

        /// <summary>
        /// Returns the message key of the entity not found error.
        /// </summary>
        public static string EntityNotFoundMessageKey => "EntityNotFound";

        /// <summary>
        /// Returns an error message indicating that the entity with the specified <paramref name="partitionKey"/> 
        /// and <paramref name="rowKey"/> was not found.
        /// </summary>
        /// <returns>A message indicating that the entity with the specified <paramref name="partitionKey"/> 
        /// and <paramref name="rowKey"/> was not found.</returns>
        public static string EntityNotFound(string partitionKey, string rowKey)
        {
            return $"The entity with partition key {partitionKey.WrapInSingleQuotationMarks()} and row key {rowKey.WrapInSingleQuotationMarks()} was not found.";
        }

        /// <summary>
        /// Returns an error message indicating that the parameter 'maxPerPage' is out of range and must be between 0 and 1000.
        /// </summary>
        /// <param name="maxPerPageParamName">The name of the maxPerPage parameter.</param>
        /// <returns>A message indicating that the parameter 'maxPerPage' is out of range and must be between 0 and 1000.</returns>
        public static string MaxPerPageOutOfRange(string maxPerPageParamName = "maxPerPage")
        {
            return $"The parameter {maxPerPageParamName.WrapInSingleQuotationMarks()} is out of range. The value of {maxPerPageParamName.WrapInSingleQuotationMarks()} must be between 0 and 1000";
        }
    }
}
