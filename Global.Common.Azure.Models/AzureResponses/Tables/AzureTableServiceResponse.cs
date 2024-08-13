
namespace Global.Common.Azure.Models
{
    /// <inheritdoc cref="IAzureTableServiceResponse"/>
    public class AzureTableServiceResponse : AzureResponse, IAzureTableServiceResponse
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureTableServiceResponse"/> class with the specified Azure <paramref name="response"/>.
        /// </summary>
        /// <param name="response">The Azure response associated with the error.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="response"/> is null.</exception>
        public AzureTableServiceResponse(Response response) : base(response)
        {
            if (IsError)
                if (AzODataTableResponseError.TryCreate(response, out AzODataTableResponseError? azODataTableResponseError))
                {
                    Message = azODataTableResponseError!.Message;
                    ErrorCode = azODataTableResponseError!.ErrorCode;
                }
                else
                    Message = AzureResponseConstants.ValuesNotRecoveredFromAzureResponse(nameof(ErrorCode), nameof(Message));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureTableServiceResponse"/> class with the specified Azure <paramref name="exception"/> 
        /// and IsError set to true.
        /// </summary>
        /// <param name="exception">The exception associated with the error.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="exception"/> is null.</exception>
        public AzureTableServiceResponse(RequestFailedException exception) : base(exception)
        {
            if (AzODataTableResponseError.TryCreate(exception, out AzODataTableResponseError? azODataTableResponseError))
            {
                Message = azODataTableResponseError!.Message;
                ErrorCode = azODataTableResponseError!.ErrorCode;
            }
            else
                Message = AzureResponseConstants.ValuesNotRecoveredFromAzureResponse(nameof(ErrorCode), nameof(Message));
        }

        #endregion

        #region Static

        /// <summary>
        /// Creates an instance of <see cref="AzureTableServiceResponse"/> based on the provided Azure <see cref="Response"/>.
        /// </summary>
        /// <param name="response">The Azure response.</param>
        /// <returns>An instance of <see cref="AzureTableServiceResponse"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="response"/> is null.</exception>
        public static AzureTableServiceResponse Create(Response response)
        {
            return new AzureTableServiceResponse(response);
        }

        /// <summary>
        /// Creates an instance of <see cref="AzureTableServiceResponse"/> based on the provided Azure <see cref="Response{T}"/>.
        /// </summary>
        /// <param name="response">The Azure response.</param>
        /// <returns>An instance of <see cref="AzureTableServiceResponse"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="response"/> is null.</exception>
        public static AzureTableServiceResponse Create<T>(Response<T> response)
        {
            AssertHelper.AssertNotNullOrThrow(response, nameof(response));

            return Create(response.GetRawResponse());
        }

        /// <summary>
        /// Creates an instance of <see cref="AzureTableServiceResponse"/> based on the provided Azure <paramref name="exception"/>.
        /// </summary>
        /// <param name="exception">The Azure exception.</param>
        /// <returns>An instance of <see cref="AzureTableServiceResponse"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="exception"/> is null.</exception>
        public static AzureTableServiceResponse Create(RequestFailedException exception)
        {
            return new AzureTableServiceResponse(exception);
        }

        #endregion

        #region Properties

        /// <inheritdoc/>
        public bool HasErrorCode => ErrorCode != default;

        /// <inheritdoc/>
        public virtual TableErrorCode? ErrorCode { get; protected internal set; }

        #endregion

        #region IVerboseResponse implementation

        /// <inheritdoc/>
        public override Dictionary<string, string> BuildVerbose()
        {
            var verboseResponse = base.BuildVerbose();
            verboseResponse.AddOrRenameKey(nameof(HasErrorCode), HasErrorCode.ToString());
            if (HasErrorCode)
                verboseResponse.AddOrRenameKey(nameof(ErrorCode),
                    $"{typeof(TableErrorCode).FullName ?? GlobalResponseConstants.DefaultTypeFullNameUndefinedMessage.WrapInSingleQuotationMarks()}.{ErrorCode.ToString()}");

            return verboseResponse;
        }

        #endregion
    }
}
