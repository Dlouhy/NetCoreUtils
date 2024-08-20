using CSharpFunctionalExtensions;
using System.Globalization;

namespace NetCoreUtils
{
    /// <summary>
    /// DateTimeRange value object with helper methods
    /// </summary>
    public sealed class DateTimeRange : ValueObject
    {
        /// <summary>
        /// Gets start date/time from DateTimeRange.
        /// </summary>
        public DateTimeOffset StartDateTime { get; }

        /// <summary>
        /// Gets end date/time from DateTimeRange.
        /// </summary>
        public DateTimeOffset EndDateTime { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DateTimeRange"/> class with a start and end
        /// date.
        /// </summary>
        /// <param name="startDateTime">The start date and time.</param>
        /// <param name="endDateTime">The end date and time.</param>
        private DateTimeRange(DateTimeOffset startDateTime, DateTimeOffset endDateTime)
        {
            StartDateTime = startDateTime;
            EndDateTime = endDateTime;
        }

        /// <summary>
        /// Creates a new DateTimeRange object with a start date and end date. Ensures the start
        /// date is before the end date."
        /// </summary>
        /// <param name="startDateTime">The start date and time.</param>
        /// <param name="endDateTime">The end date and time.</param>
        /// <returns>
        /// Returns a Result object indicating success or failure. On failure, the Error property
        /// will contain an error message.
        /// </returns>
        public static Result<DateTimeRange> Create(DateTimeOffset startDateTime, DateTimeOffset endDateTime)
        {
            if (startDateTime > endDateTime)
                return Result.Failure<DateTimeRange>("Start date/time is later than end date/time.");

            return Result.Success(new DateTimeRange(startDateTime, endDateTime));
        }

        /// <summary>
        /// Creates a new DateTimeRange object by parsing the provided start and end date strings
        /// using the specified formats array.
        /// </summary>
        /// <param name="startDateTime">The start date/time string to parse.</param>
        /// <param name="endDateTime">The end date/time string to parse.</param>
        /// <param name="formats">An array of possible date/time formats.</param>
        /// <returns>
        /// A Result object. On success, it contains a new DateTimeRange object. On failure, it
        /// contains the error message from parsing.
        /// </returns>
        public static Result<DateTimeRange> Create(string startDateTime, string endDateTime, string[] formats)
        {
            var startDateTimeOrFailure = Parse(startDateTime, formats);
            var endDateTimeOrFailure = Parse(endDateTime, formats);

            if (startDateTimeOrFailure.IsFailure)
                return Result.Failure<DateTimeRange>($"{startDateTimeOrFailure.Error} Parameter: startDateTime");

            if (endDateTimeOrFailure.IsFailure)
                return Result.Failure<DateTimeRange>($"{endDateTimeOrFailure.Error} Parameter: endDateTime");

            return Create(startDateTimeOrFailure.Value, endDateTimeOrFailure.Value);
        }

        /// <summary>
        /// Formats the StartDateTime property into a string using the specified format and
        /// InvariantCulture.
        /// </summary>
        /// <param name="dateTimeFormat">
        /// The format string to use for formatting the date and time.
        /// </param>
        /// <returns>A string representing the formatted StartDateTime.</returns>
        public string StartDateTimeFormatted(string dateTimeFormat)
        {
            return StartDateTimeFormatted(dateTimeFormat, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Formats the StartDateTime property into a string using the specified format and culture.
        /// </summary>
        /// <param name="dateTimeFormat">
        /// The format string to use for formatting the date and time.
        /// </param>
        /// <param name="cultureInfo">The CultureInfo object to use for formatting.</param>
        /// <returns>A string representing the formatted StartDateTime.</returns>
        public string StartDateTimeFormatted(string dateTimeFormat, CultureInfo cultureInfo)
        {
            return StartDateTime.ToString(dateTimeFormat, cultureInfo);
        }

        /// <summary>
        /// Formats the EndDateTime property into a string using the specified format and the
        /// InvariantCulture.
        /// </summary>
        /// <param name="dateTimeFormat">
        /// The format string to use for formatting the date and time.
        /// </param>
        /// <returns>A string representing the formatted EndDateTime.</returns>
        public string EndDateTimeFormatted(string dateTimeFormat)
        {
            return EndDateTimeFormatted(dateTimeFormat, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Formats the EndDateTime property into a string using the specified format and culture.
        /// </summary>
        /// <param name="dateTimeFormat">
        /// The format string to use for formatting the date and time.
        /// </param>
        /// <param name="cultureInfo">The CultureInfo object to use for formatting.</param>
        /// <returns>A string representing the formatted EndDateTime.</returns>
        public string EndDateTimeFormatted(string dateTimeFormat, CultureInfo cultureInfo)
        {
            return EndDateTime.ToString(dateTimeFormat, cultureInfo);
        }

        /// <summary>
        /// Creates a new DateTimeRange by adding the specified duration to the internal
        /// StartDateTime.
        /// </summary>
        /// <param name="newDuration">The TimeSpan representing the duration to add.</param>
        /// <returns>A Result object containing the newly created DateTimeRange.</returns>
        public Result<DateTimeRange> CreateNewDuration(TimeSpan newDuration)
        {
            return Create(StartDateTime, StartDateTime.Add(newDuration));
        }

        /// <summary>
        /// Creates a new DateTimeRange object with the same end date but a new start date.
        /// </summary>
        /// <param name="newStart">The new start date and time.</param>
        /// <returns>
        /// Returns a Result object indicating success or failure. On failure, the Error property
        /// will contain an error message.
        /// </returns>
        public Result<DateTimeRange> CreateNewStart(DateTimeOffset newStart)
        {
            return Create(newStart, EndDateTime);
        }

        /// <summary>
        /// Creates a new DateTimeRange object with the same start date but a new end date.
        /// </summary>
        /// <param name="newEnd">The new end date and time.</param>
        /// Returns a Result object indicating success or failure. On failure, the Error property
        /// will contain an error message.
        public Result<DateTimeRange> CreateNewEnd(DateTimeOffset newEnd)
        {
            return Create(StartDateTime, newEnd);
        }

        /// <summary>
        /// Creates a new Result object containing a DateTimeRange for a single day.
        /// </summary>
        /// <param name="day">The DateTimeOffset representing the specific day for the range.</param>
        /// <returns>
        /// A Result object containing the newly created DateTimeRange spanning the specified day.
        /// </returns>
        public static Result<DateTimeRange> CreateOneDayRange(DateTimeOffset day)
        {
            return Create(day, day.AddDays(1));
        }

        /// <summary>
        /// Creates a new Result object containing a DateTimeRange for one week.
        /// </summary>
        /// <param name="startDay">
        /// The DateTimeOffset representing the start day of the week range.
        /// </param>
        /// <returns>
        /// A Result object containing the newly created DateTimeRange spanning the specified week.
        /// </returns>
        public static Result<DateTimeRange> CreateOneWeekRange(DateTimeOffset startDay)
        {
            return Create(startDay, startDay.AddDays(7));
        }

        /// <summary>
        /// Gets the date range for the previous month relative to the specified datetime (defaults
        /// to DateTimeOffset.UtcNow).
        /// </summary>
        /// <param name="now">
        /// Optional. The datetime representing the current time. If not provided,
        /// DateTimeOffset.UtcNow is used.
        /// </param>
        /// <returns>
        /// A Result object containing a DateTimeRange representing the previous month.
        /// </returns>
        public static Result<DateTimeRange> CreatePreviousMonth(DateTimeOffset? now = null)
        {
            if (now == null)
            {
                now = DateTimeOffset.UtcNow;
            }
            return Create(now.Value.AddMonths(-1), now.Value);
        }

        /// <summary>
        /// Returns DateTimeRange object that adds a specified number of whole and fractional days
        /// to the value of parameter now
        /// </summary>
        /// <param name="days">
        /// A number of whole and fractional days. The number can be negative or positive.
        /// </param>
        /// <param name="now">
        /// An optional DateTimeOffset representing the reference date from which to calculate the
        /// range. Defaults to DateTimeOffset.UtcNow if not provided.
        /// </param>
        /// <returns>
        /// A Result object, containing value is the sum of the date and time represented by the
        /// current System.DateTimeOffset object and the number of days represented by days.
        /// </returns>
        public static Result<DateTimeRange> CreatePreviousDays(int days, DateTimeOffset? now = null)
        {
            if (now == null)
            {
                now = DateTimeOffset.UtcNow;
            }

            return Create(now.Value.AddDays(-1 * days), now.Value);
        }

        /// <summary>
        /// Calculates the duration of the DateTimeRange in minutes. Rounds the total number of
        /// minutes to the nearest whole number.
        /// </summary>
        /// <returns>The duration of the DateTimeRange in whole minutes.</returns>
        public int DurationInMinutes()
        {
            return (int)Math.Round((EndDateTime - StartDateTime).TotalMinutes, 0);
        }

        /// <summary>
        /// Parses a string representation of a date and time offset into a DateTimeOffset object.
        /// </summary>
        /// <param name="dateTimeValue">The string containing the date and time offset value.</param>
        /// <param name="formats">
        /// An array of possible date and time formats that the string might be in.
        /// </param>
        /// <returns>
        /// A Result object containing either the parsed DateTimeOffset value or an error message if
        /// the parsing fails.
        /// </returns>
        private static Result<DateTimeOffset> Parse(string dateTimeValue, string[] formats)
        {
            if (string.IsNullOrEmpty(dateTimeValue))
                return Result.Failure<DateTimeOffset>("DateTimeValue can not be empty or null.");

            var dateTimeOffsetValue = new DateTimeOffset();
            if (!formats.Any(f => DateTimeOffset.TryParseExact(dateTimeValue, f, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTimeOffsetValue)))

            {
                return Result.Failure<DateTimeOffset>("DateTimeValue has unrecognized format.");
            }

            return dateTimeOffsetValue;
        }

        protected override IEnumerable<IComparable> GetEqualityComponents()
        {
            yield return StartDateTime;
            yield return EndDateTime;
        }

        public override string ToString()
        {
            return $"Start date/time: {StartDateTime} -  End date/time: {EndDateTime}";
        }
    }
}