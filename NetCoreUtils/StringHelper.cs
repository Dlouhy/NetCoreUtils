using System.Globalization;
using System.Text;

namespace NetCoreUtils
{
    /// <summary>
    /// Utilities for String formatting and manipulation.
    /// </summary>
    public static class StringHelper
    {
        /// <summary>
        /// Removes the diacritics using the specified input
        /// </summary>
        /// <remarks>https://stackoverflow.com/a/249126</remarks>
        /// <param name="input">Input string with diacritics</param>
        /// <returns>String without diacritics</returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the input string is null or empty.
        /// </exception>
        public static string RemoveDiacritics(this string input)
        {
            if (string.IsNullOrEmpty(input))
                throw new ArgumentNullException(nameof(input), "Input string can not be empty.");

            string normalizedString = input.Normalize(NormalizationForm.FormD);

            StringBuilder stringBuilder = new StringBuilder();

            foreach (char c in normalizedString)
            {
                UnicodeCategory unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);

                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        /// <summary>
        /// Make first letter of a string upper case (C#7)
        /// </summary>
        /// <remarks>https://stackoverflow.com/a/4405876</remarks>
        /// <param name="input">The string to capitalize.</param>
        /// <returns>String with the first capitalized character</returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the input string is null or empty.
        /// </exception>
        public static string FirstCharToUpper(this string input)
        {
            if (string.IsNullOrEmpty(input))
                throw new ArgumentNullException(nameof(input), "Input string can not be empty.");

            return input[0].ToString().ToUpperInvariant() + input.Substring(1);
        }
    }
}