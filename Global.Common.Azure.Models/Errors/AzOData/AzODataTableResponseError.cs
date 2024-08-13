
namespace Global.Common.Azure.Models
{
    /// <summary>
    /// Represents an error specific to Azure Table operations, with parsing of OData information. 
    /// 
    /// This code uses the Azure SDK provided by Microsoft.
    /// See https://www.nuget.org/packages/Azure.Data.Tables for more details.
    ///
    /// Licensed under the MIT License.
    /// Copyright (c) Microsoft Corporation.
    /// </summary>
    public class AzODataTableResponseError : AzODataResponseError
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AzODataTableResponseError"/> class with the specified message and error code.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <param name="errorCode">The error code.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="message"/> is null.</exception>
        public AzODataTableResponseError(string message, TableErrorCode errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }

        /// <summary>
        /// Gets or sets the error code.
        /// </summary>
        public TableErrorCode ErrorCode { get; set; }

        #region Static

        /// <summary>
        /// Tries to create an instance of <see cref="AzODataTableResponseError"/> from the specified value response.
        /// </summary>
        /// <typeparam name="T">The type of the value response.</typeparam>
        /// <param name="valueResponse">The value response.</param>
        /// <param name="azODataError">When this method returns, contains the created <see cref="AzODataTableResponseError"/> object if the operation succeeded, or null if the operation failed.</param>
        /// <returns>True if the <see cref="AzODataTableResponseError"/> was successfully created; otherwise, false.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="valueResponse"/> is null.</exception>
        public static bool TryCreate<T>(Response<T> valueResponse, out AzODataTableResponseError? azODataError)
        {
            AssertHelper.AssertNotNullOrThrow(valueResponse, nameof(valueResponse));

            return TryCreate(valueResponse.GetRawResponse(), out azODataError);
        }

        /// <summary>
        /// Tries to create an instance of <see cref="AzODataTableResponseError"/> from the specified response.
        /// </summary>
        /// <param name="response">The HTTP response.</param>
        /// <param name="azODataError">When this method returns, contains the created <see cref="AzODataTableResponseError"/> object if the operation succeeded, or null if the operation failed.</param>
        /// <returns>True if the <see cref="AzODataTableResponseError"/> was successfully created; otherwise, false.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="response"/> is null.</exception>
        public static bool TryCreate(Response response, out AzODataTableResponseError? azODataError)
        {
            AssertHelper.AssertNotNullOrThrow(response, nameof(response));

            if (response.ContentStream == null || !(response.ContentStream is MemoryStream))
            {
                azODataError = default;
                return false;
            }

            return TryCreate(response.Content.ToString(), out azODataError);
        }

        /// <summary>
        /// Tries to create an instance of <see cref="AzODataTableResponseError"/> from the specified OData string.
        /// </summary>
        /// <param name="oData">The OData string.</param>
        /// <param name="azODataError">When this method returns, contains the created <see cref="AzODataTableResponseError"/> object if the operation succeeded, or null if the operation failed.</param>
        /// <returns>True if the <see cref="AzODataTableResponseError"/> was successfully created; otherwise, false.</returns>
        public static bool TryCreate(string? oData, out AzODataTableResponseError? azODataError)
        {
            // Version of TryParse from: https://github.com/Azure/azure-sdk-for-net/blob/main/sdk/tables/Azure.Data.Tables/src/TablesRequestFailedDetailsParser.cs
            try
            {
                if (string.IsNullOrEmpty(oData))
                {
                    azODataError = default;
                    return false;
                }
                TableErrorCode? errorCode = default;
                string? message = default;

                var oDataMessageValueSeparator = AzODataResponseErrorHelper.GetMessageValueSeparator();
                using var document = JsonDocument.Parse(oData.Replace("\n", oDataMessageValueSeparator));
                var odataError = document.RootElement.EnumerateObject();
                odataError.MoveNext();
                if (odataError.Current.Name == "odata.error")
                {
                    foreach (var odataErrorProp in odataError.Current.Value.EnumerateObject())
                    {
                        if (odataErrorProp.NameEquals("code"))
                        {
                            errorCode = odataErrorProp.Value.GetString();
                            continue;
                        }
                        if (odataErrorProp.NameEquals("message"))
                        {
                            foreach (var msgProperty in odataErrorProp.Value.EnumerateObject())
                            {
                                if (msgProperty.NameEquals("value"))
                                {
                                    message = msgProperty.Value.GetString()?.Split(oDataMessageValueSeparator, StringSplitOptions.None)[0];

                                    break;
                                }
                            }
                        }
                    }
                }

                if (errorCode == default || string.IsNullOrEmpty(message))
                {
                    azODataError = default;
                    return false;
                }

                azODataError = new(message, errorCode!.Value);
                return true;
            }
            catch
            {
                azODataError = default;
                return false;
            }
        }

        /// <summary>
        /// Tries to create an instance of <see cref="AzODataTableResponseError"/> from the specified exception.
        /// </summary>
        /// <typeparam name="TEx">The type of the exception.</typeparam>
        /// <param name="exception">The exception.</param>
        /// <param name="azODataError">When this method returns, contains the created <see cref="AzODataTableResponseError"/> object if the operation succeeded, or null if the operation failed.</param>
        /// <returns>True if the <see cref="AzODataTableResponseError"/> was successfully created; otherwise, false.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="exception"/> is null.</exception>
        public static bool TryCreate<TEx>(TEx exception, out AzODataTableResponseError? azODataError) where TEx : RequestFailedException
        {
            AssertHelper.AssertNotNullOrThrow(exception, nameof(exception));

            try
            {
                TableErrorCode? errorCode = exception.ErrorCode;

                if (errorCode == default)
                {
                    azODataError = default;
                    return false;
                }

                var oDataMessageValueSeparator = AzODataResponseErrorHelper.GetMessageValueSeparator();
                string message = exception.Message.Replace("\n", oDataMessageValueSeparator).Split(oDataMessageValueSeparator, StringSplitOptions.None)[0];

                azODataError = new(message, errorCode!.Value);
                return true;
            }
            catch
            {
                azODataError = default;
                return false;
            }
        }

        #endregion
    }
}
