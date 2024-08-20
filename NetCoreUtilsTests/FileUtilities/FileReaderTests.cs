using Moq;
using System.Text;

namespace NetCoreUtils.FileUtilities.Tests
{
    [TestClass()]
    public class FileReaderTests
    {
        [TestMethod]
        public async Task ReadAllLinesAsync_Should_Throw_For_Empty_PathAsync()
        {
            // Arrange
            string emptyPath = "";

            // Act & Assert
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await FileReader.ReadAllLinesAsync(emptyPath, Encoding.UTF8));
        }

        [TestMethod]
        public async Task ReadAllLinesAsync_Should_Read_All_LinesAsync()
        {
            // Arrange
            string testFilePath = "test.txt";
            string line1 = "Line 1";
            string line2 = "Line 2";
            string content = string.Join(Environment.NewLine, line1, line2);
            await File.WriteAllTextAsync(testFilePath, content);

            // Act
            string[] lines = await FileReader.ReadAllLinesAsync(testFilePath, Encoding.UTF8);

            // Assert
            CollectionAssert.AreEqual(new[] { line1, line2 }, lines);

            // Cleanup
            File.Delete(testFilePath);
        }

        [TestMethod]
        public async Task ReadFileAsync_Should_Throw_For_Empty_PathAsync()
        {
            // Arrange
            string emptyPath = "";
            var mockProcessor = new Mock<IFileProcessor>();

            // Act & Assert
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await FileReader.ReadFileAsync(emptyPath, mockProcessor.Object, null));
        }

        [TestMethod]
        public async Task ReadFileAsync_Should_Throw_For_Null_ProcessorAsync()
        {
            // Arrange
            string filePath = "test.txt";

            // Act & Assert
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await FileReader.ReadFileAsync(filePath, null, null));
        }

        [TestMethod]
        public async Task ReadFileAsync_Should_Report_ProgressAsync()
        {
            // Arrange
            string testFilePath = "test.txt";
            byte[] testData = new byte[10240]; // Simulate a small file
            await File.WriteAllBytesAsync(testFilePath, testData);

            var mockProgress = new Mock<IProgress<double>>();
            double totalProgress = 0;
            mockProgress.Setup(p => p.Report(It.IsAny<double>()))
                .Callback<double>(progress => totalProgress = progress);

            // Act
            await FileReader.ReadFileAsync(testFilePath, Mock.Of<FileDataProcessor>(), mockProgress.Object);

            // Assert
            Assert.AreEqual(100, totalProgress, 0.01); // Allow for minor rounding errors

            // Cleanup
            File.Delete(testFilePath);
        }
    }
}