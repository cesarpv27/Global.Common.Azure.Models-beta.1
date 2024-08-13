
namespace Global.Common.Azure.Models
{
    /// <summary>
    /// Provides factory methods for creating instances of <see cref="AzBlobResponse{TEx}"/>.
    /// </summary>
    public static class AzBlobResponseFactory
    {
        #region AzBlobResponse<TEx>

        /// <summary>
        /// Creates a new instance of <see cref="AzBlobResponse{TEx}"/> with the Status set to <see cref="ResponseStatus.Success"/>.
        /// </summary>
        /// <typeparam name="TEx">The type of exception associated with the response.</typeparam>
        /// <returns>A new instance of <see cref="AzBlobResponse{TEx}"/>.</returns>
        public static AzBlobResponse<TEx> CreateSuccessful<TEx>()
            where TEx : Exception
        {
            return new AzBlobResponse<TEx>(ResponseStatus.Success);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="AzBlobResponse{TEx}"/> with the Status set to <see cref="ResponseStatus.Failure"/>.
        /// </summary>
        /// <typeparam name="TEx">The type of exception associated with the response.</typeparam>
        /// <returns>An instance of <see cref="AzBlobResponse{TEx}"/>.</returns>
        public static AzBlobResponse<TEx> CreateFailure<TEx>()
            where TEx : Exception
        {
            return new AzBlobResponse<TEx>(ResponseStatus.Failure);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="AzBlobResponse{TEx}"/> class with the specified <paramref name="response"/>  
        /// and the Status set to <see cref="ResponseStatus.Success"/> if the property 'IsError' of <paramref name="response"/> is <c>false</c>, 
        /// or the Status set to <see cref="ResponseStatus.Failure"/> if the property 'IsError' of <paramref name="response"/> is <c>true</c>.
        /// </summary>
        /// <typeparam name="TEx">The type of exception associated with the response.</typeparam>
        /// <param name="response">The Azure Blob Storage response.</param>
        /// <returns>An instance of <see cref="AzBlobResponse{TEx}"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="response"/> is null.</exception>
        public static AzBlobResponse<TEx> CreateFailure<TEx>(AzureBlobStorageResponse response)
            where TEx : Exception
        {
            return new AzBlobResponse<TEx>(response);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="AzBlobResponse{TEx}"/> class with the specified <paramref name="exception"/> 
        /// and the Status set to <see cref="ResponseStatus.Failure"/>.
        /// </summary>
        /// <typeparam name="TEx">The type of exception associated with the response.</typeparam>
        /// <param name="exception">The exception associated with the response.</param>
        /// <returns>An instance of <see cref="AzBlobResponse{TEx}"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="exception"/> is null.</exception>
        public static AzBlobResponse<TEx> CreateFailure<TEx>(TEx exception)
            where TEx : Exception
        {
            return new AzBlobResponse<TEx>(exception);
        }

        #region CreateFrom methods

        /// <summary>
        /// Creates a new instance of <see cref="AzBlobResponse{TEx}"/> with the specified <paramref name="response"/> from Azure Blob Storage operation.
        /// </summary>
        /// <typeparam name="TEx">The type of exception associated with the response.</typeparam>
        /// <param name="response">The response from Azure Blob Storage operation.</param>
        /// <returns>A new instance of <see cref="AzBlobResponse{TEx}"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="response"/> is null.</exception>
        public static AzBlobResponse<TEx> CreateFrom<TEx>(Response response)
            where TEx : Exception
        {
            return new AzBlobResponse<TEx>(AzureBlobStorageResponse.Create(response));
        }

        /// <summary>
        /// Creates a new instance of <see cref="AzBlobResponse{TEx}"/> with the specified <paramref name="response"/> from Azure Blob Storage operation.
        /// </summary>
        /// <typeparam name="TEx">The type of exception associated with the response.</typeparam>
        /// <typeparam name="T">The type of value in Azure blob response.</typeparam>
        /// <param name="response">The response from Azure Blob Storage operation.</param>
        /// <returns>A new instance of <see cref="AzBlobResponse{TEx}"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="response"/> is null.</exception>
        public static AzBlobResponse<TEx> CreateFrom<TEx, T>(Response<T> response)
            where TEx : Exception
        {
            return new AzBlobResponse<TEx>(AzureBlobStorageResponse.Create(response));
        }

        /// <summary>
        /// Creates a new instance of <see cref="AzBlobResponse{TEx}"/> with the specified <paramref name="exception"/> from Azure Blob Storage operation.
        /// </summary>
        /// <typeparam name="TEx">The type of exception associated with the response.</typeparam>
        /// <param name="exception">The exception from Azure Blob Storage operation.</param>
        /// <returns>A new instance of <see cref="AzBlobResponse{TEx}"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="exception"/> is null.</exception>
        public static AzBlobResponse<TEx> CreateFrom<TEx>(RequestFailedException exception)
            where TEx : Exception
        {
            return CreateFailure<TEx>(AzureBlobStorageResponse.Create(exception));
        }

        /// <summary>
        /// Creates a new instance of the <see cref="AzBlobResponse{TEx}"/> class from the data in the <paramref name="globalResponse"/> specified.
        /// The new instance contains the same status and messages of the <paramref name="globalResponse"/> specified, 
        /// but the message keys may have been modified with suffix. 
        /// </summary>
        /// <typeparam name="TEx">The type of exception associated with the response.</typeparam>
        /// <param name="globalResponse">The response from where the outgoing response will be generated.</param>
        /// <returns>An instance of <see cref="AzBlobResponse{TEx}"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="globalResponse"/> is null.</exception>
        public static AzBlobResponse<TEx> CreateFrom<TEx>(IGlobalResponse globalResponse)
            where TEx : Exception
        {
            return new AzBlobResponse<TEx>(globalResponse);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="AzBlobResponse{TEx}"/> class from the data in the specified <paramref name="globalResponse"/>.
        /// The new instance contains the same status, exception and messages of the <paramref name="globalResponse"/> specified, 
        /// but the message keys may have been modified with suffix. 
        /// </summary>
        /// <typeparam name="TEx">The type of exception associated with the response.</typeparam>
        /// <param name="globalResponse">The response from where the outgoing response will be generated.</param>
        /// <returns>An instance of <see cref="AzBlobResponse{TEx}"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="globalResponse"/> is null.</exception>
        public static AzBlobResponse<TEx> CreateFrom<TEx>(IGlobalResponse<TEx> globalResponse)
            where TEx : Exception
        {
            return new AzBlobResponse<TEx>(globalResponse);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="AzBlobResponse{TEx}"/> class from the data in <paramref name="azGlobalResponse"/>.
        /// The new instance contains the same status, exception, error and messages of the <paramref name="azGlobalResponse"/> specified, 
        /// but the message keys may have been modified with suffix. 
        /// When <see cref="IAzGlobalResponse{TAzureResponse, TEx}.AzureResponse"/> is specified in <paramref name="azGlobalResponse"/>, 
        /// if 'IsError' in <see cref="IAzGlobalResponse{TAzureResponse, TEx}.AzureResponse"/> is <c>true</c>, 
        /// the 'Status' in <paramref name="azGlobalResponse"/> must be <see cref="ResponseStatus.Failure"/>;
        /// otherwise an <see cref="ArgumentException"/> will be thrown. 
        /// </summary>
        /// <typeparam name="TEx">The type of exception associated with the response.</typeparam>
        /// <param name="azGlobalResponse">The response from where the outgoing response will be generated.</param>
        /// <returns>An instance of <see cref="AzBlobResponse{TEx}"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="azGlobalResponse"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if the property 'IsError' in <see cref="IAzGlobalResponse{TAzureResponse, TEx}.AzureResponse"/> is <c>true</c> 
        /// and the 'Status' in <paramref name="azGlobalResponse"/> is not <see cref="ResponseStatus.Failure"/>.</exception>
        public static AzBlobResponse<TEx> CreateFrom<TEx>(IAzGlobalResponse<AzureBlobStorageResponse, TEx> azGlobalResponse)
            where TEx : Exception
        {
            return new AzBlobResponse<TEx>(azGlobalResponse);
        }

        #endregion

        #region MapFromFailure methods

        /// <summary>
        /// Creates a new instance of the <see cref="AzBlobResponse{TEx}"/> class from the data in <paramref name="azGlobalValueResponse"/>. 
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
        /// <returns>An instance of <see cref="AzBlobResponse{TEx}"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="azGlobalValueResponse"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if the 'Status' in <paramref name="azGlobalValueResponse"/> is not <see cref="ResponseStatus.Failure"/>,
        /// or if 'IsError' in <see cref="IAzGlobalResponse{TAzureResponse, TEx}.AzureResponse"/> is <c>false</c>.</exception>
        public static AzBlobResponse<TEx> MapFromFailure<T, TEx>(IAzGlobalValueResponse<T, AzureBlobStorageResponse, TEx> azGlobalValueResponse)
            where TEx : Exception
        {
            AssertResponseHelper.AssertFailureNotNullOrThrow(azGlobalValueResponse, nameof(azGlobalValueResponse));

            return new AzBlobResponse<TEx>(azGlobalValueResponse, azGlobalValueResponse.AzureResponse, azGlobalValueResponse.IncludeAzureResponseVerbose);
        }

        #endregion

        #endregion

        #region AzBlobResponse

        /// <summary>
        /// Creates a new instance of <see cref="AzBlobResponse"/> with the Status set to <see cref="ResponseStatus.Success"/>.
        /// </summary>
        /// <returns>A new instance of <see cref="AzBlobResponse"/>.</returns>
        public static AzBlobResponse CreateSuccessful()
        {
            return new AzBlobResponse(ResponseStatus.Success);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="AzBlobResponse"/> with the Status set to <see cref="ResponseStatus.Failure"/>.
        /// </summary>
        /// <returns>An instance of <see cref="AzBlobResponse"/>.</returns>
        public static AzBlobResponse CreateFailure()
        {
            return new AzBlobResponse(ResponseStatus.Failure);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="AzBlobResponse"/> class with the specified <paramref name="response"/>  
        /// and the Status set to <see cref="ResponseStatus.Success"/> if the property 'IsError' of <paramref name="response"/> is <c>false</c>, 
        /// or the Status set to <see cref="ResponseStatus.Failure"/> if the property 'IsError' of <paramref name="response"/> is <c>true</c>.
        /// </summary>
        /// <param name="response">The Azure Blob Storage response.</param>
        /// <returns>An instance of <see cref="AzBlobResponse"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="response"/> is null.</exception>
        public static AzBlobResponse CreateFailure(AzureBlobStorageResponse response)
        {
            return new AzBlobResponse(response);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="AzBlobResponse"/> class with the specified <paramref name="exception"/> 
        /// and the Status set to <see cref="ResponseStatus.Failure"/>.
        /// </summary>
        /// <param name="exception">The exception associated with the response.</param>
        /// <returns>An instance of <see cref="AzBlobResponse"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="exception"/> is null.</exception>
        public static AzBlobResponse CreateFailure(Exception exception)
        {
            return new AzBlobResponse(exception);
        }

        #region CreateFrom methods

        /// <summary>
        /// Creates a new instance of <see cref="AzBlobResponse"/> with the specified <paramref name="response"/> from Azure Blob Storage operation.
        /// </summary>
        /// <param name="response">The response from Azure Blob Storage operation.</param>
        /// <returns>A new instance of <see cref="AzBlobResponse"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="response"/> is null.</exception>
        public static AzBlobResponse CreateFrom(Response response)
        {
            return new AzBlobResponse(AzureBlobStorageResponse.Create(response));
        }

        /// <summary>
        /// Creates a new instance of <see cref="AzBlobResponse"/> with the specified <paramref name="response"/> from Azure Blob Storage operation.
        /// </summary>
        /// <typeparam name="T">The type of value in Azure blob response.</typeparam>
        /// <param name="response">The response from Azure Blob Storage operation.</param>
        /// <returns>A new instance of <see cref="AzBlobResponse"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="response"/> is null.</exception>
        public static AzBlobResponse CreateFrom<T>(Response<T> response)
        {
            return new AzBlobResponse(AzureBlobStorageResponse.Create(response));
        }

        /// <summary>
        /// Creates a new instance of <see cref="AzBlobResponse"/> with the specified <paramref name="response"/> from Azure Blob Storage operation.
        /// </summary>
        /// <param name="response">The response from Azure Blob Storage operation.</param>
        /// <returns>A new instance of <see cref="AzBlobResponse"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="response"/> is null.</exception>
        public static AzBlobResponse CreateFrom(Response<BlobItem> response)
        {
            var azBlobResponse = CreateFrom(response);

            azBlobResponse.AddMessageTransactionally(
                $"{nameof(Response<BlobItem>)}.{nameof(response.Value)}.{nameof(response.Value.Name)}",
                response.Value.Name);

            azBlobResponse.AddMessageTransactionally(
                $"{nameof(Response<BlobItem>)}.FullName", 
                $"{typeof(Response<BlobItem>).FullName}");

            return azBlobResponse;
        }

        /// <summary>
        /// Creates a new instance of <see cref="AzBlobResponse"/> with the specified <paramref name="exception"/> from Azure Blob Storage operation.
        /// </summary>
        /// <param name="exception">The exception from Azure Blob Storage operation.</param>
        /// <returns>A new instance of <see cref="AzBlobResponse"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="exception"/> is null.</exception>
        public static AzBlobResponse CreateFrom(RequestFailedException exception)
        {
            return CreateFailure(AzureBlobStorageResponse.Create(exception));
        }

        /// <summary>
        /// Creates a new instance of the <see cref="AzBlobResponse"/> class from the data in the <paramref name="globalResponse"/> specified.
        /// The new instance contains the same status and messages of the <paramref name="globalResponse"/> specified, 
        /// but the message keys may have been modified with suffix. 
        /// </summary>
        /// <param name="globalResponse">The response from where the outgoing response will be generated.</param>
        /// <returns>An instance of <see cref="AzBlobResponse"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="globalResponse"/> is null.</exception>
        public static AzBlobResponse CreateFrom(IGlobalResponse globalResponse)
        {
            return new AzBlobResponse(globalResponse);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="AzBlobResponse"/> class from the data in the specified <paramref name="globalResponse"/>.
        /// The new instance contains the same status, exception and messages of the <paramref name="globalResponse"/> specified, 
        /// but the message keys may have been modified with suffix. 
        /// </summary>
        /// <param name="globalResponse">The response from where the outgoing response will be generated.</param>
        /// <returns>An instance of <see cref="AzBlobResponse"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="globalResponse"/> is null.</exception>
        public static AzBlobResponse CreateFrom(IGlobalResponse<Exception> globalResponse)
        {
            return new AzBlobResponse(globalResponse);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="AzBlobResponse"/> class from the data in <paramref name="azGlobalResponse"/>.
        /// The new instance contains the same status, exception, error and messages of the <paramref name="azGlobalResponse"/> specified, 
        /// but the message keys may have been modified with suffix. 
        /// When <see cref="IAzGlobalResponse{TAzureResponse, TEx}.AzureResponse"/> is specified in <paramref name="azGlobalResponse"/>, 
        /// if 'IsError' in <see cref="IAzGlobalResponse{TAzureResponse, TEx}.AzureResponse"/> is <c>true</c>, 
        /// the 'Status' in <paramref name="azGlobalResponse"/> must be <see cref="ResponseStatus.Failure"/>;
        /// otherwise an <see cref="ArgumentException"/> will be thrown. 
        /// </summary>
        /// <param name="azGlobalResponse">The response from where the outgoing response will be generated.</param>
        /// <returns>An instance of <see cref="AzBlobResponse"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="azGlobalResponse"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if the property 'IsError' in <see cref="IAzGlobalResponse{TAzureResponse, TEx}.AzureResponse"/> is <c>true</c> 
        /// and the 'Status' in <paramref name="azGlobalResponse"/> is not <see cref="ResponseStatus.Failure"/>.</exception>
        public static AzBlobResponse CreateFrom(IAzGlobalResponse<AzureBlobStorageResponse, Exception> azGlobalResponse)
        {
            return new AzBlobResponse(azGlobalResponse);
        }

        #endregion

        #region MapFromFailure methods

        /// <summary>
        /// Creates a new instance of the <see cref="AzBlobResponse"/> class from the data in <paramref name="azGlobalValueResponse"/>. 
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
        /// <returns>An instance of <see cref="AzBlobResponse"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="azGlobalValueResponse"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if the 'Status' in <paramref name="azGlobalValueResponse"/> is not <see cref="ResponseStatus.Failure"/>,
        /// or if 'IsError' in <see cref="IAzGlobalResponse{TAzureResponse, TEx}.AzureResponse"/> is <c>false</c>..</exception>
        public static AzBlobResponse MapFromFailure<T>(IAzGlobalValueResponse<T, AzureBlobStorageResponse, Exception> azGlobalValueResponse)
        {
            return new AzBlobResponse(azGlobalValueResponse, azGlobalValueResponse.AzureResponse, azGlobalValueResponse.IncludeAzureResponseVerbose);
        }

        #endregion

        #endregion
    }
}
