
namespace Global.Common.Azure.Models
{
    /// <inheritdoc cref="IAzGlobalValueResponse{T, TAzureResponse, TEx}"/>
    public class AzGlobalValueResponse<T, TAzureResponse, TEx> : AzGlobalResponse<TAzureResponse, TEx>, IAzGlobalValueResponse<T, TAzureResponse, TEx>
        where TAzureResponse : IAzureResponse
        where TEx : Exception
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AzGlobalValueResponse{T, TAzureResponse, TEx}"/> class.
        /// </summary>
        protected AzGlobalValueResponse() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzGlobalValueResponse{T, TAzureResponse, TEx}"/> class with the specified <paramref name="status"/>.
        /// </summary>
        protected AzGlobalValueResponse(ResponseStatus status) : base(status) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzGlobalValueResponse{T, TAzureResponse, TEx}"/> class with the specified <paramref name="status"/> 
        /// and <paramref name="value"/>.
        /// If the <paramref name="status"/> is <see cref="ResponseStatus.Success"/>, 
        /// then the <paramref name="value"/> must be specified.
        /// If the <paramref name="status"/> is <see cref="ResponseStatus.Success"/> 
        /// and the <paramref name="value"/> was not specified, an <see cref="ArgumentNullException"/> will be thrown.
        /// If the <paramref name="status"/> is <see cref="ResponseStatus.Failure"/>, 
        /// then the <paramref name="value"/> must be null.
        /// If the <paramref name="status"/> is <see cref="ResponseStatus.Failure"/> and the <paramref name="value"/> was specified, 
        /// an <see cref="ArgumentException"/> will be thrown.
        /// </summary>
        /// <param name="status">The status of the response.</param>
        /// <param name="value">The value associated with the response.</param>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="status"/> is <see cref="ResponseStatus.Success"/> 
        /// and the <paramref name="value"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if the <paramref name="status"/> is <see cref="ResponseStatus.Failure"/> 
        /// and the <paramref name="value"/> was specified.</exception>
        public AzGlobalValueResponse(ResponseStatus status, T? value) : this(status)
        {
            if (status == ResponseStatus.Success)
                AssertHelper.AssertNotNullOrThrow(value, nameof(value));
            else
            if (status == ResponseStatus.Failure && !IsDefaultValue(value))
                throw new ArgumentException(GlobalResponseConstants.ResponseStatusFailureAndValueSpecified(nameof(status), nameof(value)));

            Value = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzGlobalValueResponse{T, TAzureResponse, TEx}"/> class with the specified <paramref name="value"/> 
        /// and a status of <see cref="ResponseStatus.Success"/>.
        /// </summary>
        /// <param name="value">The value associated with the response.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is null.</exception>
        public AzGlobalValueResponse(T value) : base(ResponseStatus.Success)
        {
            AssertHelper.AssertNotNullOrThrow(value, nameof(value));

            Value = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzGlobalValueResponse{T, TAzureResponse, TEx}"/> class with the specified <paramref name="azureResponse"/> 
        /// and <paramref name="value"/>.
        /// The status will be set to <see cref="ResponseStatus.Success"/> if the property 'IsError' in <paramref name="azureResponse"/> is <c>false</c>.
        /// The status set to <see cref="ResponseStatus.Failure"/> if the property 'IsError' in <paramref name="azureResponse"/> is <c>true</c>.
        /// If the property 'IsError' in <paramref name="azureResponse"/> is <c>false</c>, then the <paramref name="value"/> must be specified.
        /// If the property 'IsError' in <paramref name="azureResponse"/> is <c>false</c> and the <paramref name="value"/> was not specified, 
        /// an <see cref="ArgumentNullException"/> will be thrown.
        /// If the property 'IsError' in <paramref name="azureResponse"/> is <c>true</c>, then the <paramref name="value"/> must be null.
        /// If the property 'IsError' in <paramref name="azureResponse"/> is <c>true</c> and the <paramref name="value"/> was specified, 
        /// an <see cref="ArgumentException"/> will be thrown.
        /// </summary>
        /// <param name="azureResponse">The Azure response.</param>
        /// <param name="value">The value associated with the response.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="azureResponse"/> is null, or 
        /// if the property 'IsError' in <paramref name="azureResponse"/> is <c>false</c> 
        /// and the <paramref name="value"/> was not specified.</exception>
        /// <exception cref="ArgumentException">Thrown if the property 'IsError' in <paramref name="azureResponse"/> is <c>true</c> 
        /// and the <paramref name="value"/> was specified</exception>
        public AzGlobalValueResponse(TAzureResponse azureResponse, T? value) : base(azureResponse)
        {
            if (!azureResponse.IsError)
                AssertHelper.AssertNotNullOrThrow(value, nameof(value));
            else
            if (azureResponse.IsError && !IsDefaultValue(value))
                throw new ArgumentException(AzureResponseConstants.AzResponseIsNotInErrorAndValueSpecified(
                    $"{typeof(TAzureResponse).FullName}.{nameof(azureResponse.IsError)}", nameof(value)));

            Value = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzGlobalValueResponse{T, TAzureResponse, TEx}"/> class with the specified <paramref name="exception"/> 
        /// and the Status set to <see cref="ResponseStatus.Failure"/>.
        /// </summary>
        /// <param name="exception">The exception associated with the response.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="exception"/> is null.</exception>
        public AzGlobalValueResponse(TEx exception) : base(exception) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzGlobalValueResponse{T, TAzureResponse, TEx}"/> class from the data in <paramref name="globalResponse"/>. 
        /// The <paramref name="globalResponse"/> specified must contain the Status set to <see cref="ResponseStatus.Failure"/>. 
        /// If the 'Status' in <paramref name="globalResponse"/> is not <see cref="ResponseStatus.Failure"/>, 
        /// an <see cref="ArgumentException"/> will be thrown. 
        /// The new instance contains the same status and messages of the <paramref name="globalResponse"/>, 
        /// but the message keys may have been modified with suffix. 
        /// </summary>
        /// <param name="globalResponse">The response from where the new instance will be generated.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="globalResponse"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if the 'Status' in <paramref name="globalResponse"/> is not <see cref="ResponseStatus.Failure"/>.</exception>
        protected internal AzGlobalValueResponse(IGlobalResponse globalResponse) : this(ResponseStatus.Failure, default)
        {
            AssertResponseHelper.AssertFailureNotNullOrThrow(globalResponse, nameof(globalResponse));

            AddMessagesFrom(globalResponse, KeyExistAction.Rename);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzGlobalValueResponse{T, TAzureResponse, TEx}"/> class from the data in <paramref name="globalResponse"/>. 
        /// The <paramref name="globalResponse"/> specified must contain the Status set to <see cref="ResponseStatus.Failure"/>. 
        /// If the 'Status' in <paramref name="globalResponse"/> is not <see cref="ResponseStatus.Failure"/>, 
        /// an <see cref="ArgumentException"/> will be thrown. 
        /// The new instance contains the same status, exception and messages of the <paramref name="globalResponse"/>, 
        /// but the message keys may have been modified with suffix. 
        /// </summary>
        /// <param name="globalResponse">The response from where the new instance will be generated.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="globalResponse"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if the 'Status' in <paramref name="globalResponse"/> is not <see cref="ResponseStatus.Failure"/>.</exception>
        protected internal AzGlobalValueResponse(IGlobalResponse<TEx> globalResponse) : this(ResponseStatus.Failure, default)
        {
            AssertResponseHelper.AssertFailureNotNullOrThrow(globalResponse, nameof(globalResponse));

            Exception = globalResponse.Exception;

            AddMessagesFrom(globalResponse, KeyExistAction.Rename);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzGlobalValueResponse{T, TAzureResponse, TEx}"/> class from the data in <paramref name="azGlobalResponse"/>.
        /// The <paramref name="azGlobalResponse"/> specified must contain the Status set to <see cref="ResponseStatus.Failure"/>. 
        /// If the 'Status' in <paramref name="azGlobalResponse"/> is not <see cref="ResponseStatus.Failure"/>, 
        /// an <see cref="ArgumentException"/> will be thrown. 
        /// The new instance contains the same status, exception, error and messages of the <paramref name="azGlobalResponse"/> specified, 
        /// but the message keys may have been modified with suffix. 
        /// When <see cref="IAzGlobalResponse{TAzureResponse, TEx}.AzureResponse"/> is specified, 
        /// if 'IsError' in <see cref="IAzGlobalResponse{TAzureResponse, TEx}.AzureResponse"/> is <c>false</c> an <see cref="ArgumentException"/> will be thrown. 
        /// </summary>
        /// <param name="azGlobalResponse">The response from where the new instance will be generated.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="azGlobalResponse"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if the 'Status' in <paramref name="azGlobalResponse"/> is not <see cref="ResponseStatus.Failure"/>,
        /// or if 'IsError' in <see cref="IAzGlobalResponse{TAzureResponse, TEx}.AzureResponse"/> is <c>false</c>.</exception>
        protected internal AzGlobalValueResponse(IAzGlobalResponse<TAzureResponse, TEx> azGlobalResponse) : base(azGlobalResponse)
        {
            AssertResponseHelper.AssertFailureNotNullOrThrow(azGlobalResponse, nameof(azGlobalResponse));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzGlobalValueResponse{T, TAzureResponse, TEx}"/> class from the data in <paramref name="azGlobalValueResponse"/>. 
        /// The new instance contains the same status, exception, error and messages of the <paramref name="azGlobalValueResponse"/> specified, 
        /// but the message keys may have been modified with suffix. 
        /// </summary>
        /// <param name="azGlobalValueResponse">The response from where the new instance will be generated.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="azGlobalValueResponse"/> is null.</exception>
        protected internal AzGlobalValueResponse(IAzGlobalValueResponse<T, TAzureResponse, TEx> azGlobalValueResponse) : base(azGlobalValueResponse)
        {
            Value = azGlobalValueResponse.Value;
        }

        #endregion

        #region Properties

        /// <inheritdoc/>
        public virtual bool HasValue => !IsDefaultValue(Value);

        /// <inheritdoc/>
        public virtual T? Value { get; protected set; }

        #endregion

        #region IVerboseResponse implementation

        /// <inheritdoc/>
        public override Dictionary<string, string> BuildVerbose()
        {
            var verboseResponse = base.BuildVerbose();
            verboseResponse.AddOrRenameKey(nameof(HasValue), HasValue.ToString());
            if (HasValue)
            {
                verboseResponse.AddOrRenameKey($"{nameof(Value)}{GlobalResponseConstants.DefaultTypeMessageKeySuffix}",
                    Value!.GetType().FullName ?? GlobalResponseConstants.DefaultTypeFullNameUndefinedMessage);
                verboseResponse.AddOrRenameKey($"{nameof(Value)}", Value.ToString() ?? GlobalResponseConstants.DefaultValueToStringUndefinedMessage);
            }
            
            return verboseResponse;
        }

        #endregion

        #region Protected methods

        /// <summary>
        /// Determines whether the specified value is the default value for its type.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <returns><c>true</c> if the value is the default value for its type; otherwise, <c>false</c>.</returns>
        protected bool IsDefaultValue(T? value)
        {
            if (value == null)
                return true;

            return Equals(value, default(T));
        }

        #endregion
    }
}
