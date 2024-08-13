
namespace Global.Common.Azure.Models
{
    /// <summary>
    /// Provides factory methods for creating instances of <see cref="AzTableResponse{TEx}"/>.
    /// </summary>
    public static class AzTableResponseFactory
    {
        #region AzTableResponse<TEx>

        /// <summary>
        /// Creates a new instance of <see cref="AzTableResponse{TEx}"/> with the Status set to <see cref="ResponseStatus.Success"/>.
        /// </summary>
        /// <typeparam name="TEx">The type of exception associated with the response.</typeparam>
        /// <returns>A new instance of <see cref="AzTableResponse{TEx}"/>.</returns>
        public static AzTableResponse<TEx> CreateSuccessful<TEx>()
            where TEx : Exception
        {
            return new AzTableResponse<TEx>(ResponseStatus.Success);
        }

        /// <summary>
        /// Creates a new instance of <see cref="AzTableResponse{TEx}"/> with the Status set to <see cref="ResponseStatus.Warning"/>.
        /// </summary>
        /// <typeparam name="TEx">The type of exception associated with the response.</typeparam>
        /// <returns>A new instance of <see cref="AzTableResponse{TEx}"/>.</returns>
        public static AzTableResponse<TEx> CreateWarning<TEx>()
            where TEx : Exception
        {
            return new AzTableResponse<TEx>(ResponseStatus.Warning);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="AzTableResponse{TEx}"/> with the Status set to <see cref="ResponseStatus.Failure"/>.
        /// </summary>
        /// <typeparam name="TEx">The type of exception associated with the response.</typeparam>
        /// <returns>An instance of <see cref="AzTableResponse{TEx}"/>.</returns>
        public static AzTableResponse<TEx> CreateFailure<TEx>()
            where TEx : Exception
        {
            return new AzTableResponse<TEx>(ResponseStatus.Failure);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="AzTableResponse{TEx}"/> class with the specified <paramref name="response"/>  
        /// and the Status set to <see cref="ResponseStatus.Success"/> if the property 'IsError' of <paramref name="response"/> is <c>false</c>, 
        /// or the Status set to <see cref="ResponseStatus.Failure"/> if the property 'IsError' of <paramref name="response"/> is <c>true</c>.
        /// </summary>
        /// <typeparam name="TEx">The type of exception associated with the response.</typeparam>
        /// <param name="response">The Azure Table Service response.</param>
        /// <returns>An instance of <see cref="AzTableResponse{TEx}"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="response"/> is null.</exception>
        public static AzTableResponse<TEx> CreateFailure<TEx>(AzureTableServiceResponse response)
            where TEx : Exception
        {
            return new AzTableResponse<TEx>(response);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="AzTableResponse{TEx}"/> class with the specified <paramref name="exception"/> 
        /// and the Status set to <see cref="ResponseStatus.Failure"/>.
        /// </summary>
        /// <typeparam name="TEx">The type of exception associated with the response.</typeparam>
        /// <param name="exception">The exception associated with the response.</param>
        /// <returns>An instance of <see cref="AzTableResponse{TEx}"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="exception"/> is null.</exception>
        public static AzTableResponse<TEx> CreateFailure<TEx>(TEx exception)
            where TEx : Exception
        {
            return new AzTableResponse<TEx>(exception);
        }

        #region CreateFrom methods

        /// <summary>
        /// Creates a new instance of <see cref="AzTableResponse{TEx}"/> with the specified <paramref name="response"/> from Azure Table Service operation.
        /// </summary>
        /// <typeparam name="TEx">The type of exception associated with the response.</typeparam>
        /// <param name="response">The response from Azure Table Service operation.</param>
        /// <returns>A new instance of <see cref="AzTableResponse{TEx}"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="response"/> is null.</exception>
        public static AzTableResponse<TEx> CreateFrom<TEx>(Response response)
            where TEx : Exception
        {
            return new AzTableResponse<TEx>(AzureTableServiceResponse.Create(response));
        }

        /// <summary>
        /// Creates a new instance of <see cref="AzTableResponse{TEx}"/> with the specified <paramref name="response"/> from Azure Table Service operation.
        /// </summary>
        /// <typeparam name="TEx">The type of exception associated with the response.</typeparam>
        /// <typeparam name="T">The type of value in Azure table response.</typeparam>
        /// <param name="response">The response from Azure Table Service operation.</param>
        /// <returns>A new instance of <see cref="AzTableResponse{TEx}"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="response"/> is null.</exception>
        public static AzTableResponse<TEx> CreateFrom<TEx, T>(Response<T> response)
            where TEx : Exception
        {
            return new AzTableResponse<TEx>(AzureTableServiceResponse.Create(response));
        }

        /// <summary>
        /// Creates a new instance of <see cref="AzTableResponse{TEx}"/> with the specified <paramref name="exception"/> from Azure Table Service operation 
        /// and the Status set to <see cref="ResponseStatus.Failure"/>.
        /// </summary>
        /// <typeparam name="TEx">The type of exception associated with the response.</typeparam>
        /// <param name="exception">The exception from Azure Table Service operation.</param>
        /// <returns>A new instance of <see cref="AzTableResponse{TEx}"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="exception"/> is null.</exception>
        public static AzTableResponse<TEx> CreateFrom<TEx>(RequestFailedException exception)
            where TEx : Exception
        {
            return CreateFailure<TEx>(AzureTableServiceResponse.Create(exception));
        }

        /// <summary>
        /// Creates a new instance of the <see cref="AzTableResponse{TEx}"/> class from the data in the <paramref name="globalResponse"/> specified.
        /// The new instance contains the same status and messages of the <paramref name="globalResponse"/> specified, 
        /// but the message keys may have been modified with suffix. 
        /// </summary>
        /// <typeparam name="TEx">The type of exception associated with the response.</typeparam>
        /// <param name="globalResponse">The response from where the outgoing response will be generated.</param>
        /// <returns>An instance of <see cref="AzTableResponse{TEx}"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="globalResponse"/> is null.</exception>
        public static AzTableResponse<TEx> CreateFrom<TEx>(IGlobalResponse globalResponse)
            where TEx : Exception
        {
            return new AzTableResponse<TEx>(globalResponse);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="AzTableResponse{TEx}"/> class from the data in the specified <paramref name="globalResponse"/>.
        /// The new instance contains the same status, exception and messages of the <paramref name="globalResponse"/> specified, 
        /// but the message keys may have been modified with suffix. 
        /// </summary>
        /// <typeparam name="TEx">The type of exception associated with the response.</typeparam>
        /// <param name="globalResponse">The response from where the outgoing response will be generated.</param>
        /// <returns>An instance of <see cref="AzTableResponse{TEx}"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="globalResponse"/> is null.</exception>
        public static AzTableResponse<TEx> CreateFrom<TEx>(IGlobalResponse<TEx> globalResponse)
            where TEx : Exception
        {
            return new AzTableResponse<TEx>(globalResponse);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="AzTableResponse{TEx}"/> class from the data in <paramref name="azGlobalResponse"/>.
        /// The new instance contains the same status, exception, error and messages of the <paramref name="azGlobalResponse"/> specified, 
        /// but the message keys may have been modified with suffix. 
        /// When <see cref="IAzGlobalResponse{TAzureResponse, TEx}.AzureResponse"/> is specified in <paramref name="azGlobalResponse"/>, 
        /// if 'IsError' in <see cref="IAzGlobalResponse{TAzureResponse, TEx}.AzureResponse"/> is <c>true</c>, 
        /// the 'Status' in <paramref name="azGlobalResponse"/> must be <see cref="ResponseStatus.Failure"/>;
        /// otherwise an <see cref="ArgumentException"/> will be thrown. 
        /// </summary>
        /// <typeparam name="TEx">The type of exception associated with the response.</typeparam>
        /// <param name="azGlobalResponse">The response from where the outgoing response will be generated.</param>
        /// <returns>An instance of <see cref="AzTableResponse{TEx}"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="azGlobalResponse"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if the property 'IsError' in <see cref="IAzGlobalResponse{TAzureResponse, TEx}.AzureResponse"/> is <c>true</c> 
        /// and the 'Status' in <paramref name="azGlobalResponse"/> is not <see cref="ResponseStatus.Failure"/>.</exception>
        public static AzTableResponse<TEx> CreateFrom<TEx>(IAzGlobalResponse<AzureTableServiceResponse, TEx> azGlobalResponse)
            where TEx : Exception
        {
            return new AzTableResponse<TEx>(azGlobalResponse);
        }

        #endregion

        #region MapFromFailure methods

        /// <summary>
        /// Creates a new instance of the <see cref="AzTableResponse{TEx}"/> class from the data in <paramref name="azGlobalValueResponse"/>. 
        /// The <paramref name="azGlobalValueResponse"/> specified must contain the Status set to <see cref="ResponseStatus.Failure"/>. 
        /// If the 'Status' in <paramref name="azGlobalValueResponse"/> is not <see cref="ResponseStatus.Failure"/>, 
        /// an <see cref="ArgumentException"/> will be thrown. 
        /// The new instance contains the same status, exception, error and messages of the <paramref name="azGlobalValueResponse"/>, 
        /// but the message keys may have been modified with suffix. 
        /// When <see cref="IAzGlobalResponse{TAzureResponse, TEx}.AzureResponse"/> is specified, 
        /// if 'IsError' in <see cref="IAzGlobalResponse{TAzureResponse, TEx}.AzureResponse"/> is <c>false</c> an <see cref="ArgumentException"/> will be thrown. 
        /// </summary>
        /// <typeparam name="T">The type of value associated with the response.</typeparam>
        /// <typeparam name="TEx">The type of exception associated with the response.</typeparam>
        /// <param name="azGlobalValueResponse">The response from where the outgoing response will be generated.</param>
        /// <returns>An instance of <see cref="AzTableResponse{TEx}"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="azGlobalValueResponse"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if the 'Status' in <paramref name="azGlobalValueResponse"/> is not <see cref="ResponseStatus.Failure"/>,
        /// or if 'IsError' in <see cref="IAzGlobalResponse{TAzureResponse, TEx}.AzureResponse"/> is <c>false</c>.</exception>
        public static AzTableResponse<TEx> MapFromFailure<T, TEx>(IAzGlobalValueResponse<T, AzureTableServiceResponse, TEx> azGlobalValueResponse)
            where TEx : Exception
        {
            return new AzTableResponse<TEx>(azGlobalValueResponse, azGlobalValueResponse.AzureResponse, azGlobalValueResponse.IncludeAzureResponseVerbose);
        }

        #endregion

        #endregion

        #region AzTableResponse

        /// <summary>
        /// Creates a new instance of <see cref="AzTableResponse"/> with the Status set to <see cref="ResponseStatus.Success"/>.
        /// </summary>
        /// <returns>A new instance of <see cref="AzTableResponse"/>.</returns>
        public static AzTableResponse CreateSuccessful()
        {
            return new AzTableResponse(ResponseStatus.Success);
        }

        /// <summary>
        /// Creates a new instance of <see cref="AzTableResponse"/> with the Status set to <see cref="ResponseStatus.Warning"/>.
        /// </summary>
        /// <returns>A new instance of <see cref="AzTableResponse"/>.</returns>
        public static AzTableResponse CreateWarning()
        {
            return new AzTableResponse(ResponseStatus.Warning);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="AzTableResponse"/> with the Status set to <see cref="ResponseStatus.Failure"/>.
        /// </summary>
        /// <returns>An instance of <see cref="AzTableResponse"/>.</returns>
        public static AzTableResponse CreateFailure()
        {
            return new AzTableResponse(ResponseStatus.Failure);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="AzTableResponse"/> class with the specified <paramref name="response"/>  
        /// and the Status set to <see cref="ResponseStatus.Success"/> if the property 'IsError' of <paramref name="response"/> is <c>false</c>, 
        /// or the Status set to <see cref="ResponseStatus.Failure"/> if the property 'IsError' of <paramref name="response"/> is <c>true</c>.
        /// </summary>
        /// <param name="response">The Azure Table Service response.</param>
        /// <returns>An instance of <see cref="AzTableResponse"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="response"/> is null.</exception>
        public static AzTableResponse CreateFailure(AzureTableServiceResponse response)
        {
            return new AzTableResponse(response);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="AzTableResponse"/> class with the specified <paramref name="exception"/> 
        /// and the Status set to <see cref="ResponseStatus.Failure"/>.
        /// </summary>
        /// <param name="exception">The exception associated with the response.</param>
        /// <returns>An instance of <see cref="AzTableResponse"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="exception"/> is null.</exception>
        public static AzTableResponse CreateFailure(Exception exception)
        {
            return new AzTableResponse(exception);
        }

        #region CreateFrom methods

        /// <summary>
        /// Creates a new instance of <see cref="AzTableResponse"/> with the specified <paramref name="response"/> from Azure Table Service operation.
        /// </summary>
        /// <param name="response">The response from Azure Table Service operation.</param>
        /// <returns>A new instance of <see cref="AzTableResponse"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="response"/> is null.</exception>
        public static AzTableResponse CreateFrom(Response response)
        {
            return new AzTableResponse(AzureTableServiceResponse.Create(response));
        }

        /// <summary>
        /// Creates a new instance of <see cref="AzTableResponse"/> with the specified <paramref name="response"/> from Azure Table Service operation.
        /// </summary>
        /// <typeparam name="T">The type of value in Azure table response.</typeparam>
        /// <param name="response">The response from Azure Table Service operation.</param>
        /// <returns>A new instance of <see cref="AzTableResponse"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="response"/> is null.</exception>
        public static AzTableResponse CreateFrom<T>(Response<T> response)
        {
            return new AzTableResponse(AzureTableServiceResponse.Create(response));
        }

        /// <summary>
        /// Creates a new instance of <see cref="AzTableResponse"/> with the specified <paramref name="response"/> from Azure Table Service operation.
        /// </summary>
        /// <param name="response">The response from Azure Table Service operation.</param>
        /// <returns>A new instance of <see cref="AzTableResponse"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="response"/> is null.</exception>
        public static AzTableResponse CreateFrom(Response<TableItem> response)
        {
            var azTableResponse = new AzTableResponse(AzureTableServiceResponse.Create(response));

            azTableResponse.AddMessageTransactionally(
                $"{nameof(Response<TableItem>)}.{nameof(response.Value)}.{nameof(response.Value.Name)}",
                response.Value.Name);

            azTableResponse.AddMessageTransactionally(
                $"{nameof(Response<TableItem>)}.FullName", 
                $"{typeof(Response<TableItem>).FullName}");

            return azTableResponse;
        }

        /// <summary>
        /// Creates a new instance of <see cref="AzTableResponse"/> with the specified <paramref name="exception"/> from Azure Table Service operation.
        /// </summary>
        /// <param name="exception">The exception from Azure Table Service operation.</param>
        /// <returns>A new instance of <see cref="AzTableResponse"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="exception"/> is null.</exception>
        public static AzTableResponse CreateFrom(RequestFailedException exception)
        {
            return CreateFailure(AzureTableServiceResponse.Create(exception));
        }

        /// <summary>
        /// Creates a new instance of the <see cref="AzTableResponse"/> class from the data in the <paramref name="globalResponse"/> specified.
        /// The new instance contains the same status and messages of the <paramref name="globalResponse"/> specified, 
        /// but the message keys may have been modified with suffix. 
        /// </summary>
        /// <param name="globalResponse">The response from where the outgoing response will be generated.</param>
        /// <returns>An instance of <see cref="AzTableResponse"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="globalResponse"/> is null.</exception>
        public static AzTableResponse CreateFrom(IGlobalResponse globalResponse)
        {
            return new AzTableResponse(globalResponse);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="AzTableResponse"/> class from the data in the specified <paramref name="globalResponse"/>.
        /// The new instance contains the same status, exception and messages of the <paramref name="globalResponse"/> specified, 
        /// but the message keys may have been modified with suffix. 
        /// </summary>
        /// <param name="globalResponse">The response from where the outgoing response will be generated.</param>
        /// <returns>An instance of <see cref="AzTableResponse"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="globalResponse"/> is null.</exception>
        public static AzTableResponse CreateFrom(IGlobalResponse<Exception> globalResponse)
        {
            return new AzTableResponse(globalResponse);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="AzTableResponse"/> class from the data in <paramref name="azGlobalResponse"/>.
        /// The new instance contains the same status, exception, error and messages of the <paramref name="azGlobalResponse"/> specified, 
        /// but the message keys may have been modified with suffix. 
        /// When <see cref="IAzGlobalResponse{TAzureResponse, TEx}.AzureResponse"/> is specified in <paramref name="azGlobalResponse"/>, 
        /// if 'IsError' in <see cref="IAzGlobalResponse{TAzureResponse, TEx}.AzureResponse"/> is <c>true</c>, 
        /// the 'Status' in <paramref name="azGlobalResponse"/> must be <see cref="ResponseStatus.Failure"/>;
        /// otherwise an <see cref="ArgumentException"/> will be thrown. 
        /// </summary>
        /// <param name="azGlobalResponse">The response from where the outgoing response will be generated.</param>
        /// <returns>An instance of <see cref="AzTableResponse"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="azGlobalResponse"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if the property 'IsError' in <see cref="IAzGlobalResponse{TAzureResponse, TEx}.AzureResponse"/> is <c>true</c> 
        /// and the 'Status' in <paramref name="azGlobalResponse"/> is not <see cref="ResponseStatus.Failure"/>.</exception>
        public static AzTableResponse CreateFrom(IAzGlobalResponse<AzureTableServiceResponse, Exception> azGlobalResponse)
        {
            return new AzTableResponse(azGlobalResponse);
        }

        #endregion

        #region MapFromFailure methods

        /// <summary>
        /// Creates a new instance of the <see cref="AzTableResponse"/> class from the data in <paramref name="azGlobalValueResponse"/>. 
        /// The <paramref name="azGlobalValueResponse"/> specified must contain the Status set to <see cref="ResponseStatus.Failure"/>. 
        /// If the 'Status' in <paramref name="azGlobalValueResponse"/> is not <see cref="ResponseStatus.Failure"/>, 
        /// an <see cref="ArgumentException"/> will be thrown. 
        /// The new instance contains the same status, exception, error and messages of the <paramref name="azGlobalValueResponse"/>, 
        /// but the message keys may have been modified with suffix. 
        /// When <see cref="IAzGlobalResponse{TAzureResponse, TEx}.AzureResponse"/> is specified, 
        /// if 'IsError' in <see cref="IAzGlobalResponse{TAzureResponse, TEx}.AzureResponse"/> is <c>false</c> an <see cref="ArgumentException"/> will be thrown. 
        /// </summary>
        /// <typeparam name="T">The type of value associated with the response.</typeparam>
        /// <param name="azGlobalValueResponse">The response from where the outgoing response will be generated.</param>
        /// <returns>An instance of <see cref="AzTableResponse"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="azGlobalValueResponse"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if the 'Status' in <paramref name="azGlobalValueResponse"/> is not <see cref="ResponseStatus.Failure"/>,
        /// or if 'IsError' in <see cref="IAzGlobalResponse{TAzureResponse, TEx}.AzureResponse"/> is <c>false</c>.</exception>
        public static AzTableResponse MapFromFailure<T>(IAzGlobalValueResponse<T, AzureTableServiceResponse, Exception> azGlobalValueResponse)
        {
            return new AzTableResponse(azGlobalValueResponse, azGlobalValueResponse.AzureResponse, azGlobalValueResponse.IncludeAzureResponseVerbose);
        }

        #endregion

        #endregion
    }
}
