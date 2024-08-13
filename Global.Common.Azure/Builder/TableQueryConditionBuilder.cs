
namespace Global.Common.Azure
{
    /// <summary>
    /// Provides methods for building query conditions for Azure Table Service.
    /// </summary>
    public class TableQueryConditionBuilder
    {
        /// <summary>
        /// Gets the current culture used for formatting values in query conditions.
        /// </summary>
        public static CultureInfo CurrentCulture { get; } = CultureInfo.InvariantCulture;

        /// <summary>
        /// Builds a query condition for a string property.
        /// </summary>
        /// <param name="propertyName">The name of the property to compare.</param>
        /// <param name="operation">The comparison operation to use.</param>
        /// <param name="value">The string value to compare against.</param>
        /// <returns>A query condition string.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="propertyName"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown when the <paramref name="propertyName"/> is empty or whitespace.</exception>
        public static string BuildQueryCondition(
            string propertyName,
            QueryComparison operation,
            string value)
        {
            return BuildQueryCondition(propertyName, operation, value ?? string.Empty, AzPropertyType.String);
        }

        /// <summary>
        /// Builds a query condition for an integer property.
        /// </summary>
        /// <param name="propertyName">The name of the property to compare.</param>
        /// <param name="operation">The comparison operation to use.</param>
        /// <param name="value">The integer value to compare against.</param>
        /// <returns>A query condition string.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="propertyName"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown when the <paramref name="propertyName"/> is empty or whitespace.</exception>
        public static string BuildQueryCondition(
            string propertyName,
            QueryComparison operation,
            int value)
        {
            return BuildQueryCondition(propertyName, operation, Convert.ToString(value, CurrentCulture), AzPropertyType.Int32);
        }

        /// <summary>
        /// Builds a query condition for a double property.
        /// </summary>
        /// <param name="propertyName">The name of the property to compare.</param>
        /// <param name="operation">The comparison operation to use.</param>
        /// <param name="value">The double value to compare against.</param>
        /// <returns>A query condition string.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="propertyName"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown when the <paramref name="propertyName"/> is empty or whitespace.</exception>
        public static string BuildQueryCondition(
            string propertyName,
            QueryComparison operation,
            double value)
        {
            return BuildQueryCondition(propertyName, operation, Convert.ToString(value, CurrentCulture), AzPropertyType.Double);
        }

        /// <summary>
        /// Builds a query condition for a long property.
        /// </summary>
        /// <param name="propertyName">The name of the property to compare.</param>
        /// <param name="operation">The comparison operation to use.</param>
        /// <param name="value">The long value to compare against.</param>
        /// <returns>A query condition string.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="propertyName"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown when the <paramref name="propertyName"/> is empty or whitespace.</exception>
        public static string BuildQueryCondition(
            string propertyName,
            QueryComparison operation,
            long value)
        {
            return BuildQueryCondition(propertyName, operation, Convert.ToString(value, CurrentCulture), AzPropertyType.Int64);
        }

        /// <summary>
        /// Builds a query condition for a boolean property.
        /// </summary>
        /// <param name="propertyName">The name of the property to compare.</param>
        /// <param name="operation">The comparison operation to use.</param>
        /// <param name="value">The boolean value to compare against.</param>
        /// <returns>A query condition string.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="propertyName"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown when the <paramref name="propertyName"/> is empty or whitespace.</exception>
        public static string BuildQueryCondition(
            string propertyName,
            QueryComparison operation,
            bool value)
        {
            return BuildQueryCondition(propertyName, operation, value ? "true" : "false", AzPropertyType.Boolean);
        }

        /// <summary>
        /// Builds a query condition for a binary property.
        /// </summary>
        /// <param name="propertyName">The name of the property to compare.</param>
        /// <param name="operation">The comparison operation to use.</param>
        /// <param name="value">The byte array value to compare against.</param>
        /// <returns>A query condition string.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="propertyName"/> or the <paramref name="value"/> are null.</exception>
        /// <exception cref="ArgumentException">Thrown when the <paramref name="propertyName"/> is empty or whitespace.</exception>
        public static string BuildQueryCondition(
            string propertyName,
            QueryComparison operation,
            byte[] value)
        {
            AssertHelper.AssertNotNullOrThrow(value, nameof(value));

            var stringBuilder = new StringBuilder();
            foreach (var num in value)
                stringBuilder.AppendFormat("{0:x2}", num);

            return BuildQueryCondition(propertyName, operation, stringBuilder.ToString(), AzPropertyType.Binary);
        }

        /// <summary>
        /// Builds a query condition for a DateTimeOffset property.
        /// </summary>
        /// <param name="propertyName">The name of the property to compare.</param>
        /// <param name="operation">The comparison operation to use.</param>
        /// <param name="value">The DateTimeOffset value to compare against.</param>
        /// <returns>A query condition string.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="propertyName"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown when the <paramref name="propertyName"/> is empty or whitespace.</exception>
        public static string BuildQueryCondition(
            string propertyName,
            QueryComparison operation,
            DateTimeOffset value)
        {
            return BuildQueryCondition(propertyName, operation, value.UtcDateTime.ToString("o", CurrentCulture), AzPropertyType.DateTime);
        }

        /// <summary>
        /// Builds a query condition for a Guid property.
        /// </summary>
        /// <param name="propertyName">The name of the property to compare.</param>
        /// <param name="operation">The comparison operation to use.</param>
        /// <param name="value">The Guid value to compare against.</param>
        /// <returns>A query condition string.</returns>
        /// <exception cref="ArgumentException">Thrown .</exception>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="propertyName"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown when the <paramref name="propertyName"/> is empty, or whitespace,
        /// or when the <paramref name="value"/> value is <see cref="Guid.Empty"/>.</exception>
        public static string BuildQueryCondition(
            string propertyName,
            QueryComparison operation,
            Guid value)
        {
            AssertHelper.AssertNotEmptyOrThrow(value, nameof(value));

            return BuildQueryCondition(propertyName, operation, value.ToString(), AzPropertyType.Guid);
        }

        /// <summary>
        /// Builds a query condition for a specified property, operation, value, and property type.
        /// </summary>
        /// <param name="propertyName">The name of the property to compare.</param>
        /// <param name="operation">The comparison operation to use.</param>
        /// <param name="value">The value to compare against.</param>
        /// <param name="azPropertyType">The type of the property.</param>
        /// <returns>A query condition string.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="propertyName"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown when the <paramref name="propertyName"/> is empty or whitespace.</exception>
        private static string BuildQueryCondition(
            string propertyName,
            QueryComparison operation,
            string value,
            AzPropertyType azPropertyType)
        {
            AssertHelper.AssertNotNullNotWhiteSpaceOrThrow(propertyName, nameof(propertyName));

            string formattedValue;
            switch (azPropertyType)
            {
                case AzPropertyType.Binary:
                    formattedValue = string.Format(CurrentCulture, "X'{0}'", value);
                    break;
                case AzPropertyType.Boolean:
                case AzPropertyType.Int32:
                    formattedValue = value;
                    break;
                case AzPropertyType.Int64:
                    formattedValue = string.Format(CurrentCulture, "{0}L", value);
                    break;
                case AzPropertyType.Double:
                    if (!int.TryParse(value, out int _))
                        formattedValue = value;
                    else
                        formattedValue = string.Format(CurrentCulture, "{0}.0", value);
                    break;
                case AzPropertyType.Guid:
                    formattedValue = string.Format(CurrentCulture, "guid'{0}'", value);
                    break;
                case AzPropertyType.DateTime:
                    formattedValue = string.Format(CurrentCulture, "datetime'{0}'", value);
                    break;
                default:
                    formattedValue = string.Format(CurrentCulture, "'{0}'", value.Replace("'", "''"));
                    break;
            }

            return string.Format(CurrentCulture, "{0} {1} {2}", propertyName, operation, formattedValue);
        }

        /// <summary>
        /// Combines two query conditions using the specified boolean operator.
        /// </summary>
        /// <param name="queryConditionA">The first query condition.</param>
        /// <param name="boolOperator">The boolean operator to use for combining the conditions.</param>
        /// <param name="queryConditionB">The second query condition.</param>
        /// <returns>A combined query condition string.</returns>
        public static string CombineQueryConditions(string queryConditionA, BooleanOperator boolOperator, string queryConditionB)
            => string.Format(CurrentCulture, "({0}) {1} ({2})", queryConditionA, boolOperator, queryConditionB);
    }
}
