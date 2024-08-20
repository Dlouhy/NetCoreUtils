namespace NetCoreUtils
{
    /// <summary>
    /// Converter between DateTime and Unix timestamp format (milliseconds from 1.1.1970)
    /// </summary>
    /// <remarks>https://stackoverflow.com/a/7983514</remarks>
    public static class UnixTimeHelper
    {
        /// <summary>
        /// Unix timestamp - milliseconds from 1.1.1970
        /// </summary>
        private static DateTime unixEpochDate = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        /// Converts Unix timestamp format in long into DateTime.
        /// </summary>
        /// <param name="miliseconds">Unix timestamp in milliseconds in long</param>
        /// <returns>Returns date and time</returns>
        public static DateTime ConvertToDateTime(this long miliseconds)
        {
            return unixEpochDate.AddMilliseconds(miliseconds);
        }

        /// <summary>
        /// Converts Unix timestamp in double into DateTime.
        /// </summary>
        /// <param name="miliseconds">Unix timestamp in milliseconds in double</param>
        /// <returns>Returns date and time</returns>
        public static DateTime ConvertToDateTime(this double miliseconds)
        {
            return unixEpochDate.AddMilliseconds(miliseconds);
        }

        /// <summary>
        /// Converts datetime into Unix timestamp format
        /// </summary>
        /// <param name="date">Date and time</param>
        /// <returns>Returns Unix date and time representation of type in milliseconds</returns>
        public static long ConvertToUnixTimestamp(this DateTime date)
        {
            var timeSpan = date.Subtract(unixEpochDate);
            return (long)timeSpan.TotalMilliseconds;
        }
    }
}