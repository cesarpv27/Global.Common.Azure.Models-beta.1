
namespace Global.Common.Azure.Models
{
    /// <summary>
    /// Provides factory methods for creating instances of <see cref="AzGlobalResponse{TAzureResponse, TEx}"/>.
    /// </summary>
    public static class AzGlobalResponseFactory
    {
        /// <summary>
        /// Creates a new instance of <see cref="AzGlobalResponse{TAzureResponse, TEx}"/> with the Status set to <see cref="ResponseStatus.Success"/>.
        /// </summary>
        /// <typeparam name="TAzureResponse">The type of Azure response.</typeparam>
        /// <typeparam name="TEx">The type of exception associated with the response.</typeparam>
        /// <returns>A new instance of <see cref="AzGlobalResponse{TAzureResponse, TEx}"/>.</returns>
        public static AzGlobalResponse<TAzureResponse, TEx> CreateSuccessful<TAzureResponse, TEx>()
            where TAzureResponse : IAzureResponse
            where TEx : Exception
        {
            return new AzGlobalResponse<TAzureResponse, TEx>(ResponseStatus.Success);
        }

        /// <summary>
        /// Creates a new instance of <see cref="AzGlobalResponse{TAzureResponse, TEx}"/> with the Status set to <see cref="ResponseStatus.Warning"/>.
        /// </summary>
        /// <typeparam name="TAzureResponse">The type of Azure response.</typeparam>
        /// <typeparam name="TEx">The type of exception associated with the response.</typeparam>
        /// <returns>A new instance of <see cref="AzGlobalResponse{TAzureResponse, TEx}"/>.</returns>
        public static AzGlobalResponse<TAzureResponse, TEx> CreateWarning<TAzureResponse, TEx>()
            where TAzureResponse : IAzureResponse
            where TEx : Exception
        {
            return new AzGlobalResponse<TAzureResponse, TEx>(ResponseStatus.Warning);
        }

        /// <summary>
        /// Creates a new instance of <see cref="AzGlobalResponse{TAzureResponse, TEx}"/> with the Status set to <see cref="ResponseStatus.Failure"/>.
        /// </summary>
        /// <typeparam name="TAzureResponse">The type of Azure response.</typeparam>
        /// <typeparam name="TEx">The type of exception associated with the response.</typeparam>
        /// <returns>A new instance of <see cref="AzGlobalResponse{TAzureResponse, TEx}"/>.</returns>
        public static AzGlobalResponse<TAzureResponse, TEx> CreateFailure<TAzureResponse, TEx>()
            where TAzureResponse : IAzureResponse
            where TEx : Exception
        {
            return new AzGlobalResponse<TAzureResponse, TEx>(ResponseStatus.Failure);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="AzGlobalResponse{TAzureResponse, TEx}"/> class with the specified <paramref name="response"/>  
        /// and the Status set to <see cref="ResponseStatus.Success"/> if the property 'IsError' of <paramref name="response"/> is <c>false</c>, 
        /// or the Status set to <see cref="ResponseStatus.Failure"/> if the property 'IsError' of <paramref name="response"/> is <c>true</c>.
        /// </summary>
        /// <typeparam name="TAzureResponse">The type of Azure response.</typeparam>
        /// <typeparam name="TEx">The type of exception associated with the response.</typeparam>
        /// <returns>An instance of <see cref="AzGlobalResponse{TAzureResponse, TEx}"/>.</returns>
        /// <param name="response">The Azure response.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="response"/> is null.</exception>
        public static AzGlobalResponse<TAzureResponse, TEx> CreateFailure<TAzureResponse, TEx>(TAzureResponse response)
            where TAzureResponse : IAzureResponse
            where TEx : Exception
        {
            return new AzGlobalResponse<TAzureResponse, TEx>(response);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="AzGlobalResponse{TAzureResponse, TEx}"/> class with the specified <paramref name="exception"/> 
        /// and the Status set to <see cref="ResponseStatus.Failure"/>.
        /// </summary>
        /// <typeparam name="TAzureResponse">The type of Azure response.</typeparam>
        /// <typeparam name="TEx">The type of exception associated with the response.</typeparam>
        /// <param name="exception">The exception associated with the response.</param>
        /// <returns>An instance of <see cref="AzGlobalResponse{TAzureResponse, TEx}"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="exception"/> is null.</exception>
        public static AzGlobalResponse<TAzureResponse, TEx> CreateFailure<TAzureResponse, TEx>(TEx exception)
            where TAzureResponse : IAzureResponse
            where TEx : Exception
        {
            return new AzGlobalResponse<TAzureResponse, TEx>(exception);
        }

        #region CreateFrom methods

        /// <summary>
        /// Creates a new instance of the <see cref="AzGlobalResponse{TAzureResponse, TEx}"/> class from the data in the <paramref name="globalResponse"/> specified.
        /// The new instance contains the same status and messages of the <paramref name="globalResponse"/> specified, 
        /// but the message keys may have been modified with suffix. 
        /// </summary>
        /// <typeparam name="TAzureResponse">The type of Azure response.</typeparam>
        /// <typeparam name="TEx">The type of exception associated with the response.</typeparam>
        /// <param name="globalResponse">The response from where the outgoing response will be generated.</param>
        /// <returns>An instance of <see cref="AzGlobalResponse{TAzureResponse, TEx}"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="globalResponse"/> is null.</exception>
        public static AzGlobalResponse<TAzureResponse, TEx> CreateFrom<TAzureResponse, TEx>(IGlobalResponse globalResponse)
            where TAzureResponse : IAzureResponse
            where TEx : Exception
        {
            return new AzGlobalResponse<TAzureResponse, TEx>(globalResponse);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="AzGlobalResponse{TAzureResponse, TEx}"/> class from the data in the specified <paramref name="globalResponse"/>.
        /// The new instance contains the same status, exception and messages of the <paramref name="globalResponse"/> specified, 
        /// but the message keys may have been modified with suffix. 
        /// </summary>
        /// <typeparam name="TAzureResponse">The type of Azure response.</typeparam>
        /// <typeparam name="TEx">The type of exception associated with the response.</typeparam>
        /// <param name="globalResponse">The response from where the outgoing response will be generated.</param>
        /// <returns>An instance of <see cref="AzGlobalResponse{TAzureResponse, TEx}"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="globalResponse"/> is null.</exception>
        public static AzGlobalResponse<TAzureResponse, TEx> CreateFrom<TAzureResponse, TEx>(IGlobalResponse<TEx> globalResponse)
            where TAzureResponse : IAzureResponse
            where TEx : Exception
        {
            return new AzGlobalResponse<TAzureResponse, TEx>(globalResponse);
        }

        #endregion

        #region MapFromFailure methods

        /// <summary>
        /// Creates a new instance of the <see cref="AzGlobalResponse{TAzureResponse, TEx}"/> class from the data in <paramref name="azGlobalValueResponse"/>. 
        /// The <paramref name="azGlobalValueResponse"/> specified must contain the Status set to <see cref="ResponseStatus.Failure"/>. 
        /// If the 'Status' in <paramref name="azGlobalValueResponse"/> is not <see cref="ResponseStatus.Failure"/>, 
        /// an <see cref="ArgumentException"/> will be thrown. 
        /// The new instance contains the same status, exception, error and messages of the <paramref name="azGlobalValueResponse"/>, 
        /// but the message keys may have been modified with suffix. 
        /// When <see cref="IAzGlobalResponse{TAzureResponse, TEx}.AzureResponse"/> is specified, 
        /// if 'IsError' in <see cref="IAzGlobalResponse{TAzureResponse, TEx}.AzureResponse"/> is <c>false</c> an <see cref="ArgumentException"/> will be thrown. 
        /// </summary>
        /// <typeparam name="T">The type of value associated with the response.</typeparam>
        /// <typeparam name="TAzureResponse">The type of Azure response.</typeparam>
        /// <typeparam name="TEx">The type of exception associated with the response.</typeparam>
        /// <param name="azGlobalValueResponse">The response from where the outgoing response will be generated.</param>
        /// <returns>An instance of <see cref="AzGlobalResponse{TAzureResponse, TEx}"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="azGlobalValueResponse"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if the 'Status' in <paramref name="azGlobalValueResponse"/> is not <see cref="ResponseStatus.Failure"/>,
        /// or if 'IsError' in <see cref="IAzGlobalResponse{TAzureResponse, TEx}.AzureResponse"/> is <c>false</c>.</exception>
        public static AzGlobalResponse<TAzureResponse, TEx> MapFromFailure<T, TAzureResponse, TEx>(IAzGlobalValueResponse<T, TAzureResponse, TEx> azGlobalValueResponse)
            where TAzureResponse : IAzureResponse
            where TEx : Exception
        {
            return new AzGlobalResponse<TAzureResponse, TEx>(azGlobalValueResponse, azGlobalValueResponse.AzureResponse, azGlobalValueResponse.IncludeAzureResponseVerbose);
        }

        #endregion
    }
}
