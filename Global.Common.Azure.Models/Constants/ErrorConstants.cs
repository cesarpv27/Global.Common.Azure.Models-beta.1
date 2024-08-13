
namespace Global.Common.Azure.Models.Constants
{
    internal static class AzODataConstants
    {
        public static string ErrorCode => "Code";
        public static string ErrorMessage => "Message";
               
        public static string ErrorPropertyKey => "error";
        public static string DetailPropertyKey => "detail";
        public static string MessagePropertyKey => "message";
        public static string CodePropertyKey => "code";
               
        public static string BlobErrorCodeHeaderName => "x-ms-error-code";
    }

    internal static class AzureResponseConstants
    {
        public static string ValuesNotRecoveredFromAzureResponse(string paramName1, string paramName2)
        {
            return $"The {paramName1.WrapInSingleQuotationMarks()} and the {paramName2.WrapInSingleQuotationMarks()} could not be recovered from Azure Response";
        }

        public static string StatusIsNotHttpStatusCode(string statusPropName, string paramName)
        {
            return $"The value of {statusPropName.WrapInSingleQuotationMarks} in {paramName.WrapInSingleQuotationMarks()} is not an {nameof(HttpStatusCode)}.";
        }

        public static string AzResponseIsNotInErrorAndValueSpecified(string azureResponseParamName, string valueParamName)
        {
            return $"The {azureResponseParamName.WrapInSingleQuotationMarks()} is 'True' and the {valueParamName.WrapInSingleQuotationMarks()} was specified.";
        }

        public static string StatusDoesNotMatchWithAzureResponse(string statusParamName, string azureResponseParamName)
        {
            return $"The {statusParamName.WrapInSingleQuotationMarks()} specified does not match with the value of the 'IsError' property in {azureResponseParamName.WrapInSingleQuotationMarks()}.";
        }

        public static string ResponseStatusIsNotFailure(string responseParamName)
        {
            return $"The Status in the {responseParamName} is not {Enum.GetName(typeof(ResponseStatus), ResponseStatus.Failure)}";
        }
    }
}
