
namespace Global.Common.Azure.Models
{
    /// <inheritdoc cref="IAzureResponse"/>
    public abstract class AzureResponse : IAzureResponse
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureResponse"/> class with the specified Azure <paramref name="response"/> 
        /// and IsError equals to <see cref="Response.IsError"/> in <paramref name="response"/>.
        /// </summary>
        /// <param name="response">The Azure response associated with the error.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="response"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if <see cref="Response.Status"/> in <paramref name="response"/> 
        /// is not an <see cref="HttpStatusCode"/>.</exception>
        protected AzureResponse(Response response)
        {
            AssertHelper.AssertNotNullOrThrow(response, nameof(response));

            if (!Enum.IsDefined(typeof(HttpStatusCode), response.Status))
                throw new ArgumentException(AzureResponseConstants.StatusIsNotHttpStatusCode(nameof(response.Status), nameof(Response)), nameof(response));
                
            IsError = response.IsError;
            Status = (HttpStatusCode)response.Status;
            Response = response;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureResponse"/> class with the specified Azure <paramref name="exception"/> 
        /// and IsError set to true.
        /// </summary>
        /// <param name="exception">The exception associated with the error.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="exception"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if <see cref="RequestFailedException.Status"/> in <paramref name="exception"/> 
        /// is not an <see cref="HttpStatusCode"/>.</exception>
        public AzureResponse(RequestFailedException exception)
        {
            AssertHelper.AssertNotNullOrThrow(exception, nameof(exception));

            if (!Enum.IsDefined(typeof(HttpStatusCode), exception.Status))
                throw new ArgumentException(AzureResponseConstants.StatusIsNotHttpStatusCode(nameof(exception.Status), nameof(Response)), nameof(exception));
            
            IsError = true;
            Status = (HttpStatusCode)exception.Status;
            Exception = exception;
        }

        #endregion

        #region Properties

        /// <inheritdoc/>
        public virtual bool IsError { get; protected set; }

        /// <inheritdoc/>
        public virtual HttpStatusCode Status { get; protected set; }

        /// <inheritdoc/>
        public bool HasResponse => Response != default;

        /// <inheritdoc/>
        public virtual Response? Response { get; protected set; }

        /// <inheritdoc/>
        public bool HasException => Exception != default;

        /// <inheritdoc/>
        public virtual RequestFailedException? Exception { get; protected set; }

        /// <inheritdoc/>
        public bool HasMessage => !string.IsNullOrEmpty(Message);

        /// <inheritdoc/>
        public virtual string? Message { get; protected set; }

        #endregion

        #region IVerboseResponse implementation

        /// <summary>
        /// The default amount of elements to build the verbose collection. Default value is 8.
        /// </summary>
        protected virtual int DefaultVerboseAmount { get; set; } = 8;

        /// <inheritdoc/>
        public virtual Dictionary<string, string> BuildVerbose()
        {
            var verboseResponse = new Dictionary<string, string>(DefaultVerboseAmount);

            verboseResponse.AddOrRenameKey(nameof(IsError), IsError.ToString()); 
            verboseResponse.AddOrRenameKey(nameof(Status),
                $"{typeof(HttpStatusCode).FullName ?? GlobalResponseConstants.DefaultTypeFullNameUndefinedMessage.WrapInSingleQuotationMarks()}.{Status.ToString()}");
            verboseResponse.AddOrRenameKey(nameof(HasResponse), HasResponse.ToString());
            if (HasResponse)
            {
                verboseResponse.AddOrRenameKey($"{nameof(Response)}{GlobalResponseConstants.DefaultTypeMessageKeySuffix}",
                    Response!.GetType().FullName ?? GlobalResponseConstants.DefaultTypeFullNameUndefinedMessage);
                verboseResponse.AddOrRenameKey($"{nameof(Response)}.{nameof(Response.Status)}", Response!.Status.ToString());
                verboseResponse.AddOrRenameKey($"{nameof(Response)}.{nameof(Response.ReasonPhrase)}", Response!.ReasonPhrase);
                verboseResponse.AddOrRenameKey($"{nameof(Response)}.{nameof(Response.IsError)}", Response!.IsError.ToString());
                foreach (var header in Response.Headers)
                    verboseResponse.AddOrRenameKey($"{nameof(Response)}.{nameof(Response.Headers)}.{header.Name}", header.Value);
            }
            verboseResponse.AddOrRenameKey(nameof(HasException), HasException.ToString());
            if (HasException)
            {
                verboseResponse.AddOrRenameKey($"{nameof(Exception)}{GlobalResponseConstants.DefaultTypeMessageKeySuffix}",
                    Exception!.GetType().FullName ?? GlobalResponseConstants.DefaultTypeFullNameUndefinedMessage);
                verboseResponse.AddOrRenameKey($"{nameof(Exception)}{GlobalResponseConstants.DefaultExceptionMessageKeySuffix}",
                    Exception!.GetAllMessagesFromExceptionHierarchy());
                verboseResponse.AddOrRenameKey($"{nameof(Exception)}{GlobalResponseConstants.DefaultStackTraceKeySuffix}",
                    Exception!.GetAllStackTracesFromExceptionHierarchy() ?? GlobalResponseConstants.DefaultExceptionStackTraceUndefinedMessage);
                verboseResponse.AddOrRenameKey($"{nameof(Exception)}.{nameof(Exception.Status)}", Exception!.Status.ToString());
                verboseResponse.AddOrRenameKey($"{nameof(Exception)}.{nameof(Exception.ErrorCode)}", Exception!.ErrorCode ?? string.Empty);
            }
            verboseResponse.AddOrRenameKey(nameof(HasMessage), HasMessage.ToString());
            if (HasMessage)
                verboseResponse.AddOrRenameKey(nameof(Message), Message!);

            return verboseResponse;
        }

        #endregion
    }
}
