
namespace Global.Common.Azure.Models
{
    /// <summary>
    ///  The set of options that can be specified in Azure to influence how
    ///  retry attempts are made, and a failure is eligible to be retried.
    ///  Based on the <see cref="RetryOptions"/> from Azure.Core by Microsoft.
    /// </summary>
    public class AzRetryOptions
    {
        /// <summary>
        /// The maximum number of retry attempts before giving up.
        /// </summary>
        public int MaxRetries { get; set; } = 3;

        /// <summary>
        /// The delay between retry attempts for a fixed approach or the delay
        /// on which to base calculations for a backoff-based approach.
        /// If the service provides a Retry-After response header, the next retry will be delayed by the duration specified by the header value.
        /// </summary>
        public TimeSpan Delay { get; set; }

        /// <summary>
        /// The maximum permissible delay between retry attempts when the service does not provide a Retry-After response header.
        /// If the service provides a Retry-After response header, the next retry will be delayed by the duration specified by the header value.
        /// </summary>
        public TimeSpan MaxDelay { get; set; }

        /// <summary>
        /// The approach to use for calculating retry delays.
        /// </summary>
        public RetryMode Mode { get; set; }

        /// <summary>
        /// The timeout applied to an individual network operations.
        /// </summary>
        public TimeSpan NetworkTimeout { get; set; }

        /// <summary>
        /// Copies the retry options to the specified <paramref name="retryOptions"/>.
        /// </summary>
        /// <param name="retryOptions">The instance of <see cref="RetryOptions"/> to which the retry options will be copied.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="retryOptions"/> is null.</exception>
        public virtual void CopyTo(RetryOptions retryOptions)
        {
            AssertHelper.AssertNotNullOrThrow(retryOptions, nameof(retryOptions));

            retryOptions.MaxRetries = MaxRetries;
            retryOptions.Delay = Delay;
            retryOptions.MaxDelay = MaxDelay;
            retryOptions.Mode = Mode;
            retryOptions.NetworkTimeout = NetworkTimeout;
        }
    }
}
