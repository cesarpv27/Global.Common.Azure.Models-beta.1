using System.Xml.Linq;

namespace Global.Common.Azure.Models
{
    /// <summary>
    /// Represents an error specific to Azure Blob operations, with parsing of OData information.
    /// 
    /// This code uses the Azure SDK provided by Microsoft.
    /// See https://www.nuget.org/packages/Azure.Storage.Blobs for more details.
    ///
    /// Licensed under the MIT License.
    /// Copyright (c) Microsoft Corporation.
    /// </summary>
    public class AzODataBlobResponseError : AzODataResponseError
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AzODataBlobResponseError"/> class with the specified message and error code.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <param name="errorCode">The error code.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="message"/> is null.</exception>
        public AzODataBlobResponseError(string message, BlobErrorCode errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }

        /// <summary>
        /// Gets or sets the error code associated with the error.
        /// </summary>
        public BlobErrorCode ErrorCode { get; set; }

        #region Static

        /// <summary>
        /// Tries to create an instance of <see cref="AzODataBlobResponseError"/> from the specified <see cref="Response{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of the response content.</typeparam>
        /// <param name="valueResponse">The response containing the error.</param>
        /// <param name="azODataError">When this method returns, contains the error if the creation was successful; otherwise, <see langword="null"/>.</param>
        /// <returns><see langword="true"/> if an <see cref="AzODataBlobResponseError"/> was successfully created; otherwise, <see langword="false"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="valueResponse"/> is null.</exception>
        public static bool TryCreate<T>(Response<T> valueResponse, out AzODataBlobResponseError? azODataError)
        {
            AssertHelper.AssertNotNullOrThrow(valueResponse, nameof(valueResponse));

            return TryCreate(valueResponse.GetRawResponse(), out azODataError);
        }

        /// <summary>
        /// Tries to create an instance of <see cref="AzODataBlobResponseError"/> from the specified <see cref="Response"/>.
        /// </summary>
        /// <param name="response">The HTTP response.</param>
        /// <param name="azODataError">When this method returns, contains the error if the creation was successful; otherwise, <see langword="null"/>.</param>
        /// <returns><see langword="true"/> if an <see cref="AzODataBlobResponseError"/> was successfully created; otherwise, <see langword="false"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="response"/> is null.</exception>
        public static bool TryCreate(Response response, out AzODataBlobResponseError? azODataError)
        {
            // Version of TryParse from: https://github.com/Azure/azure-sdk-for-net/blob/Azure.Storage.Blobs_12.20.0-beta.1/sdk/storage/Azure.Storage.Common/src/Shared/StorageRequestFailedDetailsParser.cs
            
            AssertHelper.AssertNotNullOrThrow(response, nameof(response));

            try
            {
                BlobErrorCode? errorCode = default;
                string? message = default;

                if (response.ContentStream is { } contentStream && response.Headers.ContentType is not null)
                {
                    var position = contentStream.CanSeek ? contentStream.Position : 0;
                    try
                    {
                        if (contentStream.CanSeek)
                        {
                            contentStream.Position = 0;
                        }
                        // XML body
                        if (response.Headers.ContentType.Contains(GlobalConstants.ContentTypeApplicationXml))
                        {
                            XDocument xml = XDocument.Load(contentStream);
                            errorCode = xml.Root!.Element(AzODataConstants.ErrorCode)!.Value;
                            message = xml.Root.Element(AzODataConstants.ErrorMessage)!.Value;
                        }

                        // Json body
                        if (response.Headers.ContentType.Contains(GlobalConstants.ContentTypeApplicationJson))
                        {
                            using JsonDocument json = JsonDocument.Parse(contentStream);
                            JsonElement errorElement = json.RootElement.GetProperty(AzODataConstants.ErrorPropertyKey);

                            errorCode = errorElement.GetProperty(AzODataConstants.CodePropertyKey).GetString();
                            message = errorElement.GetProperty(AzODataConstants.MessagePropertyKey).GetString();
                        }
                    }
                    finally
                    {
                        if (contentStream.CanSeek)
                            contentStream.Position = position;
                    }
                }
                // No response body.
                // The other headers will appear in the "Headers" section of the Exception message.
                else if (response.Headers.TryGetValue(AzODataConstants.BlobErrorCodeHeaderName, out string? value))
                {
                    errorCode = value;
                    message = value;
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
        /// Tries to create an instance of <see cref="AzODataBlobResponseError"/> from the specified <typeparamref name="TEx"/> exception.
        /// </summary>
        /// <typeparam name="TEx">The type of the exception.</typeparam>
        /// <param name="exception">The exception containing the error information.</param>
        /// <param name="azODataError">When this method returns, contains the error if the creation was successful; otherwise, <see langword="null"/>.</param>
        /// <returns><see langword="true"/> if an <see cref="AzODataBlobResponseError"/> was successfully created; otherwise, <see langword="false"/>.</returns>
        /// <remarks>
        /// The exception should be of type <see cref="RequestFailedException"/>.
        /// </remarks>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="exception"/> is null.</exception>
        public static bool TryCreate<TEx>(TEx exception, out AzODataBlobResponseError? azODataError) where TEx : RequestFailedException
        {
            AssertHelper.AssertNotNullOrThrow(exception, nameof(exception));
            try
            {
                BlobErrorCode? errorCode = exception.ErrorCode;

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
