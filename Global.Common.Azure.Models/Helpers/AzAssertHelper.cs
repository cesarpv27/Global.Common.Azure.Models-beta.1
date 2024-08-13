
namespace Global.Common.Azure.Models.Helpers
{
    /// <summary>
    /// Helper class for assertion operations in Azure.
    /// </summary>
    public static class AzAssertHelper
    {
        /// <summary>
        /// Asserts whether the provided <paramref name="tableName"/> is in the correct format for Azure Table Service; otherwise, throws an exception.
        /// A name in the correct format must follow these conditions:
        /// - All characters must be in the English alphabet.
        /// - All characters must be letters or numbers.
        /// - The name must begin with a letter.
        /// - The length must be between 3 and 63 characters.
        /// - Some names are reserved, including 'tables'.
        /// For more information, visit https://docs.microsoft.com/en-us/rest/api/storageservices/Understanding-the-Table-Service-Data-Model.
        /// </summary>
        /// <param name="tableName">The table name to assert.</param>
        /// <param name="paramName">The parameter name for the table name (optional).</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="tableName"/> has an incorrect format.</exception>
        public static void AssertTableNameOrThrow(string tableName, string? paramName)
        {
            var validationResult = ValidateTableName(tableName);
            if (!validationResult.IsValid)
                throw new ArgumentException(validationResult.Message, paramName);
        }

        /// <summary>
        /// Determines if the <paramref name="tableName"/> is a valid name for a table in Azure Table Service.
        /// A name in the correct format must follow these conditions:
        /// - All characters must be in the English alphabet.
        /// - All characters must be letters or numbers.
        /// - The name must begin with a letter.
        /// - The length must be between 3 and 63 characters.
        /// - Some names are reserved, including 'tables'.
        /// For more information, visit https://docs.microsoft.com/en-us/rest/api/storageservices/Understanding-the-Table-Service-Data-Model.
        /// </summary>
        /// <param name="tableName">The name of the table.</param>
        /// <returns><c>true</c> if the <paramref name="tableName"/> is valid, otherwise <c>false</c>.</returns>
        public static (bool IsValid, string? Message) ValidateTableName(string? tableName)
        {
            if (string.IsNullOrEmpty(tableName))
                return (false, AzAssertHelperConstants.TheNameOfIsNullOrEmpty(AzAssertHelperConstants.table));

            if (tableName.Equals("tables"))
                return (false, AzAssertHelperConstants.TheNameOfHasAnIncorrectFormat(
                    AzAssertHelperConstants.table,
                    "Some table names are reserved, including 'tables'. Attempting to create a table with a reserved table name returns error code 404 (Bad Request)."));

            if (tableName.Length < 3 || tableName.Length > 63)
                return (false, AzAssertHelperConstants.TheLengthOfIsOutOfRange("table", "Table names", 3, 63));

            // Ensure the first character is a letter (A-Z, a-z)
            if (!IsEnglishLetter(tableName[0])) 
                return (false, AzAssertHelperConstants.TheNameOfHasAnIncorrectFormat(
                    AzAssertHelperConstants.table,
                    "Table names must begin with a letter."));

            // Ensure all characters are letters (A-Z, a-z) or digits (0-9)
            for (int i = 1; i < tableName.Length; i++)
                if (!IsEnglishLetterOrDigit(tableName[i]))
                    return (false, AzAssertHelperConstants.TheNameOfHasAnIncorrectFormat(
                        AzAssertHelperConstants.table,
                        "Table names may contain only alphanumeric characters."));

            return (true, default);
        }

        /// <summary>
        /// Asserts whether the provided <paramref name="blobContainerName"/> is in the correct format for Azure Storage; otherwise, throws an exception.
        /// A name in the correct format must follow these conditions:
        /// - All characters must be in the English alphabet.
        /// - All characters must be letters, numbers, or the '-' character.
        /// - All characters must be lowercase.
        /// - The first and last characters must be a letter or a number.
        /// - The length must be between 3 and 63 characters.
        /// For more information, visit https://learn.microsoft.com/en-us/rest/api/storageservices/naming-and-referencing-containers--blobs--and-metadata.
        /// </summary>
        /// <param name="blobContainerName">The name of the blob container to assert.</param>
        /// <param name="paramName">The parameter name for the blob container name (optional).</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="blobContainerName"/> has an incorrect format.</exception>
        public static void AssertBlobContainerNameOrThrow(string blobContainerName, string? paramName)
        {
            var validationResult = ValidateBlobContainerName(blobContainerName);
            if (!validationResult.IsValid)
                throw new ArgumentException(validationResult.Message, paramName);
        }

        /// <summary>
        /// Determines if the <paramref name="blobContainerName"/> is a valid name for an blob container in Azure Storage.
        /// A name in the correct format must follow these conditions:
        /// - All characters must be in the English alphabet.
        /// - All characters must be letters, numbers, or the '-' character.
        /// - All characters must be lowercase.
        /// - The first and last characters must be a letter or a number.
        /// - The length must be between 3 and 63 characters.
        /// For more information, visit https://learn.microsoft.com/en-us/rest/api/storageservices/naming-and-referencing-containers--blobs--and-metadata.
        /// </summary>
        /// <param name="blobContainerName">The name of the blob container.</param>
        /// <returns><c>true</c> if the <paramref name="blobContainerName"/> is valid, otherwise <c>false</c>.</returns>
        public static (bool IsValid, string? Message) ValidateBlobContainerName(string? blobContainerName)
        {
            if (string.IsNullOrEmpty(blobContainerName))
                return (false, AzAssertHelperConstants.TheNameOfIsNullOrEmpty(AzAssertHelperConstants.blobContainer));

            if (blobContainerName.Length < 3 || blobContainerName.Length > 63)
                return (false, AzAssertHelperConstants.TheLengthOfIsOutOfRange(AzAssertHelperConstants.blobContainer, "Blob container names", 3, 63));

            // Ensure the first and last characters are a lowercase letter (a-z) or digits (0-9)
            if (!IsEnglishLowerCaseLetterOrDigit(blobContainerName[0]) || !IsEnglishLowerCaseLetterOrDigit(blobContainerName[^1]))
                return (false, AzAssertHelperConstants.TheNameOfHasAnIncorrectFormat(
                    AzAssertHelperConstants.blobContainer,
                    "Container names must start and end with a letter or digit."));

            // Ensure intermediate characters are lowercase letters (a-z) or digits (0-9)
            for (int i = 1; i < blobContainerName.Length - 1; i++)
                if (!IsEnglishLowerCaseLetterOrDigit(blobContainerName[i]) && blobContainerName[i] != '-')
                    return (false, AzAssertHelperConstants.TheNameOfHasAnIncorrectFormat(
                        AzAssertHelperConstants.blobContainer,
                        "All letters in a container name must be lowercase."));

            return (true, default);
        }

        /// <summary>
        /// Asserts whether the provided <paramref name="blobName"/> is in the correct format for Azure Storage; otherwise, throws an exception.
        /// </summary>
        /// <param name="blobName">The directory and name of the blob to assert.</param>
        /// <param name="paramName">The parameter name (optional).</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="blobName"/> has an incorrect format.</exception>
        public static void AssertDirectoryAndBlobNameOrThrow(string blobName, string? paramName)
        {
            var validationResult = ValidateDirectoryAndBlobName(blobName);
            if (!validationResult.IsValid)
                throw new ArgumentException(validationResult.Message, paramName);
        }

        /// <summary>
        /// Determines if the <paramref name="blobName"/> is a valid directory and name for an blob in Azure Storage.
        /// </summary>
        /// <param name="blobName">The directory and name of the blob to assert.</param>
        /// <returns><c>true</c> if the <paramref name="blobName"/> is valid, otherwise <c>false</c>.</returns>
        public static (bool IsValid, string? Message) ValidateDirectoryAndBlobName(string? blobName)
        {
            if (string.IsNullOrEmpty(blobName))
                return (false, AzAssertHelperConstants.TheNameOfIsNullOrEmpty(AzAssertHelperConstants.blob));

            if (blobName.Length < 1)
                return (false, AzAssertHelperConstants.TheLengthOfIsOutOfRange(AzAssertHelperConstants.blob, "Blob directory and name", 1, 1024));

            return (true, default);
        }

        #region Private methods

        private static bool IsEnglishLetter(char c)
        {
            return IsEnglishUpperCaseLetter(c) || IsEnglishLowerCaseLetter(c);
        }

        private static bool IsEnglishUpperCaseLetter(char c)
        {
            return (c >= 'A' && c <= 'Z');
        }

        private static bool IsEnglishLowerCaseLetter(char c)
        {
            return (c >= 'a' && c <= 'z');
        }

        private static bool IsEnglishLetterOrDigit(char c)
        {
            return IsEnglishLetter(c) || (c >= '0' && c <= '9');
        }

        private static bool IsEnglishLowerCaseLetterOrDigit(char c)
        {
            return IsEnglishLowerCaseLetter(c) || (c >= '0' && c <= '9');
        }

        private static bool IsValidSegment(string segment)
        {
            foreach (var _char in segment)
                if (_char != '.')
                    return true;

            return false;
        }

        #endregion
    }
}
