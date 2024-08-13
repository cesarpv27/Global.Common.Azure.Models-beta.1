
namespace Global.Common.Azure.Models
{
    /// <summary>
    /// Represents an error specific to Azure storage service with optional error code.
    /// </summary>
    public interface IAzStorageError<TEC> where TEC : struct
    {
        /// <summary>
        /// Gets a value indicating whether the error has a <see cref="ErrorCode"/>.
        /// </summary>
        bool HasErrorCode { get; }

        /// <summary>
        /// Gets the error code associated with the Azure error.
        /// </summary>
        TEC? ErrorCode { get; }
    }
}
