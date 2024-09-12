namespace NetCoreUtils.Tests
{
    [TestClass]
    public class DateTimeRangeTests
    {
        [TestMethod]
        public void Create_StartAfterEnd_ReturnsFailure()
        {
            var start = DateTimeOffset.UtcNow;
            var end = start.Subtract(TimeSpan.FromHours(1));

            var result = DateTimeRange.Create(start, end);

            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void Create_ValidDates_ReturnsSuccess()
        {
            var start = DateTimeOffset.UtcNow;
            var end = start.AddHours(1);

            var result = DateTimeRange.Create(start, end);

            Assert.IsTrue(result.IsSuccess);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(start, result.Value.StartDateTime);
            Assert.AreEqual(end, result.Value.EndDateTime);
        }

        [TestMethod]
        public void CreateNewStart_ValidNewStart_ReturnsSuccess()
        {
            var currentDateTime = DateTimeOffset.UtcNow;

            var originalRange = DateTimeRange.Create(currentDateTime, currentDateTime.AddDays(1)).Value;
            var newStart = originalRange.StartDateTime.Subtract(TimeSpan.FromDays(1));

            var result = originalRange.CreateNewStart(newStart);

            Assert.IsTrue(result.IsSuccess);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(newStart, result.Value.StartDateTime);
            Assert.AreEqual(originalRange.EndDateTime, result.Value.EndDateTime);
        }

        [TestMethod]
        public void CreateNewStart_NewStartAfterEnd_ReturnsFailure()
        {
            var currentDateTime = DateTimeOffset.UtcNow;

            var originalRange = DateTimeRange.Create(currentDateTime, currentDateTime.AddDays(1)).Value;
            var newStart = originalRange.EndDateTime.AddDays(1);

            var result = originalRange.CreateNewStart(newStart);

            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void CreateNewEnd_ValidNewEnd_ReturnsSuccess()
        {
            var currentDateTime = DateTimeOffset.UtcNow;

            var originalRange = DateTimeRange.Create(currentDateTime, currentDateTime.AddDays(1)).Value;
            var newEnd = originalRange.EndDateTime.AddHours(2);

            var result = originalRange.CreateNewEnd(newEnd);

            Assert.IsTrue(result.IsSuccess);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(originalRange.StartDateTime, result.Value.StartDateTime);
            Assert.AreEqual(newEnd, result.Value.EndDateTime);
        }

        [TestMethod]
        public void CreateNewEnd_NewEndBeforeStart_ReturnsFailure()
        {
            var currentDateTime = DateTimeOffset.UtcNow;

            var originalRange = DateTimeRange.Create(currentDateTime, currentDateTime.AddDays(1)).Value;
            var newEnd = originalRange.StartDateTime.Subtract(TimeSpan.FromHours(1));

            var result = originalRange.CreateNewEnd(newEnd);

            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void DurationInMinutes_ValidRange_ReturnsCorrectDuration()
        {
            var start = DateTimeOffset.UtcNow;
            var end = start.AddHours(1);
            var expectedDuration = 60; // Assuming 1 hour = 60 minutes

            var range = DateTimeRange.Create(start, end).Value;
            int actualDuration = range.DurationInMinutes();

            Assert.IsTrue(actualDuration >= expectedDuration); // Account for potential rounding errors
        }
    }
}