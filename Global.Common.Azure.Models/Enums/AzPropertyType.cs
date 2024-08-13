
namespace Global.Common.Azure.Models
{
    /// <summary>
    /// Defines property types used in Azure Table Service.
    /// </summary>
    public enum AzPropertyType
    {
        /// <summary>
        /// Represents a string property type.
        /// </summary>
        String,

        /// <summary>
        /// Represents a 32-bit integer property type.
        /// </summary>
        Int32,

        /// <summary>
        /// Represents a 64-bit integer property type.
        /// </summary>
        Int64,

        /// <summary>
        /// Represents a double precision floating point property type.
        /// </summary>
        Double,

        /// <summary>
        /// Represents a binary property type.
        /// </summary>
        Binary,

        /// <summary>
        /// Represents a boolean property type.
        /// </summary>
        Boolean,

        /// <summary>
        /// Represents a GUID (Globally Unique Identifier) property type.
        /// </summary>
        Guid,

        /// <summary>
        /// Represents a DateTime property type.
        /// </summary>
        DateTime,
    }
}
