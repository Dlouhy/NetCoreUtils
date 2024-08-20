namespace NetCoreUtils
{
    /// <summary>
    /// Methods for operations with bytes
    /// </summary>
    public static class ByteHelper
    {
        /// <summary>
        /// Converts a jagged array of bytes to a single array of bytes.
        /// </summary>
        /// <param name="jaggedArray">The jagged array of bytes to convert.</param>
        /// <returns>A single array of bytes representing the jagged array.</returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the input jagged array is null.
        /// </exception>
        public static async Task<byte[]> JaggedArrayToSingleArrayAsync(params byte[][] jaggedArray)
        {
            if (jaggedArray == null || jaggedArray.Any(a => a == null))
            {
                throw new ArgumentNullException(nameof(jaggedArray), "Input jagged array cannot be null.");
            }

            await using (MemoryStream memoryStream = new MemoryStream())
            {
                foreach (byte[] array in jaggedArray)
                {
                    await memoryStream.WriteAsync(array.AsMemory());
                    await memoryStream.FlushAsync();
                }

                return memoryStream.ToArray();
            }
        }

        /// <summary>
        /// Converts a hexadecimal string to a byte array.
        /// </summary>
        /// <param name="hex">The hexadecimal string to convert.</param>
        /// <returns>A byte array representation of the hexadecimal string.</returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the input string is null or empty.
        /// </exception>
        /// <exception cref="FormatException">
        /// Thrown when the input string is not even-numbered.
        /// </exception>
        public static byte[] HexStringToByteArray(this string hex)
        {
            if (string.IsNullOrEmpty(hex))
            {
                throw new ArgumentNullException(nameof(hex), "Input string cannot be null or empty.");
            }

            if (hex.Length % 2 != 0)
            {
                throw new FormatException("Input string must have an even number of characters (hex format).");
            }

            int numberChars = hex.Length;
            byte[] bytes = new byte[numberChars / 2];
            for (int i = 0; i < numberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }
    }
}