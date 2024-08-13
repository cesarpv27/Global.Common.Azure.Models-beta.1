
namespace Global.Common.Azure.Models.Constants
{
    internal static class AzAssertHelperConstants
    {
        public static readonly string table = "table";
        public static readonly string blobContainer = "blob container";
        public static readonly string blob = "blob";
        public static string TheNameOfIsNullOrEmpty(string value)
        {
            return $"The name of the {value} is null or empty.";
        }

        public static string TheLengthOfIsOutOfRange(string value, string complementaryValue, int rangeFrom, int rangeTo)
        {
            return $"The length of the name of the {value} is out of range. {complementaryValue} must be from {rangeFrom} to {rangeTo} characters long.";
        }

        public static string TheNameOfHasAnIncorrectFormat(string value, string suffix)
        {
            return $"The name of the {value} has an incorrect format. {suffix}";
        }
    }
}
