
namespace Global.Common.Azure.Models
{
    /// <summary>
    /// Provides factory methods for creating instances of <see cref="AzGlobalValueResponse{T, TAzureResponse, TEx}"/>.
    /// </summary>
    public static class AzGlobalValueResponseFactory
    {
        /// <summary>
        /// Creates a new instance of the <see cref="AzGlobalValueResponse{T, TAzureResponse, TEx}"/> class with the specified <paramref name="value"/> 
        /// and the Status set to <see cref="ResponseStatus.Success"/>.
        /// </summary>
        /// <typeparam name="T">The type of value associated with the response.</typeparam>
        /// <typeparam name="TAzureResponse">The type of Azure response.</typeparam>
        /// <typeparam name="TEx">The type of exception associated with the response.</typeparam>
        /// <param name="value">The value associated with the response.</param>
        /// <returns>An instance of <see cref="AzGlobalValueResponse{T, TAzureResponse, TEx}"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is null.</exception>
        public static AzGlobalValueResponse<T, TAzureResponse, TEx> CreateSuccessful<T, TAzureResponse, TEx>(T value)
            where TAzureResponse : IAzureResponse
            where TEx : Exception
        {
            return new AzGlobalValueResponse<T, TAzureResponse, TEx>(ResponseStatus.Success, value);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="AzGlobalValueResponse{T, TAzureResponse, TEx}"/> class with the specified <paramref name="value"/> 
        /// and the Status set to <see cref="ResponseStatus.Warning"/>.
        /// </summary>
        /// <typeparam name="T">The type of value associated with the response.</typeparam>
        /// <typeparam name="TAzureResponse">The type of Azure response.</typeparam>
        /// <typeparam name="TEx">The type of exception associated with the response.</typeparam>
        /// <param name="value">The value associated with the response.</param>
        /// <returns>An instance of <see cref="AzGlobalValueResponse{T, TAzureResponse, TEx}"/>.</returns>
        public static AzGlobalValueResponse<T, TAzureResponse, TEx> CreateWarning<T, TAzureResponse, TEx>(T? value)
            where TAzureResponse : IAzureResponse
            where TEx : Exception
        {
            return new AzGlobalValueResponse<T, TAzureResponse, TEx>(ResponseStatus.Warning, value);
        }

        /// <summary>
        /// Creates a new instance of <see cref="AzGlobalValueResponse{T, TAzureResponse, TEx}"/> with the Status set to <see cref="ResponseStatus.Failure"/>.
        /// </summary>
        /// <typeparam name="T">The type of value associated with the response.</typeparam>
        /// <typeparam name="TAzureResponse">The type of Azure response.</typeparam>
        /// <typeparam name="TEx">The type of exception associated with the response.</typeparam>
        /// <returns>A new instance of <see cref="AzGlobalValueResponse{T, TAzureResponse, TEx}"/>.</returns>
        public static AzGlobalValueResponse<T, TAzureResponse, TEx> CreateFailure<T, TAzureResponse, TEx>()
            where TAzureResponse : IAzureResponse
            where TEx : Exception
        {
            return new AzGlobalValueResponse<T, TAzureResponse, TEx>(ResponseStatus.Failure, default);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="AzGlobalValueResponse{T, TAzureResponse, TEx}"/> class with the specified <paramref name="exception"/> 
        /// and the Status set to <see cref="ResponseStatus.Failure"/>.
        /// </summary>
        /// <typeparam name="T">The type of value associated with the response.</typeparam>
        /// <typeparam name="TAzureResponse">The type of Azure response.</typeparam>
        /// <typeparam name="TEx">The type of exception associated with the response.</typeparam>
        /// <param name="exception">The exception associated with the response.</param>
        /// <returns>An instance of <see cref="AzGlobalValueResponse{T, TAzureResponse, TEx}"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="exception"/> is null.</exception>
        public static AzGlobalValueResponse<T, TAzureResponse, TEx> CreateFailure<T, TAzureResponse, TEx>(TEx exception)
            where TAzureResponse : IAzureResponse
            where TEx : Exception
        {
            return new AzGlobalValueResponse<T, TAzureResponse, TEx>(exception);
        }

        #region CreateFrom methods

        /// <summary>
        /// Creates a new instance of the <see cref="AzGlobalValueResponse{T, TAzureResponse, TEx}"/> class with the specified <paramref name="response"/> 
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
        /// <typeparam name="TAzureResponse">The type of Azure response.</typeparam>
        /// <typeparam name="TEx">The type of exception associated with the response.</typeparam>
        /// <param name="response">The Azure response.</param>
        /// <param name="value">The value associated with the response.</param>
        /// <returns>An instance of <see cref="AzGlobalValueResponse{T, TAzureResponse, TEx}"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="response"/> is null, or 
        /// if the property 'IsError' in <paramref name="response"/> is <c>false</c> 
        /// and the <paramref name="value"/> was not specified.</exception>
        /// <exception cref="ArgumentException">Thrown if the property 'IsError' in <paramref name="response"/> is <c>true</c> 
        /// and the <paramref name="value"/> was specified</exception>
        public static AzGlobalValueResponse<T, TAzureResponse, TEx> CreateFrom<T, TAzureResponse, TEx>(TAzureResponse response, T? value)
            where TAzureResponse : IAzureResponse
            where TEx : Exception
        {
            return new AzGlobalValueResponse<T, TAzureResponse, TEx>(response, value);
        }

        #endregion

        #region MapFrom methods

        /// <summary>
        /// Maps a response of type <typeparamref name="TInAzGlobalValueResponse"/> 
        /// to a response of type <typeparamref name="TOutAzGlobalValueResponse"/>.
        /// </summary>
        /// <typeparam name="TIn">The type of the incoming value.</typeparam>
        /// <typeparam name="TOut">The type of the outgoing value.</typeparam>
        /// <typeparam name="TInAzGlobalValueResponse">The type of the incoming global value response.</typeparam>
        /// <typeparam name="TOutAzGlobalValueResponse">The type of the outgoing global value response.</typeparam>
        /// <typeparam name="TAzureResponse">The type of the Azure response.</typeparam>
        /// <typeparam name="TEx">The type of the exception.</typeparam>
        /// <param name="azGlobalValueResponse">The input response to map from.</param>
        /// <param name="convertTo">The function to convert the incoming value to the outgoing value.</param>
        /// <param name="createSuccessful">The function to create a successful outgoing response.</param>
        /// <param name="createWarning">The function to create a warning outgoing response.</param>
        /// <param name="mapFromFailure">The function to map from a failure response.</param>
        /// <param name="messageKeyExistAction">The action to take when message keys in the incoming response already exist in the outgoint response. 
        /// This parameter is used to map the messages from the incoming response to outgoing response. 
        /// Default is <see cref="KeyExistAction.Rename"/>.</param>
        /// <returns>A mapped response of type <typeparamref name="TOutAzGlobalValueResponse"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="azGlobalValueResponse"/>, <paramref name="convertTo"/>, 
        /// <paramref name="createSuccessful"/>, <paramref name="createWarning"/>, or <paramref name="mapFromFailure"/> are null.</exception>
        internal static TOutAzGlobalValueResponse CommonMapFrom<TIn, TOut, TInAzGlobalValueResponse, TOutAzGlobalValueResponse, TAzureResponse, TEx>(
            TInAzGlobalValueResponse azGlobalValueResponse,
            Func<TIn, TOut> convertTo,
            Func<TOut, TOutAzGlobalValueResponse> createSuccessful,
            Func<TOut?, TOutAzGlobalValueResponse> createWarning,
            Func<TInAzGlobalValueResponse, TOutAzGlobalValueResponse> mapFromFailure,
            KeyExistAction messageKeyExistAction = KeyExistAction.Rename)
            where TInAzGlobalValueResponse : AzGlobalValueResponse<TIn, TAzureResponse, TEx>
            where TOutAzGlobalValueResponse : AzGlobalValueResponse<TOut, TAzureResponse, TEx>
            where TAzureResponse : IAzureResponse
            where TEx : Exception
        {
            AssertHelper.AssertNotNullOrThrow(azGlobalValueResponse, nameof(azGlobalValueResponse));
            AssertHelper.AssertNotNullOrThrow(convertTo, nameof(convertTo));
            AssertHelper.AssertNotNullOrThrow(createSuccessful, nameof(createSuccessful));
            AssertHelper.AssertNotNullOrThrow(createWarning, nameof(createWarning));
            AssertHelper.AssertNotNullOrThrow(mapFromFailure, nameof(mapFromFailure));

            if (azGlobalValueResponse.Status == ResponseStatus.Failure)
                return mapFromFailure(azGlobalValueResponse);

            TOutAzGlobalValueResponse response;
            switch (azGlobalValueResponse.Status)
            {
                case ResponseStatus.Success:
                    AssertHelper.AssertNotNullOrThrow(azGlobalValueResponse.Value, $"{nameof(azGlobalValueResponse)}.{nameof(azGlobalValueResponse.Value)}");
                    response = createSuccessful(convertTo(azGlobalValueResponse.Value!));
                    break;
                case ResponseStatus.Warning:
                    if (azGlobalValueResponse.HasValue)
                        response = createWarning(convertTo(azGlobalValueResponse.Value!));
                    else
                        response = createWarning(default);

                    break;
                default:
                    throw new InvalidOperationException(nameof(azGlobalValueResponse.Status));
            }

            response.AzureResponse = azGlobalValueResponse.AzureResponse;
            response.IncludeAzureResponseVerbose = azGlobalValueResponse.IncludeAzureResponseVerbose;
            response.AddMessagesFrom(azGlobalValueResponse, messageKeyExistAction);

            return response;
        }

        /// <summary>
        /// Maps an <see cref="AzGlobalValueResponse{TIn, TAzureResponse, TEx}"/> to an <see cref="AzGlobalValueResponse{TOut, TAzureResponse, TEx}"/> 
        /// using the provided conversion function.
        /// </summary>
        /// <typeparam name="TIn">The type of the incoming value.</typeparam>
        /// <typeparam name="TOut">The type of the outgoing value.</typeparam>
        /// <typeparam name="TAzureResponse">The type of the Azure response.</typeparam>
        /// <typeparam name="TEx">The type of the exception.</typeparam>
        /// <param name="azGlobalValueResponse">The input response to map from.</param>
        /// <param name="convertTo">The function to convert the incoming value to the outgoing value.</param>
        /// <param name="messageKeyExistAction">The action to take when message keys in the incoming response already exist in the outgoint response. 
        /// This parameter is used to map the messages from the incoming response to outgoing response. 
        /// Default is <see cref="KeyExistAction.Rename"/>.</param>
        /// <returns>A mapped <see cref="AzGlobalValueResponse{TOut, TAzureResponse, TEx}"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="azGlobalValueResponse"/>, <paramref name="convertTo"/> 
        /// or <paramref name="convertTo"/> result are null.</exception>
        public static AzGlobalValueResponse<TOut, TAzureResponse, TEx> MapFrom<TIn, TOut, TAzureResponse, TEx>(
            AzGlobalValueResponse<TIn, TAzureResponse, TEx> azGlobalValueResponse,
            Func<TIn, TOut> convertTo,
            KeyExistAction messageKeyExistAction = KeyExistAction.Rename)
            where TEx : Exception
            where TAzureResponse : IAzureResponse
        {
            return CommonMapFrom<TIn, TOut, AzGlobalValueResponse<TIn, TAzureResponse, TEx>, AzGlobalValueResponse<TOut, TAzureResponse, TEx>, TAzureResponse, TEx>(
                azGlobalValueResponse,
                convertTo,
                CreateSuccessful<TOut, TAzureResponse, TEx>,
                CreateWarning<TOut, TAzureResponse, TEx>,
                MapFromFailure<TIn, TOut, TAzureResponse, TEx>,
                messageKeyExistAction);
        }

        #endregion

        #region MapFromFailure methods

        /// <summary>
        /// Creates a new instance of the <see cref="AzGlobalValueResponse{T, TAzureResponse, TEx}"/> class from the data in <paramref name="globalResponse"/>.
        /// The <paramref name="globalResponse"/> specified must contain the Status set to <see cref="ResponseStatus.Failure"/>. 
        /// The new instance contains the same status and messages of the <paramref name="globalResponse"/> specified, 
        /// but the message keys may have been modified with suffix. 
        /// If the 'Status' in <paramref name="globalResponse"/> is not <see cref="ResponseStatus.Failure"/>, 
        /// an <see cref="ArgumentException"/> will be thrown.
        /// </summary>
        /// <typeparam name="T">The type of value associated with the response.</typeparam>
        /// <typeparam name="TAzureResponse">The type of Azure response.</typeparam>
        /// <typeparam name="TEx">The type of exception associated with the response.</typeparam>
        /// <param name="globalResponse">The response from where the outgoing response will be generated.</param>
        /// <returns>An instance of <see cref="AzGlobalValueResponse{T, TAzureResponse, TEx}"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="globalResponse"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if the 'Status' in <paramref name="globalResponse"/> is not <see cref="ResponseStatus.Failure"/>.</exception>
        public static AzGlobalValueResponse<T, TAzureResponse, TEx> MapFromFailure<T, TAzureResponse, TEx>(IGlobalResponse globalResponse)
            where TAzureResponse : IAzureResponse
            where TEx : Exception
        {
            return new AzGlobalValueResponse<T, TAzureResponse, TEx>(globalResponse);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="AzGlobalValueResponse{T, TAzureResponse, TEx}"/> class from the data in <paramref name="globalResponse"/>.
        /// The <paramref name="globalResponse"/> specified must contain the Status set to <see cref="ResponseStatus.Failure"/>. 
        /// The new instance contains the same status, exception and messages of the <paramref name="globalResponse"/> specified, 
        /// but the message keys may have been modified with suffix. 
        /// If the 'Status' in <paramref name="globalResponse"/> is not <see cref="ResponseStatus.Failure"/>, 
        /// an <see cref="ArgumentException"/> will be thrown.
        /// </summary>
        /// <typeparam name="T">The type of value associated with the response.</typeparam>
        /// <typeparam name="TAzureResponse">The type of Azure response.</typeparam>
        /// <typeparam name="TEx">The type of exception associated with the response.</typeparam>
        /// <param name="globalResponse">The response from where the outgoing response will be generated.</param>
        /// <returns>An instance of <see cref="AzGlobalValueResponse{T, TAzureResponse, TEx}"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="globalResponse"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if the 'Status' in <paramref name="globalResponse"/> is not <see cref="ResponseStatus.Failure"/>.</exception>
        public static AzGlobalValueResponse<T, TAzureResponse, TEx> MapFromFailure<T, TAzureResponse, TEx>(IGlobalResponse<TEx> globalResponse)
            where TAzureResponse : IAzureResponse
            where TEx : Exception
        {
            return new AzGlobalValueResponse<T, TAzureResponse, TEx>(globalResponse);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="AzGlobalValueResponse{T, TAzureResponse, TEx}"/> class from the data in <paramref name="globalResponse"/>.
        /// The <paramref name="globalResponse"/> specified must contain the Status set to <see cref="ResponseStatus.Failure"/>. 
        /// The new instance contains the same status, exception and messages of the <paramref name="globalResponse"/> specified, 
        /// but the message keys may have been modified with suffix. 
        /// If the 'Status' in <paramref name="globalResponse"/> is not <see cref="ResponseStatus.Failure"/>, 
        /// an <see cref="ArgumentException"/> will be thrown.
        /// </summary>
        /// <typeparam name="TIn">The type of value associated with the ingoing response.</typeparam>
        /// <typeparam name="TOut">The type of value associated with the outgoing response.</typeparam>
        /// <typeparam name="TAzureResponse">The type of Azure response.</typeparam>
        /// <typeparam name="TEx">The type of exception associated with the response.</typeparam>
        /// <param name="globalResponse">The response from where the outgoing response will be generated.</param>
        /// <returns>An instance of <see cref="AzGlobalValueResponse{T, TAzureResponse, TEx}"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="globalResponse"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if the 'Status' in <paramref name="globalResponse"/> is not <see cref="ResponseStatus.Failure"/>.</exception>
        public static AzGlobalValueResponse<TOut, TAzureResponse, TEx> MapFromFailure<TIn, TOut, TAzureResponse, TEx>(IGlobalValueResponse<TIn, TEx> globalResponse)
            where TAzureResponse : IAzureResponse
            where TEx : Exception
        {
            return MapFromFailure<TOut, TAzureResponse, TEx>(globalResponse);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="AzGlobalValueResponse{T, TAzureResponse, TEx}"/> class from the data in <paramref name="azGlobalValueResponse"/>.
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
        /// <typeparam name="TAzureResponse">The type of Azure response.</typeparam>
        /// <typeparam name="TEx">The type of exception associated with the response.</typeparam>
        /// <param name="azGlobalValueResponse">The response from where the outgoing response will be generated.</param>
        /// <returns>An instance of <see cref="AzGlobalValueResponse{T, TAzureResponse, TEx}"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="azGlobalValueResponse"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if the 'Status' in <paramref name="azGlobalValueResponse"/> is not <see cref="ResponseStatus.Failure"/>,
        /// or if 'IsError' in <see cref="IAzGlobalResponse{TAzureResponse, TEx}.AzureResponse"/> is <c>false</c>.</exception>
        public static AzGlobalValueResponse<TOut, TAzureResponse, TEx> MapFromFailure<TIn, TOut, TAzureResponse, TEx>(IAzGlobalValueResponse<TIn, TAzureResponse, TEx> azGlobalValueResponse)
        where TAzureResponse : IAzureResponse
        where TEx : Exception
        {
            return new AzGlobalValueResponse<TOut, TAzureResponse, TEx>(azGlobalValueResponse);
        }

        #endregion
    }
}
