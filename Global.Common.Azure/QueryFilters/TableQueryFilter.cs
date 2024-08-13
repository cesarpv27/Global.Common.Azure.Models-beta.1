
namespace Global.Common.Azure
{
    /// <summary>
    /// Represents a filter to be applied on a table query for data retrieval.
    /// </summary>
    public struct TableQueryFilter
    {
        private readonly string _queryCondition;

        /// <summary>
        /// Initializes a new instance of the <see cref="TableQueryFilter"/> struct with a specified query condition.
        /// </summary>
        /// <param name="queryCondition">The query condition.</param>
        private TableQueryFilter(string queryCondition)
        {
            _queryCondition = queryCondition;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TableQueryFilter"/> struct with a specified property name, comparison operation, and string value.
        /// </summary>
        /// <param name="propertyName">The name of the property to filter on.</param>
        /// <param name="operation">The comparison operation to use.</param>
        /// <param name="value">The value to compare against.</param>
        public TableQueryFilter(string propertyName, QueryComparison operation, string value)
        {
            _queryCondition = TableQueryConditionBuilder.BuildQueryCondition(propertyName, operation, value);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TableQueryFilter"/> struct with a specified property name, comparison operation, and DateTime value.
        /// </summary>
        /// <param name="propertyName">The name of the property to filter on.</param>
        /// <param name="operation">The comparison operation to use.</param>
        /// <param name="value">The DateTime value to compare against.</param>
        public TableQueryFilter(string propertyName, QueryComparison operation, DateTime value)
        {
            _queryCondition = TableQueryConditionBuilder.BuildQueryCondition(propertyName, operation, value);
        }

        /// <summary>
        /// Combines the current table query filter with another table query filter using the AND logical operator.
        /// </summary>
        /// <param name="other">The other <see cref="TableQueryFilter"/> to combine with.</param>
        /// <returns>A new <see cref="TableQueryFilter"/> representing the combined table query filter.</returns>
        public TableQueryFilter And(TableQueryFilter other)
        {
            return new TableQueryFilter(TableQueryConditionBuilder.CombineQueryConditions(_queryCondition, BooleanOperator.and, other._queryCondition));
        }

        /// <summary>
        /// Combines the current table query filter with another table query filter using the OR logical operator.
        /// </summary>
        /// <param name="other">The other <see cref="TableQueryFilter"/> to combine with.</param>
        /// <returns>A new <see cref="TableQueryFilter"/> representing the combined table query filter.</returns>
        public TableQueryFilter Or(TableQueryFilter other)
        {
            return new TableQueryFilter(TableQueryConditionBuilder.CombineQueryConditions(_queryCondition, BooleanOperator.or, other._queryCondition));
        }

        /// <summary>
        /// Returns a string that represents the current table query filter.
        /// </summary>
        /// <returns>A string representing the current table query filter.</returns>
        public override string ToString()
        {
            return _queryCondition;
        }
    }
}
