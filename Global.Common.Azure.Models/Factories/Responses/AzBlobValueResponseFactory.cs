
namespace Global.Common.Azure.Models
{
    /// <summary>
    /// Provides factory methods for creating instances of <see cref="AzBlobValueResponse{T, TEx}"/> and <see cref="AzBlobValueResponse{T}"/>.
    /// </summary>
    public static class AzBlobValueResponseFactory
    {
        #region AzBlobValueResponse{T, TEx}

        /// <summary>
        /// Creates a new instance of the <see cref="AzBlobValueResponse{T, TEx}"/> class with the specified <paramref name="value"/> 
        /// and the Status set to <see cref="ResponseStatus.Success"/>.
        /// </summary>
        /// <typeparam name="T">The type of value associated with the response.</typeparam>
        /// <typeparam name="TEx">The type of exception associated with the response.</typeparam>
        /// <param name="value">The value associated with the response.</param>
        /// <returns>An instance of <see cref="AzBlobValueResponse{T, TEx}"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is null.</exception>
        public static AzBlobValueResponse<T, TEx> CreateSuccessful<T, TEx>(T value)
            where TEx : Exception
        {
            return new AzBlobValueResponse<T, TEx>(ResponseStatus.Success, value);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="AzBlobValueResponse{T, TEx}"/> class with the specified <paramref name="value"/> 
        /// and the Status set to <see cref="ResponseStatus.Warning"/>.
        /// </summary>
        /// <typeparam name="T">The type of value associated with the response.</typeparam>
        /// <typeparam name="TEx">The type of exception associated with the response.</typeparam>
        /// <param name="value">The value associated with the response.</param>
        /// <returns>An instance of <see cref="AzBlobValueResponse{T, TEx}"/>.</returns>
        public static AzBlobValueResponse<T, TEx> CreateWarning<T, TEx>(T? value)
            where TEx : Exception
        {
            return new AzBlobValueResponse<T, TEx>(ResponseStatus.Warning, value);
        }

        /// <summary>
        /// Creates a new instance of <see cref="AzBlobValueResponse{T, TEx}"/> with the Status set to <see cref="ResponseStatus.Failure"/>.
        /// </summary>
        /// <typeparam name="T">The type of value associated with the response.</typeparam>
        /// <typeparam name="TEx">The type of exception associated with the response.</typeparam>
        /// <returns>A new instance of <see cref="AzBlobValueResponse{T, TEx}"/>.</returns>
        public static AzBlobValueResponse<T, TEx> CreateFailure<T, TEx>()
            where TEx : Exception
        {
            return new AzBlobValueResponse<T, TEx>(ResponseStatus.Failure, default);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="AzBlobValueResponse{T, TEx}"/> class with the specified <paramref name="exception"/> 
        /// and the Status set to <see cref="ResponseStatus.Failure"/>.
        /// </summary>
        /// <typeparam name="T">The type of value associated with the response.</typeparam>
        /// <typeparam name="TEx">The type of exception associated with the response.</typeparam>
        /// <param name="exception">The exception associated with the response.</param>
        /// <returns>An instance of <see cref="AzBlobValueResponse{T, TEx}"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="exception"/> is null.</exception>
        public static AzBlobValueResponse<T, TEx> CreateFailure<T, TEx>(TEx exception)
            where TEx : Exception
        {
            return new AzBlobValueResponse<T, TEx>(exception);
        }

        #region CreateFrom methods

        /// <summary>
        /// Creates a new instance of the <see cref="AzBlobValueResponse{T, TEx}"/> class with the specified <paramref name="response"/> 
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
        /// <typeparam name="T">The type of value associated with the response.</typeparam>
        /// <typeparam name="TEx">The type of exception associated with the response.</typeparam>
        /// <param name="response">The Azure Blob Storage response.</param>
        /// <param name="value">The value associated with the response.</param>
        /// <returns>An instance of <see cref="AzBlobValueResponse{T, TEx}"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="response"/> is null, or 
        /// if the property 'IsError' in <paramref name="response"/> is <c>false</c> and the <paramref name="value"/> was not specified.</exception>
        /// <exception cref="ArgumentException">Thrown if the property 'IsError' in <paramref name="response"/> is <c>true</c> 
        /// and the <paramref name="value"/> was specified</exception>
        public static AzBlobValueResponse<T, TEx> CreateFrom<T, TEx>(AzureBlobStorageResponse response, T? value)
            where TEx : Exception
        {
            return new AzBlobValueResponse<T, TEx>(response, value);
        }

        /// <summary>
        /// Creates a new instance of <see cref="AzBlobValueResponse{T, TEx}"/> with the specified <paramref name="response"/> from Azure Blob Storage operation.
        /// </summary>
        /// <typeparam name="T">The type of value associated with the response.</typeparam>
        /// <typeparam name="TEx">The type of exception associated with the response.</typeparam>
        /// <param name="response">The response from Azure Blob Storage operation.</param>
        /// <returns>A new instance of <see cref="AzBlobValueResponse{T, TEx}"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="response"/> is null.</exception>
        public static AzBlobValueResponse<T, TEx> CreateFrom<T, TEx>(Response<T> response)
            where TEx : Exception
        {
            return new AzBlobValueResponse<T, TEx>(AzureBlobStorageResponse.Create(response), response.Value);
        }

        /// <summary>
        /// Creates a new instance of <see cref="AzBlobValueResponse{T, TEx}"/> with the specified <paramref name="exception"/> from Azure Blob Storage operation 
        /// and the Status set to <see cref="ResponseStatus.Failure"/>.
        /// </summary>
        /// <typeparam name="T">The type of value associated with the response.</typeparam>
        /// <typeparam name="TEx">The type of exception associated with the response.</typeparam>
        /// <param name="exception">The exception from Azure Blob Storage operation.</param>
        /// <returns>A new instance of <see cref="AzBlobValueResponse{T, TEx}"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="exception"/> is null.</exception>
        public static AzBlobValueResponse<T, TEx> CreateFrom<T, TEx>(RequestFailedException exception)
            where TEx : Exception
        {
            return new AzBlobValueResponse<T, TEx>(AzureBlobStorageResponse.Create(exception), default);
        }

        #endregion

        #region MapFrom methods

        /// <summary>
        /// Maps an <see cref="AzBlobValueResponse{TIn, TEx}"/> to an <see cref="AzBlobValueResponse{TOut, TEx}"/> 
        /// using the provided conversion function.
        /// </summary>
        /// <typeparam name="TIn">The type of the incoming value.</typeparam>
        /// <typeparam name="TOut">The type of the outgoing value.</typeparam>
        /// <typeparam name="TEx">The type of the exception.</typeparam>
        /// <param name="azBlobValueResponse">The input response to map from.</param>
        /// <param name="convertTo">The function to convert the incoming value to the outgoing value.</param>
        /// <param name="messageKeyExistAction">The action to take when message keys in the incoming response already exist in the outgoint response. 
        /// This parameter is used to map the messages from the incoming response to outgoing response. 
        /// Default is <see cref="KeyExistAction.Rename"/>.</param>
        /// <returns>A mapped <see cref="AzBlobValueResponse{TOut, TEx}"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="azBlobValueResponse"/>, <paramref name="convertTo"/> 
        /// or <paramref name="convertTo"/> result are null.</exception>
        public static AzBlobValueResponse<TOut, TEx> MapFrom<TIn, TOut, TEx>(
            AzBlobValueResponse<TIn, TEx> azBlobValueResponse,
            Func<TIn, TOut> convertTo,
            KeyExistAction messageKeyExistAction = KeyExistAction.Rename)
            where TEx : Exception
        {
            return AzGlobalValueResponseFactory.CommonMapFrom<TIn, TOut, AzBlobValueResponse<TIn, TEx>, AzBlobValueResponse<TOut, TEx>, AzureBlobStorageResponse, TEx>(
                azBlobValueResponse,
                convertTo,
                CreateSuccessful<TOut, TEx>,
                CreateWarning<TOut, TEx>,
                MapFromFailure<TIn, TOut, TEx>,
                messageKeyExistAction);
        }

        #endregion

        #region MapFromFailure methods

        /// <summary>
        /// Creates a new instance of the <see cref="AzBlobValueResponse{T, TEx}"/> class from the data in <paramref name="globalResponse"/>.
        /// The <paramref name="globalResponse"/> specified must contain the Status set to <see cref="ResponseStatus.Failure"/>. 
        /// The new instance contains the same status and messages of the <paramref name="globalResponse"/> specified, 
        /// but the message keys may have been modified with suffix. 
        /// If the 'Status' in <paramref name="globalResponse"/> is not <see cref="ResponseStatus.Failure"/>, 
        /// an <see cref="ArgumentException"/> will be thrown.
        /// </summary>
        /// <typeparam name="T">The type of value associated with the response.</typeparam>
        /// <typeparam name="TEx">The type of exception associated with the response.</typeparam>
        /// <param name="globalResponse">The response from where the outgoing response will be generated.</param>
        /// <returns>An instance of <see cref="AzBlobValueResponse{T, TEx}"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="globalResponse"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if the 'Status' in <paramref name="globalResponse"/> is not <see cref="ResponseStatus.Failure"/>.</exception>
        public static AzBlobValueResponse<T, TEx> MapFromFailure<T, TEx>(IGlobalResponse globalResponse)
            where TEx : Exception
        {
            return new AzBlobValueResponse<T, TEx>(globalResponse);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="AzBlobValueResponse{T, TEx}"/> class from the data in <paramref name="globalResponse"/>.
        /// The <paramref name="globalResponse"/> specified must contain the Status set to <see cref="ResponseStatus.Failure"/>. 
        /// The new instance contains the same status, exception and messages of the <paramref name="globalResponse"/> specified, 
        /// but the message keys may have been modified with suffix. 
        /// If the 'Status' in <paramref name="globalResponse"/> is not <see cref="ResponseStatus.Failure"/>, 
        /// an <see cref="ArgumentException"/> will be thrown.
        /// </summary>
        /// <typeparam name="T">The type of value associated with the response.</typeparam>
        /// <typeparam name="TEx">The type of exception associated with the response.</typeparam>
        /// <param name="globalResponse">The response from where the outgoing response will be generated.</param>
        /// <returns>An instance of <see cref="AzBlobValueResponse{T, TEx}"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="globalResponse"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if the 'Status' in <paramref name="globalResponse"/> is not <see cref="ResponseStatus.Failure"/>.</exception>
        public static AzBlobValueResponse<T, TEx> MapFromFailure<T, TEx>(IGlobalResponse<TEx> globalResponse)
            where TEx : Exception
        {
            return new AzBlobValueResponse<T, TEx>(globalResponse);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="AzBlobValueResponse{T, TEx}"/> class from the data in <paramref name="globalResponse"/>.
        /// The <paramref name="globalResponse"/> specified must contain the Status set to <see cref="ResponseStatus.Failure"/>. 
        /// The new instance contains the same status, exception and messages of the <paramref name="globalResponse"/> specified, 
        /// but the message keys may have been modified with suffix. 
        /// If the 'Status' in <paramref name="globalResponse"/> is not <see cref="ResponseStatus.Failure"/>, 
        /// an <see cref="ArgumentException"/> will be thrown.
        /// </summary>
        /// <typeparam name="TIn">The type of value associated with the ingoing response.</typeparam>
        /// <typeparam name="TOut">The type of value associated with the outgoing response.</typeparam>
        /// <typeparam name="TEx">The type of exception associated with the response.</typeparam>
        /// <param name="globalResponse">The response from where the outgoing response will be generated.</param>
        /// <returns>An instance of <see cref="AzBlobValueResponse{T, TEx}"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="globalResponse"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if the 'Status' in <paramref name="globalResponse"/> is not <see cref="ResponseStatus.Failure"/>.</exception>
        public static AzBlobValueResponse<TOut, TEx> MapFromFailure<TIn, TOut, TEx>(IGlobalValueResponse<TIn, TEx> globalResponse)
            where TEx : Exception
        {
            return MapFromFailure<TOut, TEx>(globalResponse);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="AzBlobValueResponse{T, TEx}"/> class from the data in <paramref name="azGlobalValueResponse"/>.
        /// The <paramref name="azGlobalValueResponse"/> specified must contain the Status set to <see cref="ResponseStatus.Failure"/>. 
        /// If the 'Status' in <paramref name="azGlobalValueResponse"/> is not <see cref="ResponseStatus.Failure"/>, 
        /// an <see cref="ArgumentException"/> will be thrown. 
        /// The new instance contains the same status, exception, error and messages of the <paramref name="azGlobalValueResponse"/> specified, 
        /// but the message keys may have been modified with suffix. 
        /// When <see cref="IAzGlobalResponse{TAzureResponse, TEx}.AzureResponse"/> is specified, 
        /// if 'IsError' in <see cref="IAzGlobalResponse{TAzureResponse, TEx}.AzureResponse"/> is <c>false</c> an <see cref="ArgumentException"/> will be thrown. 
        /// </summary>
        /// <typeparam name="TIn">The type of value associated with the ingoing response.</typeparam>
        /// <typeparam name="TOut">The type of value associated with the outgoing response.</typeparam>
        /// <typeparam name="TEx">The type of exception associated with the response.</typeparam>
        /// <param name="azGlobalValueResponse">The response from where the outgoing response will be generated.</param>
        /// <returns>An instance of <see cref="AzBlobValueResponse{T, TEx}"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="azGlobalValueResponse"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if the 'Status' in <paramref name="azGlobalValueResponse"/> is not <see cref="ResponseStatus.Failure"/>,
        /// or if 'IsError' in <see cref="IAzGlobalResponse{TAzureResponse, TEx}.AzureResponse"/> is <c>false</c>.</exception>
        public static AzBlobValueResponse<TOut, TEx> MapFromFailure<TIn, TOut, TEx>(IAzGlobalValueResponse<TIn, AzureBlobStorageResponse, TEx> azGlobalValueResponse)
        where TEx : Exception
        {
            return new AzBlobValueResponse<TOut, TEx>(azGlobalValueResponse);
        }

        #endregion

        #endregion

        #region AzBlobValueResponse{T}

        /// <summary>
        /// Creates a new instance of the <see cref="AzBlobValueResponse{T}"/> class with the specified <paramref name="value"/> 
        /// and the Status set to <see cref="ResponseStatus.Success"/>.
        /// </summary>
        /// <typeparam name="T">The type of value associated with the response.</typeparam>
        /// <param name="value">The value associated with the response.</param>
        /// <returns>An instance of <see cref="AzBlobValueResponse{T}"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is null.</exception>
        public static AzBlobValueResponse<T> CreateSuccessful<T>(T value)
        {
            return new AzBlobValueResponse<T>(ResponseStatus.Success, value);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="AzBlobValueResponse{T}"/> class with the specified <paramref name="value"/> 
        /// and the Status set to <see cref="ResponseStatus.Warning"/>.
        /// </summary>
        /// <typeparam name="T">The type of value associated with the response.</typeparam>
        /// <param name="value">The value associated with the response.</param>
        /// <returns>An instance of <see cref="AzBlobValueResponse{T}"/>.</returns>
        public static AzBlobValueResponse<T> CreateWarning<T>(T? value)
        {
            return new AzBlobValueResponse<T>(ResponseStatus.Warning, value);
        }

        /// <summary>
        /// Creates a new instance of <see cref="AzBlobValueResponse{T}"/> with the Status set to <see cref="ResponseStatus.Failure"/>.
        /// </summary>
        /// <typeparam name="T">The type of value associated with the response.</typeparam>
        /// <returns>A new instance of <see cref="AzBlobValueResponse{T}"/>.</returns>
        public static AzBlobValueResponse<T> CreateFailure<T>()
        {
            return new AzBlobValueResponse<T>(ResponseStatus.Failure, default);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="AzBlobValueResponse{T}"/> class with the specified <paramref name="exception"/> 
        /// and the Status set to <see cref="ResponseStatus.Failure"/>.
        /// </summary>
        /// <typeparam name="T">The type of value associated with the response.</typeparam>
        /// <param name="exception">The exception associated with the response.</param>
        /// <returns>An instance of <see cref="AzBlobValueResponse{T}"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="exception"/> is null.</exception>
        public static AzBlobValueResponse<T> CreateFailure<T>(Exception exception)
        {
            return new AzBlobValueResponse<T>(exception);
        }

        #region CreateFrom methods

        /// <summary>
        /// Creates a new instance of the <see cref="AzBlobValueResponse{T}"/> class with the specified <paramref name="response"/> 
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
        /// <typeparam name="T">The type of value associated with the response.</typeparam>
        /// <param name="response">The Azure Blob Storage response.</param>
        /// <param name="value">The value associated with the response.</param>
        /// <returns>An instance of <see cref="AzBlobValueResponse{T}"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="response"/> is null, or 
        /// if the property 'IsError' in <paramref name="response"/> is <c>false</c> and the <paramref name="value"/> was not specified.</exception>
        /// <exception cref="ArgumentException">Thrown if the property 'IsError' in <paramref name="response"/> is <c>true</c> 
        /// and the <paramref name="value"/> was specified</exception>
        public static AzBlobValueResponse<T> CreateFrom<T>(AzureBlobStorageResponse response, T? value)
        {
            return new AzBlobValueResponse<T>(response, value);
        }

        /// <summary>
        /// Creates a new instance of <see cref="AzBlobValueResponse{T}"/> with the specified <paramref name="response"/> from Azure Blob Storage operation.
        /// </summary>
        /// <typeparam name="T">The type of value associated with the response.</typeparam>
        /// <param name="response">The response from Azure Blob Storage operation.</param>
        /// <returns>A new instance of <see cref="AzBlobValueResponse{T}"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="response"/> is null.</exception>
        public static AzBlobValueResponse<T> CreateFrom<T>(Response<T> response)
        {
            return new AzBlobValueResponse<T>(AzureBlobStorageResponse.Create(response), response.Value);
        }

        /// <summary>
        /// Creates a new instance of <see cref="AzBlobValueResponse{T}"/> with the specified <paramref name="exception"/> from Azure Blob Storage operation 
        /// and the Status set to <see cref="ResponseStatus.Failure"/>.
        /// </summary>
        /// <typeparam name="T">The type of value associated with the response.</typeparam>
        /// <param name="exception">The exception from Azure Blob Storage operation.</param>
        /// <returns>A new instance of <see cref="AzBlobValueResponse{T}"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="exception"/> is null.</exception>
        public static AzBlobValueResponse<T> CreateFrom<T>(RequestFailedException exception)
        {
            return new AzBlobValueResponse<T>(AzureBlobStorageResponse.Create(exception), default);
        }

        #endregion

        #region MapFrom methods

        /// <summary>
        /// Maps an <see cref="AzBlobValueResponse{TIn}"/> to an <see cref="AzBlobValueResponse{TOut}"/> using the provided conversion function.
        /// </summary>
        /// <typeparam name="TIn">The type of the incoming value.</typeparam>
        /// <typeparam name="TOut">The type of the outgoing value.</typeparam>
        /// <param name="azBlobValueResponse">The input response to map from.</param>
        /// <param name="convertTo">The function to convert the incoming value to the outgoing value.</param>
        /// <param name="messageKeyExistAction">The action to take when message keys in the incoming response already exist in the outgoint response. 
        /// This parameter is used to map the messages from the incoming response to outgoing response. Default is <see cref="KeyExistAction.Rename"/>.</param>
        /// <returns>A mapped <see cref="AzBlobValueResponse{TOut}"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="azBlobValueResponse"/>, <paramref name="convertTo"/> 
        /// or <paramref name="convertTo"/> result are null.</exception>
        public static AzBlobValueResponse<TOut> MapFrom<TIn, TOut>(
            AzBlobValueResponse<TIn> azBlobValueResponse,
            Func<TIn, TOut> convertTo,
            KeyExistAction messageKeyExistAction = KeyExistAction.Rename)
        {
            return AzGlobalValueResponseFactory.CommonMapFrom<TIn, TOut, AzBlobValueResponse<TIn>, AzBlobValueResponse<TOut>, AzureBlobStorageResponse, Exception>(
                azBlobValueResponse,
                convertTo,
                CreateSuccessful,
                CreateWarning,
                MapFromFailure<TIn, TOut>,
                messageKeyExistAction);
        }

        #endregion

        #region MapFromFailure methods

        /// <summary>
        /// Creates a new instance of the <see cref="AzBlobValueResponse{T}"/> class from the data in <paramref name="globalResponse"/>.
        /// The <paramref name="globalResponse"/> specified must contain the Status set to <see cref="ResponseStatus.Failure"/>. 
        /// The new instance contains the same status and messages of the <paramref name="globalResponse"/> specified, 
        /// but the message keys may have been modified with suffix. 
        /// If the 'Status' in <paramref name="globalResponse"/> is not <see cref="ResponseStatus.Failure"/>, 
        /// an <see cref="ArgumentException"/> will be thrown.
        /// </summary>
        /// <typeparam name="T">The type of value associated with the response.</typeparam>
        /// <param name="globalResponse">The response from where the outgoing response will be generated.</param>
        /// <returns>An instance of <see cref="AzBlobValueResponse{T}"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="globalResponse"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if the 'Status' in <paramref name="globalResponse"/> is not <see cref="ResponseStatus.Failure"/>.</exception>
        public static AzBlobValueResponse<T> MapFromFailure<T>(IGlobalResponse globalResponse)
        {
            return new AzBlobValueResponse<T>(globalResponse);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="AzBlobValueResponse{T}"/> class from the data in <paramref name="globalResponse"/>.
        /// The <paramref name="globalResponse"/> specified must contain the Status set to <see cref="ResponseStatus.Failure"/>. 
        /// The new instance contains the same status, exception and messages of the <paramref name="globalResponse"/> specified, 
        /// but the message keys may have been modified with suffix. 
        /// If the 'Status' in <paramref name="globalResponse"/> is not <see cref="ResponseStatus.Failure"/>, 
        /// an <see cref="ArgumentException"/> will be thrown.
        /// </summary>
        /// <typeparam name="T">The type of value associated with the response.</typeparam>
        /// <param name="globalResponse">The response from where the outgoing response will be generated.</param>
        /// <returns>An instance of <see cref="AzBlobValueResponse{T}"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="globalResponse"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if the 'Status' in <paramref name="globalResponse"/> is not <see cref="ResponseStatus.Failure"/>.</exception>
        public static AzBlobValueResponse<T> MapFromFailure<T>(IGlobalResponse<Exception> globalResponse)
        {
            return new AzBlobValueResponse<T>(globalResponse);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="AzBlobValueResponse{T}"/> class from the data in <paramref name="globalResponse"/>.
        /// The <paramref name="globalResponse"/> specified must contain the Status set to <see cref="ResponseStatus.Failure"/>. 
        /// The new instance contains the same status, exception and messages of the <paramref name="globalResponse"/> specified, 
        /// but the message keys may have been modified with suffix. 
        /// If the 'Status' in <paramref name="globalResponse"/> is not <see cref="ResponseStatus.Failure"/>, 
        /// an <see cref="ArgumentException"/> will be thrown.
        /// </summary>
        /// <typeparam name="TIn">The type of value associated with the ingoing response.</typeparam>
        /// <typeparam name="TOut">The type of value associated with the outgoing response.</typeparam>
        /// <param name="globalResponse">The response from where the outgoing response will be generated.</param>
        /// <returns>An instance of <see cref="AzBlobValueResponse{T}"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="globalResponse"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if the 'Status' in <paramref name="globalResponse"/> is not <see cref="ResponseStatus.Failure"/>.</exception>
        public static AzBlobValueResponse<TOut> MapFromFailure<TIn, TOut>(IGlobalValueResponse<TIn, Exception> globalResponse)
        {
            return MapFromFailure<TOut>(globalResponse);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="AzBlobValueResponse{T, TEx}"/> class from the data in <paramref name="azGlobalValueResponse"/>.
        /// The <paramref name="azGlobalValueResponse"/> specified must contain the Status set to <see cref="ResponseStatus.Failure"/>. 
        /// If the 'Status' in <paramref name="azGlobalValueResponse"/> is not <see cref="ResponseStatus.Failure"/>, 
        /// an <see cref="ArgumentException"/> will be thrown. 
        /// The new instance contains the same status, exception, error and messages of the <paramref name="azGlobalValueResponse"/> specified, 
        /// but the message keys may have been modified with suffix. 
        /// When <see cref="IAzGlobalResponse{TAzureResponse, TEx}.AzureResponse"/> is specified, 
        /// if 'IsError' in <see cref="IAzGlobalResponse{TAzureResponse, TEx}.AzureResponse"/> is <c>false</c> an <see cref="ArgumentException"/> will be thrown. 
        /// </summary>
        /// <typeparam name="TIn">The type of value associated with the ingoing response.</typeparam>
        /// <typeparam name="TOut">The type of value associated with the outgoing response.</typeparam>
        /// <param name="azGlobalValueResponse">The response from where the outgoing response will be generated.</param>
        /// <returns>An instance of <see cref="AzBlobValueResponse{T}"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="azGlobalValueResponse"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if the 'Status' in <paramref name="azGlobalValueResponse"/> is not <see cref="ResponseStatus.Failure"/>,
        /// or if 'IsError' in <see cref="IAzGlobalResponse{TAzureResponse, TEx}.AzureResponse"/> is <c>false</c>.</exception>
        public static AzBlobValueResponse<TOut> MapFromFailure<TIn, TOut>(IAzGlobalValueResponse<TIn, AzureBlobStorageResponse, Exception> azGlobalValueResponse)
        {
            return new AzBlobValueResponse<TOut>(azGlobalValueResponse);
        }

        #endregion

        #endregion
    }
}
