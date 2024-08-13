namespace Global.Common.Azure.Models.Helpers
{
    internal static class AzODataResponseErrorHelper
    {
        public static string GetMessageValueSeparator()
        {
            return $"--{Environment.TickCount}--";
        }
    }
}
