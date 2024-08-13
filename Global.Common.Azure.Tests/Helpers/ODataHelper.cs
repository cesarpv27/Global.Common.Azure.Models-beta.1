
namespace Global.Common.Azure.Tests.Helpers
{
    internal static class ODataHelper
    {
        public static string tableAlreadyExistMessageValue = "The table specified already exists.\nRequestId:329e77ac-5002-0001-6f7f-793522000000\nTime:2024-03-18T21:58:20.7787264Z";

        public static string TableAlreadyExistsOData = "{\"odata.error\":{\"code\":\"TableAlreadyExists\",\"message\":{\"lang\":\"en-US\",\"value\":\"" + tableAlreadyExistMessageValue + "\"}}}";

        public static ODataTest OdataErrorTestInput()
        {
            return new ODataTest { OData = TableAlreadyExistsOData, Message = tableAlreadyExistMessageValue, ErrorCode = TableErrorCode.TableAlreadyExists };
        }
    }
}
