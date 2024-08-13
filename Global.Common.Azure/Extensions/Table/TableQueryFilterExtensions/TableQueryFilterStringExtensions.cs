
namespace Global.Common.Azure.Extensions.Table.TableQueryFilterExtensions
{
    internal static class TableQueryFilterStringExtensions
    {
        public static TableQueryFilter CreateTableQueryFilter(
            this string @this,
            QueryComparison operation,
            string value)
        {
            return new TableQueryFilter(@this, operation, value);
        }

        public static TableQueryFilter CreateTableQueryFilter(
            this string @this,
            QueryComparison operation,
            DateTime value)
        {
            return new TableQueryFilter(@this, operation, value);
        }

        internal static string AddLastChar(this string @this)
        {
            return @this + "ÿ";
        }
    }
}
