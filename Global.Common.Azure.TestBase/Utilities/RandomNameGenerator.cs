
namespace Global.Common.Azure.TestBase
{
    /// <summary>
    /// Provides functionality to generate random names of a specified length.
    /// </summary>
    public class RandomNameGenerator
    {
        private static Random random = new Random();
        private static int _maxLength = 100;

        /// <summary>
        /// Gets or sets the maximum length allowed for the random name generator.
        /// </summary>
        /// <value>
        /// The maximum length that can be used to generate random names. The default value is 100.
        /// </value>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the value is less than 1.</exception>
        public static int MaxLength
        {
            get
            {
                return _maxLength;
            }
            set
            {
                ArgumentOutOfRangeException.ThrowIfLessThan(value, 1);

                _maxLength = value;
            }
        }

        /// <summary>
        /// Generates a random name consisting of uppercase and lowercase letters with the specified length.
        /// </summary>
        /// <param name="length">The length of the random name to generate. Must be between 1 and 100.</param>
        /// <returns>A random name of the specified length.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="length"/> is less than 1 
        /// or greater than <see cref="MaxLength"/>.</exception>
        public static string GenerateRandomName(int length)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(length, 1);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(length, MaxLength);

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
