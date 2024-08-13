
namespace Global.Common.Azure.Extensions
{
    /// <summary>
    /// Provides extension methods for querying and handling responses from Azure Table Service using <see cref="TableClient"/>.
    /// </summary>
    public static class TableClientExtensions
    {
        #region Asserts

        /// <summary>
        /// Assert that the value of the maximum number of entities that will be returned per page (maxPerPage) is between 0 and 1000.
        /// </summary>
        /// <param name="maxPerPage">The value of the maximum number of entities that will be returned per page.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <param name="message">The message to include in the exception.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="maxPerPage"/> is not between 0 and 1000.</exception>
        private static void AssertMaxPerPageOrThrow(int? maxPerPage, string paramName, string? message = null)
        {
            if (maxPerPage != null && (maxPerPage < 0 || maxPerPage > 1000))
                throw new ArgumentOutOfRangeException(paramName, message ?? TableQueryConstants.MaxPerPageOutOfRange(paramName));
        }

        #endregion

        #region Build responses

        /// <summary>
        /// Builds a failed response with a specified <see cref="RequestFailedException"/> and <see cref="TableClient"/>.
        /// </summary>
        /// <typeparam name="T">The type of the value in the response.</typeparam>
        /// <param name="ex">The <see cref="RequestFailedException"/> that caused the failure.</param>
        /// <param name="tableClient">The <see cref="TableClient"/> associated with the operation.</param>
        /// <returns>A failed <see cref="GlobalValueResponse{T, RequestFailedException}"/>.</returns>
        private static GlobalValueResponse<T, RequestFailedException> BuildFailedResponse<T>(
            RequestFailedException ex,
            TableClient tableClient)
            where T : notnull
        {
            return BuildFailedResponse<T, RequestFailedException>(ex, tableClient);
        }

        /// <summary>
        /// Builds a failed response with a specified exception and <see cref="TableClient"/>.
        /// </summary>
        /// <typeparam name="T">The type of the value in the response.</typeparam>
        /// <typeparam name="TEx">The type of the exception that caused the failure.</typeparam>
        /// <param name="ex">The exception that caused the failure.</param>
        /// <param name="tableClient">The <see cref="TableClient"/> associated with the operation.</param>
        /// <returns>A failed <see cref="GlobalValueResponse{T, TEx}"/>.</returns>
        private static GlobalValueResponse<T, TEx> BuildFailedResponse<T, TEx>(
            TEx ex, 
            TableClient tableClient)
            where TEx : Exception
            where T : notnull
        {
            var response =  GlobalValueResponseFactory.CreateFailure<T, TEx>(ex);
            FillResponseMetadata(response, tableClient.AccountName, tableClient.Name);

            return response;
        }

        /// <summary>
        /// Builds a failed response with a specified exception and <see cref="TableClient"/>.
        /// </summary>
        /// <typeparam name="T">The type of the value in the response.</typeparam>
        /// <param name="ex">The exception that caused the failure.</param>
        /// <param name="tableClient">The <see cref="TableClient"/> associated with the operation.</param>
        /// <returns>A failed <see cref="ExGlobalValueResponse{T}"/>.</returns>
        private static ExGlobalValueResponse<T> BuildFailedResponse<T>(
            Exception ex,
            TableClient tableClient)
            where T : notnull
        {
            var response = ExGlobalValueResponseFactory.CreateFailure<T>(ex);
            FillResponseMetadata(response, tableClient.AccountName, tableClient.Name);

            return response;
        }

        #endregion

        #region Fill responses

        /// <summary>
        /// Fills the response metadata with the account name and table name.
        /// </summary>
        /// <typeparam name="T">The type of the value in the response.</typeparam>
        /// <typeparam name="TEx">The type of the exception in the response.</typeparam>
        /// <param name="response">The response to fill metadata for.</param>
        /// <param name="accountName">The account name.</param>
        /// <param name="tableName">The table name.</param>
        private static void FillResponseMetadata<T, TEx>(
            IGlobalValueResponse<T, TEx> response,
            string? accountName,
            string? tableName)
            where TEx: Exception
        {
            response.AddMessageTransactionally(TableConstants.AccountName, accountName ?? TableConstants.AccountNameIsNullOrEmptyMessage);
            response.AddMessageTransactionally(TableConstants.TableName, tableName ?? TableConstants.TableNameIsNullOrEmptyMessage);
        }

        #endregion

        #region Common

        /// <summary>
        /// Takes a specified number of items from a pageable response synchronously.
        /// </summary>
        /// <typeparam name="T">The type of items in the pageable response.</typeparam>
        /// <param name="tableClient">The <see cref="TableClient"/> associated with the operation.</param>
        /// <param name="response">The pageable response to take items from.</param>
        /// <param name="take">The number of items to retrieve.</param>
        /// <returns>The <see cref="ExGlobalValueResponse{T}"/> associated with the result of the operation. 
        /// If the operation succeeded, the response contains the 'Status' set to <see cref="ResponseStatus.Success"/> and a <see cref="List{T}"/> with the items retrieved;
        /// otherwise, the response contains the 'Status' set to <see cref="ResponseStatus.Failure"/>.</returns>
        private static ExGlobalValueResponse<List<T>> TakeFromPageable<T>(
            TableClient tableClient,
            GlobalValueResponse<Pageable<T>, RequestFailedException> response,
            int take)
            where T : notnull
        {
            try
            {
                return response.TakeFromPageable(take);
            }
            catch (Exception ex)
            {
                return BuildFailedResponse<List<T>>(ex, tableClient);
            }
        }

        /// <summary>
        /// Asynchronously takes a specified number of items from a pageable response asynchronously.
        /// </summary>
        /// <typeparam name="T">The type of items in the pageable response.</typeparam>
        /// <param name="tableClient">The <see cref="TableClient"/> associated with the operation.</param>
        /// <param name="response">The pageable response to take items from.</param>
        /// <param name="take">The number of items to retrieve.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation. 
        /// The resulting task contains the <see cref="ExGlobalValueResponse{T}"/> associated with the result of the operation. 
        /// If the operation succeeded, the response contains the 'Status' set to <see cref="ResponseStatus.Success"/> and a <see cref="List{T}"/> with the items retrieved;
        /// otherwise, the response contains the 'Status' set to <see cref="ResponseStatus.Failure"/>.</returns>
        private static async Task<ExGlobalValueResponse<List<T>>> TakeFromPageableAsync<T>(
            TableClient tableClient,
            GlobalValueResponse<AsyncPageable<T>, RequestFailedException> response,
            int take)
            where T : notnull
        {
            try
            {
                return await response.TakeFromPageableAsync(take);
            }
            catch (Exception ex)
            {
                return BuildFailedResponse<List<T>>(ex, tableClient);
            }
        }

        /// <summary>
        /// Gets the <see cref="Pageable{T}"/> with the specified filter for synchronous queries.
        /// </summary>
        /// <typeparam name="T">The type of the entities in the query result.</typeparam>
        /// <param name="tableClient">The <see cref="TableClient"/> to query.</param>
        /// <param name="filter">The filter to apply to the query.</param>
        /// <param name="maxPerPage">The maximum number of entities that will be returned per page.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The <see cref="GlobalValueResponse{T, TEx}"/> associated with the result of the operation. 
        /// If the parameters are valid the response contains the 'Status' set to <see cref="ResponseStatus.Success"/>
        /// and a <see cref="Pageable{T}"/> for querying the items;
        /// otherwise, the response contains the 'Status' set to <see cref="ResponseStatus.Failure"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="tableClient"/> or <paramref name="filter"/> are null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="maxPerPage"/> is not between 0 and 1000.</exception>
        private static GlobalValueResponse<Pageable<T>, RequestFailedException> Query<T>(
            TableClient tableClient,
            string filter,
            int? maxPerPage = TableQueryConstants.DefaultMaxPerPage,
            CancellationToken cancellationToken = default) where T : class, ITableEntity, new()
        {
            AssertHelper.AssertNotNullOrThrow(tableClient, nameof(tableClient));
            AssertHelper.AssertNotNullOrThrow(filter, nameof(filter));
            AssertMaxPerPageOrThrow(maxPerPage, nameof(maxPerPage));

            try
            {
                return GlobalValueResponseFactory.CreateSuccessful<Pageable<T>, RequestFailedException>(
                    tableClient.Query<T>(
                        filter,
                        maxPerPage,
                        cancellationToken: cancellationToken));
            }
            catch (RequestFailedException ex)
            {
                return BuildFailedResponse<Pageable<T>>(ex, tableClient);
            }
        }

        /// <summary>
        /// Gets the <see cref="AsyncPageable{T}"/> with the specified filter for asynchronous queries.
        /// </summary>
        /// <typeparam name="T">The type of the entities in the query result.</typeparam>
        /// <param name="tableClient">The <see cref="TableClient"/> to query.</param>
        /// <param name="filter">The filter to apply to the query.</param>
        /// <param name="maxPerPage">The maximum number of entities that will be returned per page.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The <see cref="GlobalValueResponse{T, TEx}"/> associated with the result of the operation. 
        /// If the parameters are valid the response contains the 'Status' set to <see cref="ResponseStatus.Success"/>
        /// and a <see cref="Pageable{T}"/> for querying the items;
        /// otherwise, the response contains the 'Status' set to <see cref="ResponseStatus.Failure"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="tableClient"/> or <paramref name="filter"/> are null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="maxPerPage"/> is not between 0 and 1000.</exception>
        private static GlobalValueResponse<AsyncPageable<T>, RequestFailedException> QueryAsync<T>(
            TableClient tableClient,
            string filter,
            int? maxPerPage = TableQueryConstants.DefaultMaxPerPage,
            CancellationToken cancellationToken = default) where T : class, ITableEntity, new()
        {
            AssertHelper.AssertNotNullOrThrow(tableClient, nameof(tableClient));
            AssertHelper.AssertNotNullOrThrow(filter, nameof(filter));
            AssertMaxPerPageOrThrow(maxPerPage, nameof(maxPerPage));

            try
            {
                return GlobalValueResponseFactory.CreateSuccessful<AsyncPageable<T>, RequestFailedException>(
                    tableClient.QueryAsync<T>(
                        filter,
                        maxPerPage,
                        cancellationToken: cancellationToken));
            }
            catch (RequestFailedException ex)
            {
                return BuildFailedResponse<AsyncPageable<T>>(ex, tableClient);
            }
        }

        #endregion

        #region PartitionKey

        // Sync

        /// <summary>
        /// Gets the <see cref="Pageable{T}"/> for synchronous queries using the specified <paramref name="partitionKey"/> within the filter.
        /// </summary>
        /// <typeparam name="T">The type of the entities in the query result.</typeparam>
        /// <param name="tableClient">The <see cref="TableClient"/> to query.</param>
        /// <param name="partitionKey">The partition key to filter by.</param>
        /// <param name="maxPerPage">The maximum number of entities that will be returned per page.</param>
        /// <param name="cancellationToken">A token to observe for cancellation requests.</param>
        /// <returns>The <see cref="GlobalValueResponse{T, TEx}"/> associated with the result of the operation. 
        /// If the parameters are valid the response contains the 'Status' set to <see cref="ResponseStatus.Success"/>
        /// and a <see cref="Pageable{T}"/> for querying the items;
        /// otherwise, the response contains the 'Status' set to <see cref="ResponseStatus.Failure"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="tableClient"/> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="maxPerPage"/> is not between 0 and 1000.</exception>
        public static GlobalValueResponse<Pageable<T>, RequestFailedException> PageableQueryByPartitionKey<T>(
            this TableClient tableClient,
            string partitionKey,
            int? maxPerPage = TableQueryConstants.DefaultMaxPerPage,
            CancellationToken cancellationToken = default) where T : class, ITableEntity, new()
        {
            return Query<T>(
                tableClient,
                TableQueryFilterBuilder.BuildPartitionKeyFilter(QueryComparison.eq, partitionKey).ToString(),
                maxPerPage,
                cancellationToken);
        }

        /// <summary>
        /// Queries the Azure Table Service using the specified <paramref name="partitionKey"/> within the filter,
        /// and retrieves up to the specified number of items that match the filter defined by <paramref name="take"/>.
        /// </summary>
        /// <typeparam name="T">The type of the entities in the query result.</typeparam>
        /// <param name="tableClient">The <see cref="TableClient"/> to query.</param>
        /// <param name="partitionKey">The partition key to filter by.</param>
        /// <param name="take">The number of items to retrieve.</param>
        /// <param name="cancellationToken">A token to observe for cancellation requests.</param>
        /// <returns>The <see cref="ExGlobalValueResponse{T}"/> associated with the result of the operation. 
        /// If the operation succeeded, the response contains the 'Status' set to <see cref="ResponseStatus.Success"/> and a <see cref="List{T}"/> with the items retrieved;
        /// otherwise, the response contains the 'Status' set to <see cref="ResponseStatus.Failure"/>.</returns>
        public static ExGlobalValueResponse<List<T>> QueryByPartitionKey<T>(
            this TableClient tableClient,
            string partitionKey,
            int take = TableConstants.DefaultTake,
            CancellationToken cancellationToken = default)
            where T : class, ITableEntity, new()
        {
            return TakeFromPageable(
                tableClient,
                PageableQueryByPartitionKey<T>(
                    tableClient,
                    partitionKey,
                    TableQueryConstants.DefaultMaxPerPage,
                    cancellationToken),
                take);
        }

        // Async

        /// <summary>
        /// Gets the <see cref="AsyncPageable{T}"/> for asynchronous queries using the specified <paramref name="partitionKey"/> within the filter.
        /// </summary>
        /// <typeparam name="T">The type of the entities in the query result.</typeparam>
        /// <param name="tableClient">The <see cref="TableClient"/> to query.</param>
        /// <param name="partitionKey">The partition key to filter by.</param>
        /// <param name="maxPerPage">The maximum number of entities that will be returned per page.</param>
        /// <param name="cancellationToken">A token to observe for cancellation requests.</param>
        /// <returns>The <see cref="GlobalValueResponse{T, TEx}"/> associated with the result of the operation. 
        /// If the parameters are valid the response contains the 'Status' set to <see cref="ResponseStatus.Success"/>
        /// and a <see cref="AsyncPageable{T}"/> for querying the items;
        /// otherwise, the response contains the 'Status' set to <see cref="ResponseStatus.Failure"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="tableClient"/> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="maxPerPage"/> is not between 0 and 1000.</exception>
        public static GlobalValueResponse<AsyncPageable<T>, RequestFailedException> AsyncPageableQueryByPartitionKey<T>(
            this TableClient tableClient,
            string partitionKey,
            int? maxPerPage = TableQueryConstants.DefaultMaxPerPage,
            CancellationToken cancellationToken = default)
            where T : class, ITableEntity, new()
        {
            return QueryAsync<T>(
                tableClient,
                TableQueryFilterBuilder.BuildPartitionKeyFilter(QueryComparison.eq, partitionKey).ToString(),
                maxPerPage,
                cancellationToken);
        }

        /// <summary>
        /// Asynchronously queries the Azure Table Service using the specified <paramref name="partitionKey"/> within the filter,
        /// and retrieves up to the specified number of items that match the filter defined by <paramref name="take"/>.
        /// </summary>
        /// <typeparam name="T">The type of the entities in the query result.</typeparam>
        /// <param name="tableClient">The <see cref="TableClient"/> to query.</param>
        /// <param name="partitionKey">The partition key to filter by.</param>
        /// <param name="take">The number of items to retrieve.</param>
        /// <param name="cancellationToken">A token to observe for cancellation requests.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation. 
        /// The resulting task contains the <see cref="ExGlobalValueResponse{T}"/> associated with the result of the operation. 
        /// If the operation succeeded, the response contains the 'Status' set to <see cref="ResponseStatus.Success"/> and a <see cref="List{T}"/> with the items retrieved;
        /// otherwise, the response contains the 'Status' set to <see cref="ResponseStatus.Failure"/>.</returns>
        public static async Task<ExGlobalValueResponse<List<T>>> QueryByPartitionKeyAsync<T>(
            this TableClient tableClient,
            string partitionKey,
            int take = TableConstants.DefaultTake,
            CancellationToken cancellationToken = default)
            where T : class, ITableEntity, new()
        {
            return await TakeFromPageableAsync(
                tableClient,
                AsyncPageableQueryByPartitionKey<T>(
                    tableClient,
                    partitionKey,
                    TableQueryConstants.DefaultMaxPerPage,
                    cancellationToken),
                take);
        }

        #endregion

        #region PartitionKeyStartPattern

        // Sync

        /// <summary>
        /// Gets the <see cref="Pageable{T}"/> for synchronous queries using the specified <paramref name="startPattern"/> within the filter.
        /// </summary>
        /// <typeparam name="T">The type of the entities in the query result.</typeparam>
        /// <param name="tableClient">The <see cref="TableClient"/> to query.</param>
        /// <param name="startPattern">The partition key start pattern to filter by.</param>
        /// <param name="maxPerPage">The maximum number of items per page.</param>
        /// <param name="cancellationToken">A token to observe for cancellation requests.</param>
        /// <returns>The <see cref="GlobalValueResponse{T, TEx}"/> associated with the result of the operation. 
        /// If the parameters are valid the response contains the 'Status' set to <see cref="ResponseStatus.Success"/>
        /// and a <see cref="Pageable{T}"/> for querying the items;
        /// otherwise, the response contains the 'Status' set to <see cref="ResponseStatus.Failure"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="tableClient"/> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="maxPerPage"/> is not between 0 and 1000.</exception>
        public static GlobalValueResponse<Pageable<T>, RequestFailedException> PageableQueryByPartitionKeyStartPattern<T>(
            this TableClient tableClient,
            string startPattern,
            int? maxPerPage = TableQueryConstants.DefaultMaxPerPage,
            CancellationToken cancellationToken = default)
            where T : class, ITableEntity, new()
        {
            return Query<T>(
                tableClient,
                TableQueryFilterBuilder.BuildPartitionKeyFilter(QueryComparison.ge, startPattern)
                .And(TableQueryFilterBuilder.BuildPartitionKeyFilter(QueryComparison.lt, startPattern.AddLastChar())).ToString(),
                maxPerPage,
                cancellationToken);
        }

        /// <summary>
        /// Queries the Azure Table Service using the specified <paramref name="startPattern"/> within the filter,
        /// and retrieves up to the specified number of items that match the filter defined by <paramref name="take"/>.
        /// </summary>
        /// <typeparam name="T">The type of the entities in the query result.</typeparam>
        /// <param name="tableClient">The <see cref="TableClient"/> to query.</param>
        /// <param name="startPattern">The partition key start pattern to filter by.</param>
        /// <param name="take">The number of items to retrieve.</param>
        /// <param name="cancellationToken">A token to observe for cancellation requests.</param>
        /// <returns>The <see cref="ExGlobalValueResponse{T}"/> associated with the result of the operation. 
        /// If the operation succeeded, the response contains the 'Status' set to <see cref="ResponseStatus.Success"/> and a <see cref="List{T}"/> with the items retrieved;
        /// otherwise, the response contains the 'Status' set to <see cref="ResponseStatus.Failure"/>.</returns>
        public static ExGlobalValueResponse<List<T>> QueryByPartitionKeyStartPattern<T>(
            this TableClient tableClient,
            string startPattern,
            int take = TableConstants.DefaultTake,
            CancellationToken cancellationToken = default)
            where T : class, ITableEntity, new()
        {
            return TakeFromPageable(
                tableClient,
                PageableQueryByPartitionKeyStartPattern<T>(
                    tableClient,
                    startPattern,
                    TableQueryConstants.DefaultMaxPerPage,
                    cancellationToken),
                take);
        }

        // Async

        /// <summary>
        /// Gets the <see cref="AsyncPageable{T}"/> for asynchronous queries using the specified <paramref name="startPattern"/> within the filter.
        /// </summary>
        /// <typeparam name="T">The type of the entities in the query result.</typeparam>
        /// <param name="tableClient">The <see cref="TableClient"/> to query.</param>
        /// <param name="startPattern">The partition key start pattern to filter by.</param>
        /// <param name="maxPerPage">The maximum number of items per page.</param>
        /// <param name="cancellationToken">A token to observe for cancellation requests.</param>
        /// <returns>The <see cref="GlobalValueResponse{T, TEx}"/> associated with the result of the operation. 
        /// If the parameters are valid the response contains the 'Status' set to <see cref="ResponseStatus.Success"/>
        /// and a <see cref="AsyncPageable{T}"/> for querying the items;
        /// otherwise, the response contains the 'Status' set to <see cref="ResponseStatus.Failure"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="tableClient"/> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="maxPerPage"/> is not between 0 and 1000.</exception>
        public static GlobalValueResponse<AsyncPageable<T>, RequestFailedException> AsyncPageableQueryByPartitionKeyStartPattern<T>(
            this TableClient tableClient,
            string startPattern,
            int? maxPerPage = TableQueryConstants.DefaultMaxPerPage,
            CancellationToken cancellationToken = default)
            where T : class, ITableEntity, new()
        {
            return QueryAsync<T>(
                tableClient,
                TableQueryFilterBuilder.BuildPartitionKeyFilter(QueryComparison.ge, startPattern)
                .And(TableQueryFilterBuilder.BuildPartitionKeyFilter(QueryComparison.lt, startPattern.AddLastChar())).ToString(),
                maxPerPage,
                cancellationToken);
        }

        /// <summary>
        /// Asynchronously queries the Azure Table Service using the specified <paramref name="startPattern"/> within the filter,
        /// and retrieves up to the specified number of items that match the filter defined by <paramref name="take"/>.
        /// </summary>
        /// <typeparam name="T">The type of the entities in the query result.</typeparam>
        /// <param name="tableClient">The <see cref="TableClient"/> to query.</param>
        /// <param name="startPattern">The partition key start pattern to filter by.</param>
        /// <param name="take">The number of items to retrieve.</param>
        /// <param name="cancellationToken">A token to observe for cancellation requests.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation. 
        /// The resulting task contains the <see cref="ExGlobalValueResponse{T}"/> associated with the result of the operation. 
        /// If the operation succeeded, the response contains the 'Status' set to <see cref="ResponseStatus.Success"/> and a <see cref="List{T}"/> with the items retrieved;
        /// otherwise, the response contains the 'Status' set to <see cref="ResponseStatus.Failure"/>.</returns>
        public static async Task<ExGlobalValueResponse<List<T>>> QueryByPartitionKeyStartPatternAsync<T>(
            this TableClient tableClient,
            string startPattern,
            int take = TableConstants.DefaultTake,
            CancellationToken cancellationToken = default)
            where T : class, ITableEntity, new()
        {
            return await TakeFromPageableAsync(
                tableClient,
                AsyncPageableQueryByPartitionKeyStartPattern<T>(
                    tableClient,
                    startPattern,
                    TableQueryConstants.DefaultMaxPerPage,
                    cancellationToken),
                take);
        }

        #endregion

        #region PartitionKeyRowKey

        // Sync

        /// <summary>
        /// Gets the <see cref="Pageable{T}"/> for synchronous queries using the specified <paramref name="partitionKey"/> and <paramref name="rowKey"/> within the filter.
        /// </summary>
        /// <typeparam name="T">The type of the entities in the query result.</typeparam>
        /// <param name="tableClient">The <see cref="TableClient"/> to query.</param>
        /// <param name="partitionKey">The partition key to filter by.</param>
        /// <param name="rowKey">The row key to filter by.</param>
        /// <param name="cancellationToken">A token to observe for cancellation requests.</param>
        /// <returns>The <see cref="GlobalValueResponse{T, TEx}"/> associated with the result of the operation. 
        /// If the parameters are valid the response contains the 'Status' set to <see cref="ResponseStatus.Success"/>
        /// and a <see cref="Pageable{T}"/> for querying the items;
        /// otherwise, the response contains the 'Status' set to <see cref="ResponseStatus.Failure"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="tableClient"/> is null.</exception>
        public static GlobalValueResponse<Pageable<T>, RequestFailedException> PageableQueryByPartitionKeyRowKey<T>(
            this TableClient tableClient,
            string partitionKey,
            string rowKey,
            CancellationToken cancellationToken = default)
            where T : class, ITableEntity, new()
        {
            return Query<T>(
                tableClient,
                TableQueryFilterBuilder.BuildPartitionKeyFilter(QueryComparison.eq, partitionKey)
                .And(TableQueryFilterBuilder.BuildRowKeyFilter(QueryComparison.eq, rowKey)).ToString(),
                1,
                cancellationToken);
        }

        /// <summary>
        /// Queries the Azure Table Service using the specified <paramref name="partitionKey"/> and <paramref name="rowKey"/> within the filter,
        /// and retrieves the item that match the filter.
        /// </summary>
        /// <typeparam name="T">The type of the entities in the query result.</typeparam>
        /// <param name="tableClient">The <see cref="TableClient"/> to query.</param>
        /// <param name="partitionKey">The partition key to filter by.</param>
        /// <param name="rowKey">The row key to filter by.</param>
        /// <param name="cancellationToken">A token to observe for cancellation requests.</param>
        /// <returns>The <see cref="ExGlobalValueResponse{T}"/> associated with the result of the operation. 
        /// If the operation succeeded, the response contains the 'Status' set to <see cref="ResponseStatus.Success"/> 
        /// and the item retrieved if a matching item was found, or the 'Status' set to <see cref="ResponseStatus.Warning"/> 
        /// if no items matched the filter.
        /// Otherwise, the response contains the 'Status' set to <see cref="ResponseStatus.Failure"/>.</returns>
        public static ExGlobalValueResponse<T?> QueryByPartitionKeyRowKey<T>(
            this TableClient tableClient,
            string partitionKey,
            string rowKey,
            CancellationToken cancellationToken = default)
            where T : class, ITableEntity, new()
        {
            var takeFromPageableResponse = TakeFromPageable(
                tableClient,
                PageableQueryByPartitionKeyRowKey<T>(
                    tableClient,
                    partitionKey,
                    rowKey,
                    cancellationToken),
               1);

            if (takeFromPageableResponse.Status == ResponseStatus.Failure)
                return ExGlobalValueResponseFactory.MapFromFailure<T?>(takeFromPageableResponse);

            if (takeFromPageableResponse.Value!.Count == 0)
            {
                var response = ExGlobalValueResponseFactory.CreateWarning<T?>(null);
                response.AddMessageTransactionally(
                    TableQueryConstants.EntityNotFoundMessageKey, 
                    TableQueryConstants.EntityNotFound(partitionKey, rowKey));

                return response;
            }

            return ExGlobalValueResponseFactory.CreateSuccessful<T?>(takeFromPageableResponse.Value.First());
        }

        // Async

        /// <summary>
        /// Gets the <see cref="AsyncPageable{T}"/> for asynchronous queries using the specified <paramref name="partitionKey"/> and <paramref name="rowKey"/> within the filter.
        /// </summary>
        /// <typeparam name="T">The type of the entities in the query result.</typeparam>
        /// <param name="tableClient">The <see cref="TableClient"/> to query.</param>
        /// <param name="partitionKey">The partition key to filter by.</param>
        /// <param name="rowKey">The row key to filter by.</param>
        /// <param name="cancellationToken">A token to observe for cancellation requests.</param>
        /// <returns>The <see cref="GlobalValueResponse{T, TEx}"/> associated with the result of the operation. 
        /// If the parameters are valid the response contains the 'Status' set to <see cref="ResponseStatus.Success"/>
        /// and a <see cref="AsyncPageable{T}"/> for querying the items;
        /// otherwise, the response contains the 'Status' set to <see cref="ResponseStatus.Failure"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="tableClient"/> is null.</exception>
        public static GlobalValueResponse<AsyncPageable<T>, RequestFailedException> AsyncPageableQueryByPartitionKeyRowKey<T>(
            this TableClient tableClient,
            string partitionKey,
            string rowKey,
            CancellationToken cancellationToken = default)
            where T : class, ITableEntity, new()
        {
            return QueryAsync<T>(
                tableClient,
                TableQueryFilterBuilder.BuildPartitionKeyFilter(QueryComparison.eq, partitionKey)
                .And(TableQueryFilterBuilder.BuildRowKeyFilter(QueryComparison.eq, rowKey)).ToString(),
                TableQueryConstants.DefaultMaxPerPage,
                cancellationToken);
        }

        /// <summary>
        /// Asynchronously queries the Azure Table Service using the specified <paramref name="partitionKey"/> and <paramref name="rowKey"/> within the filter,
        /// and retrieves the item that match the filter.
        /// </summary>
        /// <typeparam name="T">The type of the entities in the query result.</typeparam>
        /// <param name="tableClient">The <see cref="TableClient"/> to query.</param>
        /// <param name="partitionKey">The partition key to filter by.</param>
        /// <param name="rowKey">The row key to filter by.</param>
        /// <param name="cancellationToken">A token to observe for cancellation requests.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation. 
        /// The resulting task contains the <see cref="ExGlobalValueResponse{T}"/> associated with the result of the operation. 
        /// If the operation succeeded, the response contains the 'Status' set to <see cref="ResponseStatus.Success"/> 
        /// and the item retrieved if a matching item was found, or the 'Status' set to <see cref="ResponseStatus.Warning"/> 
        /// if no items matched the filter.
        /// Otherwise, the response contains the 'Status' set to <see cref="ResponseStatus.Failure"/>.</returns>
        public static async Task<ExGlobalValueResponse<T?>> QueryByPartitionKeyRowKeyAsync<T>(
            this TableClient tableClient,
            string partitionKey,
            string rowKey,
            CancellationToken cancellationToken = default)
            where T : class, ITableEntity, new()
        {
            var takeFromPageableResponse = await TakeFromPageableAsync(
                tableClient,
                AsyncPageableQueryByPartitionKeyRowKey<T>(
                    tableClient,
                    partitionKey,
                    rowKey,
                    cancellationToken),
                1);

            if (takeFromPageableResponse.Status == ResponseStatus.Failure)
                return ExGlobalValueResponseFactory.MapFromFailure<T?>(takeFromPageableResponse);

            if (takeFromPageableResponse.Value!.Count == 0)
            {
                var response = ExGlobalValueResponseFactory.CreateWarning<T?>(null);
                response.AddMessageTransactionally(
                    TableQueryConstants.EntityNotFoundMessageKey,
                    TableQueryConstants.EntityNotFound(partitionKey, rowKey));

                return response;
            }

            return ExGlobalValueResponseFactory.CreateSuccessful<T?>(takeFromPageableResponse.Value.First());
        }

        #endregion

        #region PartitionKeyRowKeyStartPattern

        // Sync

        /// <summary>
        /// Gets the <see cref="Pageable{T}"/> for synchronous queries using the specified <paramref name="partitionKey"/> and <paramref name="rowKeyStartPattern"/> within the filter.
        /// </summary>
        /// <typeparam name="T">The type of the entities in the query result.</typeparam>
        /// <param name="tableClient">The <see cref="TableClient"/> to query.</param>
        /// <param name="partitionKey">The partition key to filter by.</param>
        /// <param name="rowKeyStartPattern">The row key start pattern to filter by.</param>
        /// <param name="maxPerPage">The maximum number of items per page.</param>
        /// <param name="cancellationToken">A token to observe for cancellation requests.</param>
        /// <returns>The <see cref="GlobalValueResponse{T, TEx}"/> associated with the result of the operation. 
        /// If the parameters are valid the response contains the 'Status' set to <see cref="ResponseStatus.Success"/>
        /// and a <see cref="Pageable{T}"/> for querying the items;
        /// otherwise, the response contains the 'Status' set to <see cref="ResponseStatus.Failure"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="tableClient"/> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="maxPerPage"/> is not between 0 and 1000.</exception>
        public static GlobalValueResponse<Pageable<T>, RequestFailedException> PageableQueryByPartitionKeyRowKeyStartPattern<T>(
            this TableClient tableClient,
            string partitionKey,
            string rowKeyStartPattern,
            int? maxPerPage = TableQueryConstants.DefaultMaxPerPage,
            CancellationToken cancellationToken = default)
            where T : class, ITableEntity, new()
        {
            return Query<T>(
                tableClient,
                TableQueryFilterBuilder.BuildPartitionKeyFilter(QueryComparison.eq, partitionKey)
                .And(
                    TableQueryFilterBuilder.BuildRowKeyFilter(QueryComparison.ge, rowKeyStartPattern)
                    .And(TableQueryFilterBuilder.BuildRowKeyFilter(QueryComparison.lt, rowKeyStartPattern.AddLastChar())))
                .ToString(),
                maxPerPage,
                cancellationToken);
        }

        /// <summary>
        /// Queries the Azure Table Service using the specified <paramref name="partitionKey"/> and <paramref name="rowKeyStartPattern"/> within the filter,
        /// and retrieves up to the specified number of items that match the filter defined by <paramref name="take"/>.
        /// </summary>
        /// <typeparam name="T">The type of the entities in the query result.</typeparam>
        /// <param name="tableClient">The <see cref="TableClient"/> to query.</param>
        /// <param name="partitionKey">The partition key to filter by.</param>
        /// <param name="rowKeyStartPattern">The row key start pattern to filter by.</param>
        /// <param name="take">The number of items to retrieve.</param>
        /// <param name="cancellationToken">A token to observe for cancellation requests.</param>
        /// <returns>The <see cref="ExGlobalValueResponse{T}"/> associated with the result of the operation. 
        /// If the operation succeeded, the response contains the 'Status' set to <see cref="ResponseStatus.Success"/> and a <see cref="List{T}"/> with the items retrieved;
        /// otherwise, the response contains the 'Status' set to <see cref="ResponseStatus.Failure"/>.</returns>
        public static ExGlobalValueResponse<List<T>> QueryByPartitionKeyRowKeyStartPattern<T>(
            this TableClient tableClient,
            string partitionKey,
            string rowKeyStartPattern,
            int take = TableConstants.DefaultTake,
            CancellationToken cancellationToken = default)
            where T : class, ITableEntity, new()
        {
            return TakeFromPageable(
                tableClient,
                PageableQueryByPartitionKeyRowKeyStartPattern<T>(
                    tableClient,
                    partitionKey,
                    rowKeyStartPattern,
                    TableQueryConstants.DefaultMaxPerPage,
                    cancellationToken),
                take);
        }

        // Async

        /// <summary>
        /// Gets the <see cref="AsyncPageable{T}"/> for asynchronous queries using the specified <paramref name="partitionKey"/> and <paramref name="rowKeyStartPattern"/> within the filter.
        /// </summary>
        /// <typeparam name="T">The type of the entities in the query result.</typeparam>
        /// <param name="tableClient">The <see cref="TableClient"/> to query.</param>
        /// <param name="partitionKey">The partition key to filter by.</param>
        /// <param name="rowKeyStartPattern">The row key start pattern to filter by.</param>
        /// <param name="maxPerPage">The maximum number of items per page.</param>
        /// <param name="cancellationToken">A token to observe for cancellation requests.</param>
        /// <returns>The <see cref="GlobalValueResponse{T, TEx}"/> associated with the result of the operation. 
        /// If the parameters are valid the response contains the 'Status' set to <see cref="ResponseStatus.Success"/>
        /// and a <see cref="AsyncPageable{T}"/> for querying the items;
        /// otherwise, the response contains the 'Status' set to <see cref="ResponseStatus.Failure"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="tableClient"/> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="maxPerPage"/> is not between 0 and 1000.</exception>
        public static GlobalValueResponse<AsyncPageable<T>, RequestFailedException> AsyncPageableQueryByPartitionKeyRowKeyStartPattern<T>(
            this TableClient tableClient,
            string partitionKey,
            string rowKeyStartPattern,
            int? maxPerPage = TableQueryConstants.DefaultMaxPerPage,
            CancellationToken cancellationToken = default)
            where T : class, ITableEntity, new()
        {
            return QueryAsync<T>(
                tableClient,
                TableQueryFilterBuilder.BuildPartitionKeyFilter(QueryComparison.eq, partitionKey)
                .And(
                    TableQueryFilterBuilder.BuildRowKeyFilter(QueryComparison.ge, rowKeyStartPattern)
                    .And(TableQueryFilterBuilder.BuildRowKeyFilter(QueryComparison.lt, rowKeyStartPattern.AddLastChar())))
                .ToString(),
                maxPerPage,
                cancellationToken);
        }

        /// <summary>
        /// Asynchronously queries the Azure Table Service using the specified <paramref name="partitionKey"/> and <paramref name="rowKeyStartPattern"/> within the filter,
        /// and retrieves up to the specified number of items that match the filter defined by <paramref name="take"/>.
        /// </summary>
        /// <typeparam name="T">The type of the entities in the query result.</typeparam>
        /// <param name="tableClient">The <see cref="TableClient"/> to query.</param>
        /// <param name="partitionKey">The partition key to filter by.</param>
        /// <param name="rowKeyStartPattern">The row key start pattern to filter by.</param>
        /// <param name="take">The number of items to retrieve.</param>
        /// <param name="cancellationToken">A token to observe for cancellation requests.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation. 
        /// The resulting task contains the <see cref="ExGlobalValueResponse{T}"/> associated with the result of the operation. 
        /// If the operation succeeded, the response contains the 'Status' set to <see cref="ResponseStatus.Success"/> and a <see cref="List{T}"/> with the items retrieved;
        /// otherwise, the response contains the 'Status' set to <see cref="ResponseStatus.Failure"/>.</returns>
        public static async Task<ExGlobalValueResponse<List<T>>> QueryByPartitionKeyRowKeyStartPatternAsync<T>(
            this TableClient tableClient,
            string partitionKey,
            string rowKeyStartPattern,
            int take = TableConstants.DefaultTake,
            CancellationToken cancellationToken = default)
            where T : class, ITableEntity, new()
        {
            return await TakeFromPageableAsync(
                tableClient,
                AsyncPageableQueryByPartitionKeyRowKeyStartPattern<T>(
                    tableClient,
                    partitionKey,
                    rowKeyStartPattern,
                    TableQueryConstants.DefaultMaxPerPage,
                    cancellationToken),
                take);
        }

        #endregion

        #region PartitionKeyStartPatternRowKeyStartPattern

        // Sync

        /// <summary>
        /// Gets the <see cref="Pageable{T}"/> for synchronous queries using the specified <paramref name="partitionKeyStartPattern"/> and <paramref name="rowKeyStartPattern"/> within the filter.
        /// </summary>
        /// <typeparam name="T">The type of the entities in the query result.</typeparam>
        /// <param name="tableClient">The <see cref="TableClient"/> to query.</param>
        /// <param name="partitionKeyStartPattern">The partition key start pattern to filter by.</param>
        /// <param name="rowKeyStartPattern">The row key start pattern to filter by.</param>
        /// <param name="maxPerPage">The maximum number of items per page.</param>
        /// <param name="cancellationToken">A token to observe for cancellation requests.</param>
        /// <returns>The <see cref="GlobalValueResponse{T, TEx}"/> associated with the result of the operation. 
        /// If the parameters are valid the response contains the 'Status' set to <see cref="ResponseStatus.Success"/>
        /// and a <see cref="Pageable{T}"/> for querying the items;
        /// otherwise, the response contains the 'Status' set to <see cref="ResponseStatus.Failure"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="tableClient"/> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="maxPerPage"/> is not between 0 and 1000.</exception>
        public static GlobalValueResponse<Pageable<T>, RequestFailedException> PageableQueryByPartitionKeyStartPatternRowKeyStartPattern<T>(
            this TableClient tableClient,
            string partitionKeyStartPattern,
            string rowKeyStartPattern,
            int? maxPerPage = TableQueryConstants.DefaultMaxPerPage,
            CancellationToken cancellationToken = default)
            where T : class, ITableEntity, new()
        {
            return Query<T>(
                tableClient,
                TableQueryFilterBuilder.BuildPartitionKeyFilter(QueryComparison.ge, partitionKeyStartPattern)
                .And(TableQueryFilterBuilder.BuildPartitionKeyFilter(QueryComparison.lt, partitionKeyStartPattern.AddLastChar()))
                .And(
                    TableQueryFilterBuilder.BuildRowKeyFilter(QueryComparison.ge, rowKeyStartPattern)
                    .And(TableQueryFilterBuilder.BuildRowKeyFilter(QueryComparison.lt, rowKeyStartPattern.AddLastChar()))).ToString(),
                maxPerPage,
                cancellationToken);
        }

        /// <summary>
        /// Queries the Azure Table Service using the specified <paramref name="partitionKeyStartPattern"/> and <paramref name="rowKeyStartPattern"/> within the filter,
        /// and retrieves up to the specified number of items that match the filter defined by <paramref name="take"/>.
        /// </summary>
        /// <typeparam name="T">The type of the entities in the query result.</typeparam>
        /// <param name="tableClient">The <see cref="TableClient"/> to query.</param>
        /// <param name="partitionKeyStartPattern">The partition key start pattern to filter by.</param>
        /// <param name="rowKeyStartPattern">The row key start pattern to filter by.</param>
        /// <param name="take">The number of items to retrieve.</param>
        /// <param name="cancellationToken">A token to observe for cancellation requests.</param>
        /// <returns>The <see cref="ExGlobalValueResponse{T}"/> associated with the result of the operation. 
        /// If the operation succeeded, the response contains the 'Status' set to <see cref="ResponseStatus.Success"/> and a <see cref="List{T}"/> with the items retrieved;
        /// otherwise, the response contains the 'Status' set to <see cref="ResponseStatus.Failure"/>.</returns>
        public static ExGlobalValueResponse<List<T>> QueryByPartitionKeyStartPatternRowKeyStartPattern<T>(
            this TableClient tableClient,
            string partitionKeyStartPattern,
            string rowKeyStartPattern,
            int take = TableConstants.DefaultTake,
            CancellationToken cancellationToken = default)
            where T : class, ITableEntity, new()
        {
            return TakeFromPageable(
                tableClient,
                PageableQueryByPartitionKeyStartPatternRowKeyStartPattern<T>(
                    tableClient,
                    partitionKeyStartPattern,
                    rowKeyStartPattern,
                    TableQueryConstants.DefaultMaxPerPage,
                    cancellationToken),
                take);
        }

        // Async

        /// <summary>
        /// Gets the <see cref="AsyncPageable{T}"/> for asynchronous queries using the specified <paramref name="partitionKeyStartPattern"/> and <paramref name="rowKeyStartPattern"/> within the filter.
        /// </summary>
        /// <typeparam name="T">The type of the entities in the query result.</typeparam>
        /// <param name="tableClient">The <see cref="TableClient"/> to query.</param>
        /// <param name="partitionKeyStartPattern">The partition key start pattern to filter by.</param>
        /// <param name="rowKeyStartPattern">The row key start pattern to filter by.</param>
        /// <param name="maxPerPage">The maximum number of items per page.</param>
        /// <param name="cancellationToken">A token to observe for cancellation requests.</param>
        /// <returns>The <see cref="GlobalValueResponse{T, TEx}"/> associated with the result of the operation. 
        /// If the parameters are valid the response contains the 'Status' set to <see cref="ResponseStatus.Success"/>
        /// and a <see cref="AsyncPageable{T}"/> for querying the items;
        /// otherwise, the response contains the 'Status' set to <see cref="ResponseStatus.Failure"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="tableClient"/> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="maxPerPage"/> is not between 0 and 1000.</exception>
        public static GlobalValueResponse<AsyncPageable<T>, RequestFailedException> AsyncPageableQueryByPartitionKeyStartPatternRowKeyStartPattern<T>(
            this TableClient tableClient,
            string partitionKeyStartPattern,
            string rowKeyStartPattern,
            int? maxPerPage = TableQueryConstants.DefaultMaxPerPage,
            CancellationToken cancellationToken = default)
            where T : class, ITableEntity, new()
        {
            return QueryAsync<T>(
                tableClient,
                TableQueryFilterBuilder.BuildPartitionKeyFilter(QueryComparison.ge, partitionKeyStartPattern)
                .And(TableQueryFilterBuilder.BuildPartitionKeyFilter(QueryComparison.lt, partitionKeyStartPattern.AddLastChar()))
                .And(
                    TableQueryFilterBuilder.BuildRowKeyFilter(QueryComparison.ge, rowKeyStartPattern)
                    .And(TableQueryFilterBuilder.BuildRowKeyFilter(QueryComparison.lt, rowKeyStartPattern.AddLastChar()))).ToString(),
                maxPerPage,
                cancellationToken);
        }

        /// <summary>
        /// Asynchronously queries the Azure Table Service using the specified <paramref name="partitionKeyStartPattern"/> and <paramref name="rowKeyStartPattern"/> within the filter,
        /// and retrieves up to the specified number of items that match the filter defined by <paramref name="take"/>.
        /// </summary>
        /// <typeparam name="T">The type of the entities in the query result.</typeparam>
        /// <param name="tableClient">The <see cref="TableClient"/> to query.</param>
        /// <param name="partitionKeyStartPattern">The partition key start pattern to filter by.</param>
        /// <param name="rowKeyStartPattern">The row key start pattern to filter by.</param>
        /// <param name="take">The number of items to retrieve.</param>
        /// <param name="cancellationToken">A token to observe for cancellation requests.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation. 
        /// The resulting task contains the <see cref="ExGlobalValueResponse{T}"/> associated with the result of the operation. 
        /// If the operation succeeded, the response contains the 'Status' set to <see cref="ResponseStatus.Success"/> and a <see cref="List{T}"/> with the items retrieved;
        /// otherwise, the response contains the 'Status' set to <see cref="ResponseStatus.Failure"/>.</returns>
        public static async Task<ExGlobalValueResponse<List<T>>> QueryByPartitionKeyStartPatternRowKeyStartPatternAsync<T>(
            this TableClient tableClient,
            string partitionKeyStartPattern,
            string rowKeyStartPattern,
            int take = TableConstants.DefaultTake,
            CancellationToken cancellationToken = default)
            where T : class, ITableEntity, new()
        {
            return await TakeFromPageableAsync(
                tableClient,
                AsyncPageableQueryByPartitionKeyStartPatternRowKeyStartPattern<T>(
                    tableClient,
                    partitionKeyStartPattern,
                    rowKeyStartPattern,
                    TableQueryConstants.DefaultMaxPerPage, cancellationToken),
                take);
        }


        #endregion

        #region Timestamp

        // Sync

        /// <summary>
        /// Gets the <see cref="Pageable{T}"/> for synchronous queries using the specified <paramref name="timeStampFrom"/> and <paramref name="timeStampTo"/> within the filter.
        /// </summary>
        /// <typeparam name="T">The type of the entities in the query result.</typeparam>
        /// <param name="tableClient">The <see cref="TableClient"/> to query.</param>
        /// <param name="timeStampFrom">The start timestamp to filter by.</param>
        /// <param name="timeStampTo">The end timestamp to filter by.</param>
        /// <param name="maxPerPage">The maximum number of items per page.</param>
        /// <param name="cancellationToken">A token to observe for cancellation requests.</param>
        /// <returns>The <see cref="GlobalValueResponse{T, TEx}"/> associated with the result of the operation. 
        /// If the parameters are valid the response contains the 'Status' set to <see cref="ResponseStatus.Success"/>
        /// and a <see cref="Pageable{T}"/> for querying the items;
        /// otherwise, the response contains the 'Status' set to <see cref="ResponseStatus.Failure"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="tableClient"/> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="maxPerPage"/> is not between 0 and 1000.</exception>
        public static GlobalValueResponse<Pageable<T>, RequestFailedException> PageableQueryByTimestamp<T>(
            this TableClient tableClient,
            DateTime timeStampFrom,
            DateTime timeStampTo,
            int? maxPerPage = TableQueryConstants.DefaultMaxPerPage,
            CancellationToken cancellationToken = default)
            where T : class, ITableEntity, new()
        {
            return Query<T>(
                tableClient,
                TableQueryFilterBuilder.BuildTimestampFilter(QueryComparison.ge, timeStampFrom)
                .And(TableQueryFilterBuilder.BuildTimestampFilter(QueryComparison.lt, timeStampTo.AddMilliseconds(1))).ToString(),
                maxPerPage,
                cancellationToken);
        }

        /// <summary>
        /// Queries the Azure Table Service using the specified <paramref name="timeStampFrom"/> and <paramref name="timeStampTo"/> within the filter,
        /// and retrieves up to the specified number of items that match the filter defined by <paramref name="take"/>.
        /// </summary>
        /// <typeparam name="T">The type of the entities in the query result.</typeparam>
        /// <param name="tableClient">The <see cref="TableClient"/> to query.</param>
        /// <param name="timeStampFrom">The start timestamp to filter by.</param>
        /// <param name="timeStampTo">The end timestamp to filter by.</param>
        /// <param name="take">The number of items to retrieve.</param>
        /// <param name="cancellationToken">A token to observe for cancellation requests.</param>
        /// <returns>The <see cref="ExGlobalValueResponse{T}"/> associated with the result of the operation. 
        /// If the operation succeeded, the response contains the 'Status' set to <see cref="ResponseStatus.Success"/> and a <see cref="List{T}"/> with the items retrieved;
        /// otherwise, the response contains the 'Status' set to <see cref="ResponseStatus.Failure"/>.</returns>
        public static ExGlobalValueResponse<List<T>> QueryByTimestamp<T>(
            this TableClient tableClient,
            DateTime timeStampFrom,
            DateTime timeStampTo,
            int take = TableConstants.DefaultTake,
            CancellationToken cancellationToken = default)
            where T : class, ITableEntity, new()
        {
            return TakeFromPageable(
                tableClient,
                PageableQueryByTimestamp<T>(
                    tableClient,
                    timeStampFrom,
                    timeStampTo,
                    TableQueryConstants.DefaultMaxPerPage,
                    cancellationToken),
                take);
        }

        // Async

        /// <summary>
        /// Gets the <see cref="AsyncPageable{T}"/> for asynchronous queries using the specified <paramref name="timeStampFrom"/> and <paramref name="timeStampTo"/> within the filter.
        /// </summary>
        /// <typeparam name="T">The type of the entities in the query result.</typeparam>
        /// <param name="tableClient">The <see cref="TableClient"/> to query.</param>
        /// <param name="timeStampFrom">The start timestamp to filter by.</param>
        /// <param name="timeStampTo">The end timestamp to filter by.</param>
        /// <param name="maxPerPage">The maximum number of items per page.</param>
        /// <param name="cancellationToken">A token to observe for cancellation requests.</param>
        /// <returns>The <see cref="GlobalValueResponse{T, TEx}"/> associated with the result of the operation. 
        /// If the parameters are valid the response contains the 'Status' set to <see cref="ResponseStatus.Success"/>
        /// and a <see cref="AsyncPageable{T}"/> for querying the items;
        /// otherwise, the response contains the 'Status' set to <see cref="ResponseStatus.Failure"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="tableClient"/> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="maxPerPage"/> is not between 0 and 1000.</exception>
        public static GlobalValueResponse<AsyncPageable<T>, RequestFailedException> AsyncPageableQueryByTimestamp<T>(
            this TableClient tableClient,
            DateTime timeStampFrom,
            DateTime timeStampTo,
            int? maxPerPage = TableQueryConstants.DefaultMaxPerPage,
            CancellationToken cancellationToken = default)
            where T : class, ITableEntity, new()
        {
            return QueryAsync<T>(
                tableClient,
                TableQueryFilterBuilder.BuildTimestampFilter(QueryComparison.ge, timeStampFrom)
                .And(TableQueryFilterBuilder.BuildTimestampFilter(QueryComparison.lt, timeStampTo.AddMilliseconds(1))).ToString(),
                maxPerPage,
                cancellationToken);
        }

        /// <summary>
        /// Asynchronously queries the Azure Table Service using the specified <paramref name="timeStampFrom"/> and <paramref name="timeStampTo"/> within the filter,
        /// and retrieves up to the specified number of items that match the filter defined by <paramref name="take"/>.
        /// </summary>
        /// <typeparam name="T">The type of the entities in the query result.</typeparam>
        /// <param name="tableClient">The <see cref="TableClient"/> to query.</param>
        /// <param name="timeStampFrom">The start timestamp to filter by.</param>
        /// <param name="timeStampTo">The end timestamp to filter by.</param>
        /// <param name="take">The number of items to retrieve.</param>
        /// <param name="cancellationToken">A token to observe for cancellation requests.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation. 
        /// The resulting task contains the <see cref="ExGlobalValueResponse{T}"/> associated with the result of the operation. 
        /// If the operation succeeded, the response contains the 'Status' set to <see cref="ResponseStatus.Success"/> and a <see cref="List{T}"/> with the items retrieved;
        /// otherwise, the response contains the 'Status' set to <see cref="ResponseStatus.Failure"/>.</returns>
        public static async Task<ExGlobalValueResponse<List<T>>> QueryByTimestampAsync<T>(
            this TableClient tableClient,
            DateTime timeStampFrom,
            DateTime timeStampTo,
            int take = TableConstants.DefaultTake,
            CancellationToken cancellationToken = default)
            where T : class, ITableEntity, new()
        {
            return await TakeFromPageableAsync(
                tableClient,
                AsyncPageableQueryByTimestamp<T>(
                    tableClient,
                    timeStampFrom,
                    timeStampTo,
                    TableQueryConstants.DefaultMaxPerPage,
                    cancellationToken),
                take);
        }

        #endregion

        #region PartitionKeyTimestamp

        // Sync

        /// <summary>
        /// Gets the <see cref="Pageable{T}"/> for synchronous queries using the specified <paramref name="partitionKey"/>, <paramref name="timeStampFrom"/>, and <paramref name="timeStampTo"/> within the filter.
        /// </summary>
        /// <typeparam name="T">The type of the entities in the query result.</typeparam>
        /// <param name="tableClient">The <see cref="TableClient"/> to query.</param>
        /// <param name="partitionKey">The partition key to filter by.</param>
        /// <param name="timeStampFrom">The start timestamp to filter by.</param>
        /// <param name="timeStampTo">The end timestamp to filter by.</param>
        /// <param name="maxPerPage">The maximum number of items per page.</param>
        /// <param name="cancellationToken">A token to observe for cancellation requests.</param>
        /// <returns>The <see cref="GlobalValueResponse{T, TEx}"/> associated with the result of the operation. 
        /// If the parameters are valid the response contains the 'Status' set to <see cref="ResponseStatus.Success"/>
        /// and a <see cref="Pageable{T}"/> for querying the items;
        /// otherwise, the response contains the 'Status' set to <see cref="ResponseStatus.Failure"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="tableClient"/> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="maxPerPage"/> is not between 0 and 1000.</exception>
        public static GlobalValueResponse<Pageable<T>, RequestFailedException> PageableQueryByPartitionKeyTimestamp<T>(
            this TableClient tableClient,
            string partitionKey,
            DateTime timeStampFrom,
            DateTime timeStampTo,
            int? maxPerPage = TableQueryConstants.DefaultMaxPerPage,
            CancellationToken cancellationToken = default)
            where T : class, ITableEntity, new()
        {
            return Query<T>(
                tableClient,
                TableQueryFilterBuilder.BuildPartitionKeyFilter(QueryComparison.eq, partitionKey)
                .And(
                    TableQueryFilterBuilder.BuildTimestampFilter(QueryComparison.ge, timeStampFrom)
                    .And(TableQueryFilterBuilder.BuildTimestampFilter(QueryComparison.lt, timeStampTo.AddMilliseconds(1)))).ToString(),
                maxPerPage,
                cancellationToken);
        }

        /// <summary>
        /// Queries the Azure Table Service using the specified <paramref name="partitionKey"/>, <paramref name="timeStampFrom"/> and <paramref name="timeStampTo"/> within the filter,
        /// and retrieves up to the specified number of items that match the filter defined by <paramref name="take"/>.
        /// </summary>
        /// <typeparam name="T">The type of the entities in the query result.</typeparam>
        /// <param name="tableClient">The <see cref="TableClient"/> to query.</param>
        /// <param name="partitionKey">The partition key to filter by.</param>
        /// <param name="timeStampFrom">The start timestamp to filter by.</param>
        /// <param name="timeStampTo">The end timestamp to filter by.</param>
        /// <param name="take">The number of items to retrieve.</param>
        /// <param name="cancellationToken">A token to observe for cancellation requests.</param>
        /// <returns>The <see cref="ExGlobalValueResponse{T}"/> associated with the result of the operation. 
        /// If the operation succeeded, the response contains the 'Status' set to <see cref="ResponseStatus.Success"/> and a <see cref="List{T}"/> with the items retrieved;
        /// otherwise, the response contains the 'Status' set to <see cref="ResponseStatus.Failure"/>.</returns>
        public static ExGlobalValueResponse<List<T>> QueryByPartitionKeyTimestamp<T>(
            this TableClient tableClient,
            string partitionKey,
            DateTime timeStampFrom,
            DateTime timeStampTo,
            int take = TableConstants.DefaultTake,
            CancellationToken cancellationToken = default)
            where T : class, ITableEntity, new()
        {
            return TakeFromPageable(
                tableClient,
                PageableQueryByPartitionKeyTimestamp<T>(
                    tableClient,
                    partitionKey,
                    timeStampFrom,
                    timeStampTo,
                    TableQueryConstants.DefaultMaxPerPage,
                    cancellationToken),
                take);
        }

        // Async

        /// <summary>
        /// Gets the <see cref="AsyncPageable{T}"/> for asynchronous queries using the specified <paramref name="partitionKey"/>, <paramref name="timeStampFrom"/>, and <paramref name="timeStampTo"/> within the filter.
        /// </summary>
        /// <typeparam name="T">The type of the entities in the query result.</typeparam>
        /// <param name="tableClient">The <see cref="TableClient"/> to query.</param>
        /// <param name="partitionKey">The partition key to filter by.</param>
        /// <param name="timeStampFrom">The start timestamp to filter by.</param>
        /// <param name="timeStampTo">The end timestamp to filter by.</param>
        /// <param name="maxPerPage">The maximum number of items per page.</param>
        /// <param name="cancellationToken">A token to observe for cancellation requests.</param>
        /// <returns>The <see cref="GlobalValueResponse{T, TEx}"/> associated with the result of the operation. 
        /// If the parameters are valid the response contains the 'Status' set to <see cref="ResponseStatus.Success"/>
        /// and a <see cref="AsyncPageable{T}"/> for querying the items;
        /// otherwise, the response contains the 'Status' set to <see cref="ResponseStatus.Failure"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="tableClient"/> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="maxPerPage"/> is not between 0 and 1000.</exception>
        public static GlobalValueResponse<AsyncPageable<T>, RequestFailedException> AsyncPageableQueryByPartitionKeyTimestamp<T>(
            this TableClient tableClient,
            string partitionKey,
            DateTime timeStampFrom,
            DateTime timeStampTo,
            int? maxPerPage = TableQueryConstants.DefaultMaxPerPage,
            CancellationToken cancellationToken = default)
            where T : class, ITableEntity, new()
        {
            return QueryAsync<T>(
                tableClient,
                TableQueryFilterBuilder.BuildPartitionKeyFilter(QueryComparison.eq, partitionKey)
                .And(
                    TableQueryFilterBuilder.BuildTimestampFilter(QueryComparison.ge, timeStampFrom)
                    .And(TableQueryFilterBuilder.BuildTimestampFilter(QueryComparison.lt, timeStampTo.AddMilliseconds(1)))).ToString(),
                maxPerPage,
                cancellationToken);
        }

        /// <summary>
        /// Asynchronously queries the Azure Table Service using the specified <paramref name="partitionKey"/>, <paramref name="timeStampFrom"/> and <paramref name="timeStampTo"/> within the filter,
        /// and retrieves up to the specified number of items that match the filter defined by <paramref name="take"/>.
        /// </summary>
        /// <typeparam name="T">The type of the entities in the query result.</typeparam>
        /// <param name="tableClient">The <see cref="TableClient"/> to query.</param>
        /// <param name="partitionKey">The partition key to filter by.</param>
        /// <param name="timeStampFrom">The start timestamp to filter by.</param>
        /// <param name="timeStampTo">The end timestamp to filter by.</param>
        /// <param name="take">The number of items to retrieve.</param>
        /// <param name="cancellationToken">A token to observe for cancellation requests.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation. 
        /// The resulting task contains the <see cref="ExGlobalValueResponse{T}"/> associated with the result of the operation. 
        /// If the operation succeeded, the response contains the 'Status' set to <see cref="ResponseStatus.Success"/> and a <see cref="List{T}"/> with the items retrieved;
        /// otherwise, the response contains the 'Status' set to <see cref="ResponseStatus.Failure"/>.</returns>
        public static async Task<ExGlobalValueResponse<List<T>>> QueryByPartitionKeyTimestampAsync<T>(
            this TableClient tableClient,
            string partitionKey,
            DateTime timeStampFrom,
            DateTime timeStampTo,
            int take = TableConstants.DefaultTake,
            CancellationToken cancellationToken = default)
            where T : class, ITableEntity, new()
        {
            return await TakeFromPageableAsync(
                tableClient,
                AsyncPageableQueryByPartitionKeyTimestamp<T>(
                    tableClient,
                    partitionKey,
                    timeStampFrom,
                    timeStampTo,
                    TableQueryConstants.DefaultMaxPerPage,
                    cancellationToken),
                take);
        }

        #endregion

        #region QueryAll

        // Sync

        /// <summary>
        /// Gets the <see cref="Pageable{T}"/> for synchronous queries to retrieve all items.
        /// </summary>
        /// <typeparam name="T">The type of the entities in the query result.</typeparam>
        /// <param name="tableClient">The <see cref="TableClient"/> to query.</param>
        /// <param name="maxPerPage">The maximum number of entities that will be returned per page.</param>
        /// <param name="cancellationToken">A token to observe for cancellation requests.</param>
        /// <returns>The <see cref="GlobalValueResponse{T, TEx}"/> associated with the result of the operation. 
        /// If the parameters are valid the response contains the 'Status' set to <see cref="ResponseStatus.Success"/>
        /// and a <see cref="Pageable{T}"/> for querying the items;
        /// otherwise, the response contains the 'Status' set to <see cref="ResponseStatus.Failure"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="tableClient"/> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="maxPerPage"/> is not between 0 and 1000.</exception>
        public static GlobalValueResponse<Pageable<T>, RequestFailedException> PageableQueryAll<T>(
            this TableClient tableClient,
            int? maxPerPage = TableQueryConstants.DefaultMaxPerPage,
            CancellationToken cancellationToken = default)
            where T : class, ITableEntity, new()
        {
            return Query<T>(
                tableClient,
                string.Empty,
                maxPerPage,
                cancellationToken);
        }

        /// <summary>
        /// Queries the Azure Table Service and retrieves up to the specified number of items defined by <paramref name="take"/>.
        /// </summary>
        /// <typeparam name="T">The type of the entities in the query result.</typeparam>
        /// <param name="tableClient">The <see cref="TableClient"/> to query.</param>
        /// <param name="take">The number of items to retrieve.</param>
        /// <param name="cancellationToken">A token to observe for cancellation requests.</param>
        /// <returns>The <see cref="ExGlobalValueResponse{T}"/> associated with the result of the operation. 
        /// If the operation succeeded, the response contains the 'Status' set to <see cref="ResponseStatus.Success"/> and a <see cref="List{T}"/> with the items retrieved;
        /// otherwise, the response contains the 'Status' set to <see cref="ResponseStatus.Failure"/>.</returns>
        public static ExGlobalValueResponse<List<T>> QueryAll<T>(
            this TableClient tableClient,
            int take = TableConstants.DefaultTake,
            CancellationToken cancellationToken = default)
            where T : class, ITableEntity, new()
        {
            return TakeFromPageable(
                tableClient,
                PageableQueryAll<T>(
                    tableClient,
                    TableQueryConstants.DefaultMaxPerPage,
                    cancellationToken),
                take);
        }

        // Async

        /// <summary>
        /// Gets the <see cref="AsyncPageable{T}"/> for asynchronous queries to retrieve all items.
        /// </summary>
        /// <typeparam name="T">The type of the entities in the query result.</typeparam>
        /// <param name="tableClient">The <see cref="TableClient"/> to query.</param>
        /// <param name="maxPerPage">The maximum number of entities that will be returned per page.</param>
        /// <param name="cancellationToken">A token to observe for cancellation requests.</param>
        /// <returns>The <see cref="GlobalValueResponse{T, TEx}"/> associated with the result of the operation. 
        /// If the parameters are valid the response contains the 'Status' set to <see cref="ResponseStatus.Success"/>
        /// and a <see cref="AsyncPageable{T}"/> for querying the items;
        /// otherwise, the response contains the 'Status' set to <see cref="ResponseStatus.Failure"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="tableClient"/> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="maxPerPage"/> is not between 0 and 1000.</exception>
        public static GlobalValueResponse<AsyncPageable<T>, RequestFailedException> AsyncPageableQueryAll<T>(
            this TableClient tableClient,
            int? maxPerPage = TableQueryConstants.DefaultMaxPerPage,
            CancellationToken cancellationToken = default)
            where T : class, ITableEntity, new()
        {
            return QueryAsync<T>(
                tableClient,
                string.Empty,
                maxPerPage,
                cancellationToken);
        }

        /// <summary>
        /// Asynchronously queries the Azure Table Service and retrieves up to the specified number of items defined by <paramref name="take"/>.
        /// </summary>
        /// <typeparam name="T">The type of the entities in the query result.</typeparam>
        /// <param name="tableClient">The <see cref="TableClient"/> to query.</param>
        /// <param name="take">The number of items to retrieve.</param>
        /// <param name="cancellationToken">A token to observe for cancellation requests.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation. 
        /// The resulting task contains the <see cref="ExGlobalValueResponse{T}"/> associated with the result of the operation. 
        /// If the operation succeeded, the response contains the 'Status' set to <see cref="ResponseStatus.Success"/> and a <see cref="List{T}"/> with the items retrieved;
        /// otherwise, the response contains the 'Status' set to <see cref="ResponseStatus.Failure"/>.</returns>
        public static async Task<ExGlobalValueResponse<List<T>>> QueryAllAsync<T>(
            this TableClient tableClient,
            int take = TableConstants.DefaultTake,
            CancellationToken cancellationToken = default)
            where T : class, ITableEntity, new()
        {
            return await TakeFromPageableAsync(
                tableClient,
                AsyncPageableQueryAll<T>(
                    tableClient,
                    TableQueryConstants.DefaultMaxPerPage,
                    cancellationToken),
                take);
        }

        #endregion
    }
}
