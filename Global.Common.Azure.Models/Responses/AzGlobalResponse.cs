
namespace Global.Common.Azure.Models
{
    /// <inheritdoc cref="IAzGlobalResponse{TAzureResponse, TEx}"/>
    public class AzGlobalResponse<TAzureResponse, TEx> : GlobalResponse<TEx>, IAzGlobalResponse<TAzureResponse, TEx>
        where TAzureResponse : IAzureResponse
        where TEx : Exception
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AzGlobalResponse{TAzureResponse, TEx}"/> class.
        /// </summary>
        protected AzGlobalResponse() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzGlobalResponse{TAzureResponse, TEx}"/> class with the specified <paramref name="status"/>.
        /// </summary>
        /// <param name="status">The status of the response.</param>
        public AzGlobalResponse(ResponseStatus status) : base(status) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzGlobalResponse{TAzureResponse, TEx}"/> class with the provided <paramref name="azureResponse"/> 
        /// and the Status set to <see cref="ResponseStatus.Success"/> if the property 'IsError' in <paramref name="azureResponse"/> is <c>false</c>, 
        /// or the Status set to <see cref="ResponseStatus.Failure"/> if the property 'IsError' in <paramref name="azureResponse"/> is <c>true</c>.
        /// </summary>
        /// <param name="azureResponse">The Azure response.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="azureResponse"/> is null.</exception>
        public AzGlobalResponse(TAzureResponse azureResponse) : this(ResponseStatus.Failure)
        {
            AssertHelper.AssertNotNullOrThrow(azureResponse, nameof(azureResponse));

            if (!azureResponse.IsError)
                Status =  ResponseStatus.Success;

            ValidateAndSetAzureResponse(Status, azureResponse);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzGlobalResponse{TAzureResponse, TEx}"/> class with the specified <paramref name="exception"/> 
        /// and the Status set to <see cref="ResponseStatus.Failure"/>.
        /// </summary>
        /// <param name="exception">The exception associated with the response.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="exception"/> is null.</exception>
        public AzGlobalResponse(TEx exception) : base(exception) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzGlobalResponse{TAzureResponse, TEx}"/> class from the data in <paramref name="globalResponse"/>.
        /// The new instance contains the same status and messages of the <paramref name="globalResponse"/> specified, 
        /// but the message keys may have been modified with suffix.
        /// </summary>
        /// <param name="globalResponse">The response from where the new instance will be generated.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="globalResponse"/> is null.</exception>
        protected internal AzGlobalResponse(IGlobalResponse globalResponse) : base(globalResponse) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzGlobalResponse{TAzureResponse, TEx}"/> class from the data in <paramref name="globalResponse"/>.
        /// The new instance contains the same status, exception and messages of the <paramref name="globalResponse"/> specified, 
        /// but the message keys may have been modified with suffix.
        /// </summary>
        /// <param name="globalResponse">The response from where the new instance will be generated.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="globalResponse"/> is null.</exception>
        protected internal AzGlobalResponse(IGlobalResponse<TEx> globalResponse) : base(globalResponse)
        {
            Exception = globalResponse.Exception;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzGlobalResponse{TAzureResponse, TEx}"/> class from the data in <paramref name="azGlobalResponse"/>.
        /// The new instance contains the same status, exception, error and messages of the <paramref name="azGlobalResponse"/> specified, 
        /// but the message keys may have been modified with suffix. 
        /// When <see cref="IAzGlobalResponse{TAzureResponse, TEx}.AzureResponse"/> is specified in <paramref name="azGlobalResponse"/>, 
        /// if 'IsError' in <see cref="IAzGlobalResponse{TAzureResponse, TEx}.AzureResponse"/> is <c>true</c>, 
        /// the 'Status' in <paramref name="azGlobalResponse"/> must be <see cref="ResponseStatus.Failure"/>;
        /// otherwise an <see cref="ArgumentException"/> will be thrown. 
        /// </summary>
        /// <param name="azGlobalResponse">The response from where the new instance will be generated.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="azGlobalResponse"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if the property 'IsError' in <see cref="IAzGlobalResponse{TAzureResponse, TEx}.AzureResponse"/> is <c>true</c> 
        /// and the 'Status' in <paramref name="azGlobalResponse"/> is not <see cref="ResponseStatus.Failure"/>.</exception>
        protected internal AzGlobalResponse(IAzGlobalResponse<TAzureResponse, TEx> azGlobalResponse) : this((IGlobalResponse<TEx>)azGlobalResponse)
        {
            ValidateAndSetAzureResponse(azGlobalResponse.Status, azGlobalResponse.AzureResponse);

            IncludeAzureResponseVerbose = azGlobalResponse.IncludeAzureResponseVerbose;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzGlobalResponse{TAzureResponse, TEx}"/> class from the data in <paramref name="globalResponse"/>, 
        /// with the same <paramref name="azureResponse"/> and <paramref name="includeAzureResponseVerbose"/>. 
        /// The <paramref name="globalResponse"/> specified must contain the Status set to <see cref="ResponseStatus.Failure"/>. 
        /// If the 'Status' in <paramref name="globalResponse"/> is not <see cref="ResponseStatus.Failure"/>, 
        /// an <see cref="ArgumentException"/> will be thrown. 
        /// The new instance contains the same status, exception and messages of the <paramref name="globalResponse"/> specified, 
        /// but the message keys may have been modified with suffix. 
        /// When <paramref name="azureResponse"/> is specified, 
        /// if 'IsError' in <paramref name="azureResponse"/> is <c>false</c> an <see cref="ArgumentException"/> will be thrown. 
        /// </summary>
        /// <param name="globalResponse">The response from where the new instance will be generated.</param>
        /// <param name="azureResponse">The Azure response.</param>
        /// <param name="includeAzureResponseVerbose">If set to <c>true</c>, 
        /// includes AzureResponse verbose information when <see cref="IVerboseResponse{T}.BuildVerbose"/> is called;
        /// otherwise, AzureResponse verbose information will not be included. Default value is <c>true</c>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="globalResponse"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if the 'Status' in <paramref name="globalResponse"/> is not <see cref="ResponseStatus.Failure"/>,
        /// or if 'IsError' in <paramref name="azureResponse"/> is <c>false</c>.</exception>
        protected internal AzGlobalResponse(
            IGlobalResponse<TEx> globalResponse,
            TAzureResponse? azureResponse,
            bool includeAzureResponseVerbose) : this(globalResponse)
        {
            ValidateAndSetAzureResponse(globalResponse.Status, azureResponse);

            IncludeAzureResponseVerbose = includeAzureResponseVerbose;
        }

        #endregion

        private void ValidateAndSetAzureResponse(ResponseStatus status, TAzureResponse? azureResponse)
        {
            if (azureResponse != null)
            {
                if (azureResponse!.IsError && status != ResponseStatus.Failure)
                    throw new ArgumentException(AzureResponseConstants.StatusDoesNotMatchWithAzureResponse(nameof(status), nameof(azureResponse)));

                AzureResponse = azureResponse;
            }
        }

        #region Properties

        /// <inheritdoc/>
        public bool HasAzureResponse => AzureResponse != null;

        /// <inheritdoc/>
        public virtual TAzureResponse? AzureResponse { get; protected internal set; }

        /// <inheritdoc/>
        public bool IncludeAzureResponseVerbose { get; set; } = true;

        #endregion

        #region IVerboseResponse implementation

        /// <inheritdoc/>
        public override Dictionary<string, string> BuildVerbose()
        {
            var verboseResponse = base.BuildVerbose();
            verboseResponse.AddOrRenameKey(nameof(HasAzureResponse), HasAzureResponse.ToString());
            if (HasAzureResponse)
            {
                verboseResponse.AddOrRenameKey($"{nameof(AzureResponse)}{GlobalResponseConstants.DefaultTypeMessageKeySuffix}",
                    AzureResponse!.GetType().FullName ?? GlobalResponseConstants.DefaultTypeFullNameUndefinedMessage);

                if (IncludeAzureResponseVerbose)
                    verboseResponse.TryAddRange(AzureResponse.BuildVerbose(), KeyExistAction.Rename);
            }

            return verboseResponse;
        }

        #endregion
    }
}
