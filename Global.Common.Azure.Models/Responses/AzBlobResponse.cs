
namespace Global.Common.Azure.Models
{
    /// <inheritdoc cref="IAzBlobResponse{TEx}"/>
    public class AzBlobResponse<TEx> : AzGlobalResponse<AzureBlobStorageResponse, TEx>, IAzBlobResponse<TEx> where TEx : Exception
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AzBlobResponse{TEx}"/> class.
        /// </summary>
        protected AzBlobResponse() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzBlobResponse{TEx}"/> class with the specified <paramref name="status"/>.
        /// </summary>
        /// <param name="status">The response status.</param>
        public AzBlobResponse(ResponseStatus status) : base(status) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzBlobResponse{TEx}"/> class with the provided <paramref name="response"/> 
        /// and the Status set to <see cref="ResponseStatus.Success"/> if the property 'IsError' in <paramref name="response"/> is <c>false</c>, 
        /// or the Status set to <see cref="ResponseStatus.Failure"/> if the property 'IsError' in <paramref name="response"/> is <c>true</c>.
        /// </summary>
        /// <param name="response">The Azure Blob Storage response.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="response"/> is null.</exception>
        public AzBlobResponse(AzureBlobStorageResponse response) : base(response) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzBlobResponse{TEx}"/> class with the specified <paramref name="exception"/> 
        /// and the Status set to <see cref="ResponseStatus.Failure"/>.
        /// </summary>
        /// <param name="exception">The exception associated with the response.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="exception"/> is null.</exception>
        public AzBlobResponse(TEx exception) : base(exception) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzBlobResponse{TEx}"/> class from the data in <paramref name="globalResponse"/>.
        /// The new instance contains the same status and messages of the <paramref name="globalResponse"/> specified, but the message keys may have been modified with suffix.
        /// </summary>
        /// <param name="globalResponse">The response from where the new instance will be generated.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="globalResponse"/> is null.</exception>
        protected internal AzBlobResponse(IGlobalResponse globalResponse) : base(globalResponse) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzBlobResponse{TEx}"/> class from the data in <paramref name="globalResponse"/>.
        /// The new instance contains the same status, exception and messages of the <paramref name="globalResponse"/> specified, 
        /// but the message keys may have been modified with suffix.
        /// </summary>
        /// <param name="globalResponse">The response from where the new instance will be generated.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="globalResponse"/> is null.</exception>
        protected internal AzBlobResponse(IGlobalResponse<TEx> globalResponse) : base(globalResponse) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzBlobResponse{TEx}"/> class from the data in <paramref name="azGlobalResponse"/>.
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
        protected internal AzBlobResponse(IAzGlobalResponse<AzureBlobStorageResponse, TEx> azGlobalResponse) : base(azGlobalResponse) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzBlobResponse{TEx}"/> class from the data in <paramref name="globalResponse"/>, 
        /// with the same <paramref name="response"/> and <paramref name="includeAzureResponseVerbose"/>. 
        /// The <paramref name="globalResponse"/> specified must contain the Status set to <see cref="ResponseStatus.Failure"/>. 
        /// If the 'Status' in <paramref name="globalResponse"/> is not <see cref="ResponseStatus.Failure"/>, 
        /// an <see cref="ArgumentException"/> will be thrown. 
        /// The new instance contains the same status, exception and messages of the <paramref name="globalResponse"/> specified, 
        /// but the message keys may have been modified with suffix. 
        /// When <paramref name="response"/> is specified, 
        /// if 'IsError' in <paramref name="response"/> is <c>false</c> an <see cref="ArgumentException"/> will be thrown. 
        /// </summary>
        /// <param name="globalResponse">The response from where the new instance will be generated.</param>
        /// <param name="response">The Azure Blob Storage response.</param>
        /// <param name="includeAzureResponseVerbose">If set to <c>true</c>, 
        /// includes AzureResponse verbose information when <see cref="IVerboseResponse{T}.BuildVerbose"/> is called;
        /// otherwise, AzureResponse verbose information will not be included. Default value is <c>true</c>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="globalResponse"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if the 'Status' in <paramref name="globalResponse"/> is not <see cref="ResponseStatus.Failure"/>,
        /// or if 'IsError' in <paramref name="response"/> is <c>false</c>.</exception>
        protected internal AzBlobResponse(
            IGlobalResponse<TEx> globalResponse, 
            AzureBlobStorageResponse? response,
            bool includeAzureResponseVerbose) : base(globalResponse, response, includeAzureResponseVerbose) { }

        #endregion
    }

    /// <inheritdoc/>
    public class AzBlobResponse : AzBlobResponse<Exception>, IAzBlobResponse
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AzBlobResponse"/> class.
        /// </summary>
        protected AzBlobResponse() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzBlobResponse"/> class with the specified <paramref name="status"/>.
        /// </summary>
        /// <param name="status">The response status.</param>
        public AzBlobResponse(ResponseStatus status) : base(status) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzBlobResponse"/> class with the provided <paramref name="response"/> 
        /// and the Status set to <see cref="ResponseStatus.Success"/> if the property 'IsError' in <paramref name="response"/> is <c>false</c>, 
        /// or the Status set to <see cref="ResponseStatus.Failure"/> if the property 'IsError' in <paramref name="response"/> is <c>true</c>.
        /// </summary>
        /// <param name="response">The Azure Blob Storage response.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="response"/> is null.</exception>
        public AzBlobResponse(AzureBlobStorageResponse response) : base(response) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzBlobResponse"/> class with the specified <paramref name="exception"/> 
        /// and the Status set to <see cref="ResponseStatus.Failure"/>.
        /// </summary>
        /// <param name="exception">The exception associated with the response.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="exception"/> is null.</exception>
        public AzBlobResponse(Exception exception) : base(exception) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzBlobResponse"/> class using the status and messages from the <paramref name="globalResponse"/> specified.
        /// The new instance contains the same status and messages of the <paramref name="globalResponse"/> specified, but the message keys may have been modified with suffix.
        /// </summary>
        /// <param name="globalResponse">The response from where the new instance will be generated.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="globalResponse"/> is null.</exception>
        protected internal AzBlobResponse(IGlobalResponse globalResponse) : base(globalResponse) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzBlobResponse"/> class using the status, exception 
        /// and messages from the <paramref name="globalResponse"/> specified.
        /// The new instance contains the same status, exception and messages of the <paramref name="globalResponse"/> specified, 
        /// but the message keys may have been modified with suffix.
        /// </summary>
        /// <param name="globalResponse">The response from where the new instance will be generated.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="globalResponse"/> is null.</exception>
        protected internal AzBlobResponse(IGlobalResponse<Exception> globalResponse) : base(globalResponse) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzBlobResponse"/> class from the data in <paramref name="azGlobalResponse"/>.
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
        protected internal AzBlobResponse(IAzGlobalResponse<AzureBlobStorageResponse, Exception> azGlobalResponse) : base(azGlobalResponse) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzBlobResponse"/> class from the data in <paramref name="globalResponse"/>, 
        /// with the same <paramref name="response"/> and <paramref name="includeAzureResponseVerbose"/>. 
        /// The <paramref name="globalResponse"/> specified must contain the Status set to <see cref="ResponseStatus.Failure"/>. 
        /// If the 'Status' in <paramref name="globalResponse"/> is not <see cref="ResponseStatus.Failure"/>, 
        /// an <see cref="ArgumentException"/> will be thrown. 
        /// The new instance contains the same status, exception and messages of the <paramref name="globalResponse"/> specified, 
        /// but the message keys may have been modified with suffix. 
        /// When <paramref name="response"/> is specified, 
        /// if 'IsError' in <paramref name="response"/> is <c>false</c> an <see cref="ArgumentException"/> will be thrown. 
        /// </summary>
        /// <param name="globalResponse">The response from where the new instance will be generated.</param>
        /// <param name="response">The Azure Blob Storage response.</param>
        /// <param name="includeAzureResponseVerbose">If set to <c>true</c>, 
        /// includes AzureResponse verbose information when <see cref="IVerboseResponse{T}.BuildVerbose"/> is called;
        /// otherwise, AzureResponse verbose information will not be included. Default value is <c>true</c>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="globalResponse"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if the 'Status' in <paramref name="globalResponse"/> is not <see cref="ResponseStatus.Failure"/>,
        /// or if 'IsError' in <paramref name="response"/> is <c>false</c>.</exception>
        protected internal AzBlobResponse(
            IGlobalResponse<Exception> globalResponse, 
            AzureBlobStorageResponse? response,
            bool includeAzureResponseVerbose) : base(globalResponse, response, includeAzureResponseVerbose) { }

        #endregion
    }
}
