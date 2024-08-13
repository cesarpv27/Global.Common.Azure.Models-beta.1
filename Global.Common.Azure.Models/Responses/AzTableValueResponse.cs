
namespace Global.Common.Azure.Models
{
    /// <inheritdoc cref="IAzTableValueResponse{T, TEx}"/>
    public class AzTableValueResponse<T, TEx> : AzGlobalValueResponse<T, AzureTableServiceResponse, TEx>, IAzTableValueResponse<T, TEx> where TEx : Exception
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AzTableValueResponse{T, TEx}"/> class.
        /// </summary>
        protected AzTableValueResponse() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzTableValueResponse{T, TEx}"/> class with the specified <paramref name="status"/>.
        /// </summary>
        protected AzTableValueResponse(ResponseStatus status) : base(status) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzTableValueResponse{T, TEx}"/> class with the specified <paramref name="status"/> 
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
        public AzTableValueResponse(ResponseStatus status, T? value) : base(status, value) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzTableValueResponse{T, TEx}"/> class with the specified <paramref name="value"/> 
        /// and a status of <see cref="ResponseStatus.Success"/>.
        /// </summary>
        /// <param name="value">The value associated with the response.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is null.</exception>
        public AzTableValueResponse(T value) : base(value) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzTableValueResponse{T, TEx}"/> class with the specified <paramref name="response"/> 
        /// and <paramref name="value"/>.
        /// The status will be set to <see cref="ResponseStatus.Success"/> if the property 'IsError' in <paramref name="response"/> is <c>false</c>.
        /// The status set to <see cref="ResponseStatus.Failure"/> if the property 'IsError' in <paramref name="response"/> is <c>true</c>.
        /// If the property 'IsError' in <paramref name="response"/> is <c>false</c>, then the <paramref name="value"/> must be specified.
        /// If the property 'IsError' in <paramref name="response"/> is <c>false</c> and the <paramref name="value"/> was not specified, 
        /// an <see cref="ArgumentNullException"/> will be thrown.
        /// If the property 'IsError' in <paramref name="response"/> is <c>true</c>, then the <paramref name="value"/> must be null.
        /// If the property 'IsError' in <paramref name="response"/> is <c>true</c> and the <paramref name="value"/> was specified, 
        /// an <see cref="ArgumentException"/> will be thrown.
        /// </summary>
        /// <param name="response">The Azure Table Service response.</param>
        /// <param name="value">The value associated with the response.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="response"/> is null, or 
        /// if the property 'IsError' in <paramref name="response"/> is <c>false</c> 
        /// and the <paramref name="value"/> was not specified.</exception>
        /// <exception cref="ArgumentException">Thrown if the property 'IsError' in <paramref name="response"/> is <c>true</c> 
        /// and the <paramref name="value"/> was specified</exception>
        public AzTableValueResponse(AzureTableServiceResponse response, T? value) : base(response, value) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzTableValueResponse{T, TEx}"/> class with the specified <paramref name="exception"/> 
        /// and the Status set to <see cref="ResponseStatus.Failure"/>.
        /// </summary>
        /// <param name="exception">The exception associated with the response.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="exception"/> is null.</exception>
        public AzTableValueResponse(TEx exception) : base(exception) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzTableValueResponse{T, TEx}"/> class from the data in <paramref name="globalResponse"/>. 
        /// The <paramref name="globalResponse"/> specified must contain the Status set to <see cref="ResponseStatus.Failure"/>. 
        /// If the 'Status' in <paramref name="globalResponse"/> is not <see cref="ResponseStatus.Failure"/>, 
        /// an <see cref="ArgumentException"/> will be thrown. 
        /// The new instance contains the same status and messages of the <paramref name="globalResponse"/>, 
        /// but the message keys may have been modified with suffix. 
        /// </summary>
        /// <param name="globalResponse">The response from where the new instance will be generated.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="globalResponse"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if the 'Status' in <paramref name="globalResponse"/> is not <see cref="ResponseStatus.Failure"/>.</exception>
        protected internal AzTableValueResponse(IGlobalResponse globalResponse) : base(globalResponse) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzTableValueResponse{T, TEx}"/> class from the data in <paramref name="globalResponse"/>. 
        /// The <paramref name="globalResponse"/> specified must contain the Status set to <see cref="ResponseStatus.Failure"/>. 
        /// If the 'Status' in <paramref name="globalResponse"/> is not <see cref="ResponseStatus.Failure"/>, 
        /// an <see cref="ArgumentException"/> will be thrown. 
        /// The new instance contains the same status, exception and messages of the <paramref name="globalResponse"/>, 
        /// but the message keys may have been modified with suffix. 
        /// </summary>
        /// <param name="globalResponse">The response from where the new instance will be generated.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="globalResponse"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if the 'Status' in <paramref name="globalResponse"/> is not <see cref="ResponseStatus.Failure"/>.</exception>
        protected internal AzTableValueResponse(IGlobalResponse<TEx> globalResponse) : base(globalResponse) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzTableValueResponse{T, TEx}"/> class from the data in <paramref name="azGlobalResponse"/>.
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
        protected internal AzTableValueResponse(IAzGlobalResponse<AzureTableServiceResponse, TEx> azGlobalResponse) : base(azGlobalResponse) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzTableValueResponse{T, TEx}"/> class from the data in <paramref name="azTableValueResponse"/>. 
        /// The new instance contains the same status, exception, error and messages of the <paramref name="azTableValueResponse"/> specified, 
        /// but the message keys may have been modified with suffix. 
        /// </summary>
        /// <param name="azTableValueResponse">The response from where the new instance will be generated.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="azTableValueResponse"/> is null.</exception>
        protected internal AzTableValueResponse(IAzTableValueResponse<T, TEx> azTableValueResponse) : base(azTableValueResponse) { }

        #endregion
    }

    /// <inheritdoc/>
    public class AzTableValueResponse<T> : AzTableValueResponse<T, Exception>, IAzTableValueResponse<T> 
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AzTableValueResponse{T}"/> class.
        /// </summary>
        protected AzTableValueResponse() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzTableValueResponse{T}"/> class with the specified <paramref name="status"/>.
        /// </summary>
        protected AzTableValueResponse(ResponseStatus status) : base(status) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzTableValueResponse{T}"/> class with the specified <paramref name="status"/> 
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
        public AzTableValueResponse(ResponseStatus status, T? value) : base(status, value) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzTableValueResponse{T}"/> class with the specified <paramref name="value"/> 
        /// and a status of <see cref="ResponseStatus.Success"/>.
        /// </summary>
        /// <param name="value">The value associated with the response.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is null.</exception>
        public AzTableValueResponse(T value) : base(value) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzTableValueResponse{T}"/> class with the specified <paramref name="response"/> 
        /// and <paramref name="value"/>.
        /// The status will be set to <see cref="ResponseStatus.Success"/> if the property 'IsError' in <paramref name="response"/> is <c>false</c>.
        /// The status set to <see cref="ResponseStatus.Failure"/> if the property 'IsError' in <paramref name="response"/> is <c>true</c>.
        /// If the property 'IsError' in <paramref name="response"/> is <c>false</c>, then the <paramref name="value"/> must be specified.
        /// If the property 'IsError' in <paramref name="response"/> is <c>false</c> and the <paramref name="value"/> was not specified, 
        /// an <see cref="ArgumentNullException"/> will be thrown.
        /// If the property 'IsError' in <paramref name="response"/> is <c>true</c>, then the <paramref name="value"/> must be null.
        /// If the property 'IsError' in <paramref name="response"/> is <c>true</c> and the <paramref name="value"/> was specified, 
        /// an <see cref="ArgumentException"/> will be thrown.
        /// </summary>
        /// <param name="response">The Azure Table Service response.</param>
        /// <param name="value">The value associated with the response.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="response"/> is null, or 
        /// if the property 'IsError' in <paramref name="response"/> is <c>false</c> 
        /// and the <paramref name="value"/> was not specified.</exception>
        /// <exception cref="ArgumentException">Thrown if the property 'IsError' in <paramref name="response"/> is <c>true</c> 
        /// and the <paramref name="value"/> was specified</exception>
        public AzTableValueResponse(AzureTableServiceResponse response, T? value) : base(response, value) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzTableValueResponse{T}"/> class with the specified <paramref name="exception"/> 
        /// and the Status set to <see cref="ResponseStatus.Failure"/>.
        /// </summary>
        /// <param name="exception">The exception associated with the response.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="exception"/> is null.</exception>
        public AzTableValueResponse(Exception exception) : base(exception) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzTableValueResponse{T}"/> class from the data in <paramref name="azTableValueResponse"/>. 
        /// The new instance contains the same status, exception, error and messages of the <paramref name="azTableValueResponse"/> specified, 
        /// but the message keys may have been modified with suffix. 
        /// </summary>
        /// <param name="azTableValueResponse">The response from where the new instance will be generated.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="azTableValueResponse"/> is null.</exception>
        public AzTableValueResponse(IAzTableValueResponse<T, Exception> azTableValueResponse) : base(azTableValueResponse) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzTableValueResponse{T}"/> class from the data in <paramref name="globalResponse"/>. 
        /// The <paramref name="globalResponse"/> specified must contain the Status set to <see cref="ResponseStatus.Failure"/>. 
        /// The new instance contains the same status and messages of the <paramref name="globalResponse"/>, 
        /// but the message keys may have been modified with suffix. 
        /// If the 'Status' in <paramref name="globalResponse"/> is not <see cref="ResponseStatus.Failure"/>, 
        /// an <see cref="ArgumentException"/> will be thrown.
        /// </summary>
        /// <param name="globalResponse">The response from where the new instance will be generated.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="globalResponse"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if the 'Status' in <paramref name="globalResponse"/> is not <see cref="ResponseStatus.Failure"/>.</exception>
        protected internal AzTableValueResponse(IGlobalResponse globalResponse) : base(globalResponse) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzTableValueResponse{T}"/> class from the data in <paramref name="globalResponse"/>. 
        /// The <paramref name="globalResponse"/> specified must contain the Status set to <see cref="ResponseStatus.Failure"/>. 
        /// The new instance contains the same status, exception and messages of the <paramref name="globalResponse"/>, 
        /// but the message keys may have been modified with suffix. 
        /// If the 'Status' in <paramref name="globalResponse"/> is not <see cref="ResponseStatus.Failure"/>, 
        /// an <see cref="ArgumentException"/> will be thrown.
        /// </summary>
        /// <param name="globalResponse">The response from where the new instance will be generated.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="globalResponse"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if the 'Status' in <paramref name="globalResponse"/> is not <see cref="ResponseStatus.Failure"/>.</exception>
        protected internal AzTableValueResponse(IGlobalResponse<Exception> globalResponse) : base(globalResponse) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzTableValueResponse{T}"/> class from the data in <paramref name="azGlobalResponse"/>.
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
        protected internal AzTableValueResponse(IAzGlobalResponse<AzureTableServiceResponse, Exception> azGlobalResponse) : base(azGlobalResponse) { }

        #endregion
    }
}
