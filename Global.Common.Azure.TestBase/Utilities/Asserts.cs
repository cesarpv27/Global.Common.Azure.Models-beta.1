
namespace Global.Common.Azure.TestBase
{
    /// <summary>
    /// Provides assertion methods for various conditions.
    /// </summary>
    public static class Asserts
    {
        /// <summary>
        /// Assert <paramref name="value"/> not null.
        /// </summary>
        /// <typeparam name="TIn">Type of the value.</typeparam>
        /// <param name="value">The value to assert.</param>
        public static void NotNull<TIn>(TIn value)
        {
            Assert.NotNull(value);
        }

        /// <summary>
        /// Assert <paramref name="response"/> not null and <see cref="IGlobalResponse.Status"/> iguals to <see cref="ResponseStatus.Success"/>.
        /// </summary>
        /// <typeparam name="TResponse">Type of the response.</typeparam>
        /// <param name="response">The response to assert.</param>
        public static void SuccessResponse<TResponse>(TResponse? response) where TResponse : class, IGlobalResponse
        {
            NotNull(response);
            Assert.Equal(ResponseStatus.Success, response!.Status);
        }

        /// <summary>
        /// Assert <paramref name="response"/> not null and <see cref="IGlobalResponse.Status"/> iguals to <see cref="ResponseStatus.Failure"/>.
        /// </summary>
        /// <typeparam name="TResponse">Type of the response.</typeparam>
        /// <param name="response">The response to assert.</param>
        public static void FailedResponse<TResponse>(TResponse? response) where TResponse : class, IGlobalResponse
        {
            NotNull(response);
            Assert.Equal(ResponseStatus.Failure, response!.Status);
        }

        /// <summary>
        /// Assert <paramref name="response"/> not null and <see cref="IGlobalResponse.Status"/> iguals to <see cref="ResponseStatus.Failure"/>.
        /// </summary>
        /// <typeparam name="TResponse">Type of the response.</typeparam>
        /// <typeparam name="TEx">The custom type of exception in the response.</typeparam>
        /// <param name="response">The response to assert.</param>
        /// <param name="expectedExceptionParamName">The expected parameter name of the exception.</param>
        public static void FailedResponse<TResponse, TEx>(
            TResponse response,
            string expectedExceptionParamName)
            where TResponse : IGlobalResponse<Exception>
            where TEx : Exception
        {
            NotNull(response);
            Assert.Equal(ResponseStatus.Failure, response.Status);

            Assert.NotNull(response.Exception);
            Assert.IsType<TEx>(response.Exception!);
            Assert.Equal(expectedExceptionParamName, ((ArgumentException)response.Exception).ParamName);
        }

        /// <summary>
        /// Asserts that the specified <paramref name="response"/> is not null, contains a message with the given <paramref name="messageKey"/>, 
        /// and that the message matches the expected <paramref name="expectedMessage"/>.
        /// </summary>
        /// <summary>
        /// Asserts the message in the <paramref name="response"/>.
        /// </summary>
        /// <typeparam name="TResponse">The type of response.</typeparam>
        /// <param name="response">The response.</param>
        /// <param name="messageKey">The key of the message to assert in the response.</param>
        /// <param name="expectedMessage">The expected message value.</param>
        public static void ResponseWithMessage<TResponse>(
            TResponse response,
            string messageKey,
            string expectedMessage) where TResponse : class, IGlobalResponse
        {
            NotNull(response);

            Assert.True(response.TryGetMessage(messageKey, out string? actualMessage));
            Assert.Equal(expectedMessage, actualMessage);
        }

        /// <summary>
        /// Assert <paramref name="response"/> not null and <see cref="IGlobalResponse.Status"/> iguals to <see cref="ResponseStatus.Success"/>, 
        /// and contains a message with the given <paramref name="messageKey"/>, 
        /// and that the message matches the expected <paramref name="expectedMessage"/>.
        /// </summary>
        /// <typeparam name="TResponse">The type of response.</typeparam>
        /// <param name="response">The response.</param>
        /// <param name="messageKey">The key of the message to assert in the response.</param>
        /// <param name="expectedMessage">The expected message value.</param>
        public static void SuccessResponseWithMessage<TResponse>(
            TResponse response,
            string messageKey,
            string expectedMessage) where TResponse : class, IGlobalResponse
        {
            NotNull(response);
            SuccessResponse(response);

            ResponseWithMessage(response, messageKey, expectedMessage);
        }

        /// <summary>
        /// Assert <paramref name="response"/> not null and <see cref="IGlobalResponse.Status"/> iguals to <see cref="ResponseStatus.Failure"/>, 
        /// and contains a message with the given <paramref name="messageKey"/>, 
        /// and that the message matches the expected <paramref name="expectedMessage"/>.
        /// </summary>
        /// <typeparam name="TResponse">The type of response.</typeparam>
        /// <param name="response">The response.</param>
        /// <param name="messageKey">The key of the message to assert in the response.</param>
        /// <param name="expectedMessage">The expected message value.</param>
        public static void FailedResponseWithMessage<TResponse>(
            TResponse response,
            string messageKey,
            string expectedMessage) where TResponse : class, IGlobalResponse
        {
            NotNull(response);
            FailedResponse(response);

            ResponseWithMessage(response, messageKey, expectedMessage);
        }

        #region MyRegion

        /// <summary>
        /// Asserts that the <paramref name="response"/> is not null and has a 'Status' equal to <see cref="ResponseStatus.Success"/>.
        /// Additionally, asserts that the 'Value' in the <paramref name="response"/> is not null and 'HasValue' is true.
        /// </summary>
        /// <typeparam name="TResponse">Type of the response.</typeparam>
        /// <typeparam name="TValue">Type of value.</typeparam>
        /// <typeparam name="TEx">The type of exception associated with the response.</typeparam>
        /// <param name="response">The response.</param>
        public static void SuccessGlobalValueResponse<TResponse, TValue, TEx>(TResponse response)
            where TResponse : class, IGlobalValueResponse<TValue, TEx>
            where TEx : Exception
        {
            SuccessResponse(response);

            Assert.True(response.HasValue);
            NotNull(response.Value);
        }

        /// <summary>
        /// Asserts that the <paramref name="response"/> is not null and has a 'Status' equal to <see cref="ResponseStatus.Failure"/>.
        /// Additionally, asserts that the 'Value' in the <paramref name="response"/> is null and 'HasValue' is false.
        /// </summary>
        /// <typeparam name="TResponse">Type of the response.</typeparam>
        /// <typeparam name="TValue">Type of value.</typeparam>
        /// <typeparam name="TEx">The type of exception associated with the response.</typeparam>
        /// <param name="response">The response.</param>
        public static void FailedGlobalValueResponse<TResponse, TValue, TEx>(TResponse response)
            where TResponse : class, IGlobalValueResponse<TValue, TEx>
            where TEx : Exception
        {
            FailedResponse(response);

            Assert.False(response.HasValue);
            Assert.Null(response.Value);
        }

        #endregion

        #region IAzTableValueResponse

        /// <summary>
        /// Asserts that the <paramref name="response"/> is not null and has a 'Status' equal to <see cref="ResponseStatus.Success"/>.
        /// Additionally, asserts that the 'Value' in the <paramref name="response"/> is not null and 'HasValue' is true.
        /// </summary>
        /// <typeparam name="TResponse">Type of the response.</typeparam>
        /// <typeparam name="TValue">Type of value.</typeparam>
        /// <param name="response">The response.</param>
        public static void SuccessTableValueResponse<TResponse, TValue>(TResponse response)
            where TResponse : class, IAzTableValueResponse<TValue>
        {
            SuccessResponse(response);

            Assert.True(response.HasValue);
            NotNull(response.Value);
        }

        /// <summary>
        /// Asserts that the <paramref name="response"/> is not null and has a 'Status' equal to <see cref="ResponseStatus.Failure"/>.
        /// Additionally, asserts that the 'Value' in the <paramref name="response"/> is null and 'HasValue' is false.
        /// </summary>
        /// <typeparam name="TResponse">Type of the response.</typeparam>
        /// <typeparam name="TValue">Type of value.</typeparam>
        /// <param name="response">The response.</param>
        public static void FailedTableValueResponse<TResponse, TValue>(TResponse response)
            where TResponse : class, IAzTableValueResponse<TValue>
        {
            FailedResponse(response);

            Assert.False(response.HasValue);
            Assert.Null(response.Value);
        }

        #endregion

        #region IAzBlobValueResponse

        /// <summary>
        /// Asserts that the <paramref name="response"/> is not null and has a 'Status' equal to <see cref="ResponseStatus.Success"/>.
        /// Additionally, asserts that the 'Value' in the <paramref name="response"/> is not null and 'HasValue' is true.
        /// </summary>
        /// <typeparam name="TResponse">Type of the response.</typeparam>
        /// <typeparam name="TValue">Type of value.</typeparam>
        /// <param name="response">The response.</param>
        public static void SuccessBlobValueResponse<TResponse, TValue>(TResponse response)
            where TResponse : class, IAzBlobValueResponse<TValue>
        {
            SuccessResponse(response);

            Assert.True(response.HasValue);
            NotNull(response.Value);
        }

        /// <summary>
        /// Asserts that the <paramref name="response"/> is not null and has a 'Status' equal to <see cref="ResponseStatus.Failure"/>.
        /// Additionally, asserts that the 'Value' in the <paramref name="response"/> is null and 'HasValue' is false.
        /// </summary>
        /// <typeparam name="TResponse">Type of the response.</typeparam>
        /// <typeparam name="TValue">Type of value.</typeparam>
        /// <param name="response">The response.</param>
        public static void FailedBlobValueResponse<TResponse, TValue>(TResponse response)
            where TResponse : class, IAzBlobValueResponse<TValue>
        {
            FailedResponse(response);

            Assert.False(response.HasValue);
            Assert.Null(response.Value);
        }

        #endregion
    }
}
