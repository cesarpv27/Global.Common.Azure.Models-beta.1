
namespace Global.Common.Azure.Extensions
{
    /// <summary>
    /// Provides extension methods for handling pageable responses in Azure Table Service.
    /// </summary>
    public static class PageableResponseExtensions
    {
        /// <summary>
        /// Takes a specified number of items from a pageable response synchronously.
        /// </summary>
        /// <typeparam name="T">The type of items in the pageable response.</typeparam>
        /// <param name="response">The pageable response to take items from.</param>
        /// <param name="take">The number of items to retrieve.</param>
        /// <returns>The <see cref="ExGlobalValueResponse{T}"/> associated with the result of the operation. 
        /// If the parameters are valid and <see cref="Pageable{T}"/> responds correctly to the multiple requests,
        /// the response contains the 'Status' set to <see cref="ResponseStatus.Success"/> and a <see cref="List{T}"/> with the items retrieved;
        /// otherwise, the response contains the 'Status' set to <see cref="ResponseStatus.Failure"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="response"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="take"/> is less than or equal to zero.</exception>
        internal static ExGlobalValueResponse<List<T>> TakeFromPageable<T>(
            this GlobalValueResponse<Pageable<T>, RequestFailedException> response,
            int take)
            where T : notnull
        {
            AssertParams(response, take);

            if (!TryValidateIncomingResponse(response, out ExGlobalValueResponse<List<T>>? failedResponse))
                return failedResponse!;

            var result = new List<T>(Math.Min(take, TableConstants.DefaultTake));
            var count = 0;
            foreach (var item in response.Value!)
            {
                result.Add(item);

                if (++count >= take)
                    return ExGlobalValueResponseFactory.CreateSuccessful(result);
            }

            return ExGlobalValueResponseFactory.CreateSuccessful(result);
        }

        /// <summary>
        /// Asynchronously takes a specified number of items from a pageable response.
        /// </summary>
        /// <typeparam name="T">The type of items in the pageable response.</typeparam>
        /// <param name="response">The pageable response to take items from.</param>
        /// <param name="take">The number of items to retrieve.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation. 
        /// The resulting task contains the <see cref="ExGlobalValueResponse{T}"/> associated with the result of the operation. 
        /// If the parameters are valid and <see cref="Pageable{T}"/> responds correctly to the multiple requests,
        /// the response contains the 'Status' set to <see cref="ResponseStatus.Success"/> and a <see cref="List{T}"/> with the items retrieved;
        /// otherwise, the response contains the 'Status' set to <see cref="ResponseStatus.Failure"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="response"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="take"/> is less than or equal to zero.</exception>
        internal static async Task<ExGlobalValueResponse<List<T>>> TakeFromPageableAsync<T>(
            this GlobalValueResponse<AsyncPageable<T>, RequestFailedException> response,
            int take)
            where T : notnull
        {
            AssertParams(response, take);

            if (!TryValidateIncomingResponse(response, out ExGlobalValueResponse<List<T>>? failedResponse))
                return failedResponse!;

            var result = new List<T>(Math.Min(take, TableConstants.DefaultTake));
            var count = 0;
            await foreach (var item in response.Value!)
            {
                result.Add(item);

                if (++count >= take)
                    return ExGlobalValueResponseFactory.CreateSuccessful(result);
            }

            return ExGlobalValueResponseFactory.CreateSuccessful(result);
        }

        /// <summary>
        /// Asserts that the response is not null and the number of items to take is greater than zero.
        /// </summary>
        /// <typeparam name="TValue">The type of the response value.</typeparam>
        /// <param name="response">The pageable response to validate.</param>
        /// <param name="take">The number of items to retrieve.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="response"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="take"/> is less than or equal to zero.</exception>
        private static void AssertParams<TValue>(
            GlobalValueResponse<TValue, RequestFailedException> response,
            int take)
        {
            AssertHelper.AssertNotNullOrThrow(response, nameof(response));

            if (take <= 0)
                AssertHelper.ThrowArgumentException(nameof(take), TableQueryConstants.GreaterThanZeroMessage(nameof(take)));
        }

        /// <summary>
        /// Validates the incoming response to ensure it is successful and has a value.
        /// </summary>
        /// <typeparam name="T">The type of items in the pageable response.</typeparam>
        /// <typeparam name="TResponseValue">The type of the response value.</typeparam>
        /// <param name="response">The pageable response to validate.</param>
        /// <param name="failedResponse">The response to return if validation fails.</param>
        /// <returns><c>true</c> if the response is valid; otherwise, <c>false</c>.</returns>
        private static bool TryValidateIncomingResponse<T, TResponseValue>(
            GlobalValueResponse<TResponseValue, RequestFailedException> response,
            out ExGlobalValueResponse<List<T>>? failedResponse)
        {
            if (response.Status == ResponseStatus.Failure || !response.HasValue)
            {
                if (response.HasException)
                    failedResponse = ExGlobalValueResponseFactory.CreateFailure<List<T>>(response.Exception!);
                else
                    failedResponse = ExGlobalValueResponseFactory.CreateFailure<List<T>>();

                failedResponse.AddMessagesFrom(response, KeyExistAction.Rename);

                if (!response.HasValue)
                    failedResponse.AddMessageTransactionally(
                        TableQueryConstants.NullResponseValueMessageKey,
                        TableQueryConstants.ResponseContainsNullValue());

                return false;
            }

            failedResponse = null;

            return true;
        }
    }
}
