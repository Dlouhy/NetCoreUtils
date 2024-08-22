using System.Text;

namespace NetCoreUtils.FileUtilities.Tests
{
    [TestClass()]
    public class FileWriterTests
    {
        [TestMethod]
        public async Task WriteTextToFileAsync_ThrowsArgumentNullException_WhenFilePathIsNullAsync()
        {
            // Arrange
            string testText = "This is some test text.";
            Encoding encoding = Encoding.UTF8;
            string? filePath = null;

            // Act & Assert
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await FileWriter.WriteTextToFileAsync(filePath, testText, encoding));
        }

        [TestMethod]
        public async Task WriteTextToFileAsync_ThrowsArgumentException_WhenFilePathIsEmptyAsync()
        {
            // Arrange
            string testText = "This is some test text.";
            Encoding encoding = Encoding.UTF8;
            string filePath = "";

            // Act & Assert
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await FileWriter.WriteTextToFileAsync(filePath, testText, encoding));
        }

        [TestMethod]
        public async Task WriteTextToFileAsync_ThrowsArgumentException_WhenTextIsEmptyAsync()
        {
            // Arrange
            string testText = "";
            Encoding encoding = Encoding.UTF8;
            string filePath = "";

            // Act & Assert

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await FileWriter.WriteTextToFileAsync(filePath, testText, encoding));
        }

        [TestMethod]
        public async Task WriteTextToFileAsync_CancellationRequested_ThrowsTaskCanceledExceptionAsync()
        {
            // Arrange
            var filePath = "test.txt";
            var text = "This is some test text.";
            var encoding = Encoding.UTF8;
            using var cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.Cancel();
            var cancellationToken = cancellationTokenSource.Token;

            // Act & Assert
            await Assert.ThrowsExceptionAsync<TaskCanceledException>(async () => await FileWriter.WriteTextToFileAsync(filePath, text, encoding, cancellationToken));
        }

        [TestMethod]
        public void WriteTextToFileAsync_NullFilePath_ThrowsArgumentNullExceptionAsync()
        {
            // Arrange
            string? filePath = null;
            var text = "This is some test text.";
            var encoding = Encoding.UTF8;
            var cancellationToken = CancellationToken.None;

            // Act & Assert
            Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await FileWriter.WriteTextToFileAsync(filePath, text, encoding, cancellationToken));
        }

        [TestMethod]
        [DeploymentItem("FileToDeploy.txt")]
        [Description("Check to see if a file exists using the [DeploymentItem] attribute.")]
        public void WriteTextToFileAsync_NullText_ThrowsArgumentNullException()
        {
            // Arrange
            var filePath = "FileToDeploy.txt";
            string? text = null;
            var encoding = Encoding.UTF8;
            var cancellationToken = CancellationToken.None;

            // Act & Assert
            Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await FileWriter.WriteTextToFileAsync(filePath, text, encoding, cancellationToken));
        }

        [TestMethod]
        [Description("Check to see if a file exists using the [DeploymentItem] attribute.")]
        public async Task WriteTextToFileAsync_AppendsTextToExistingFileAsync()
        {
            // Arrange
            var text = "Appended text";
            var filePath = "FileToDeploy.txt";

            await File.WriteAllTextAsync(filePath, "Initial content");

            // Act
            await FileWriter.WriteTextToFileAsync(filePath, text, Encoding.UTF8);

            // Assert
            var fileContent = await File.ReadAllTextAsync(filePath);
            Assert.IsTrue(fileContent.EndsWith(text));
        }

        [TestMethod]
        public async Task WriteTextToFileAsync_CreatesNewFileAndWritesTextAsync()
        {
            // Arrange
            var text = "New file content";
            var filePath = Path.GetTempFileName();

            // Act
            await FileWriter.WriteTextToFileAsync(filePath, text, Encoding.UTF8);

            // Assert
            var fileContent = await File.ReadAllTextAsync(filePath);
            Assert.AreEqual(text, fileContent);
            File.Delete(filePath);
        }

        [TestMethod]
        public async Task WriteByteArrayToFileAsync_WritesDataToFileAsync()
        {
            // Arrange
            string TestData = "This is some test data";
            var filePath = Path.GetTempFileName();

            byte[] data = Encoding.UTF8.GetBytes(TestData);

            // Act
            await FileWriter.WriteByteArrayToFileAsync(filePath, data);

            // Assert
            Assert.IsTrue(File.Exists(filePath));

            string actualData = await File.ReadAllTextAsync(filePath);

            Assert.AreEqual(TestData, actualData);
            File.Delete(filePath);
        }

        [TestMethod]
        public async Task WriteByteArrayToFileAsync_ThrowsArgumentNullException_WhenPathIsNullAsync()
        {
            // Arrange
            string TestData = "This is some test data";

            byte[] data = Encoding.UTF8.GetBytes(TestData);
            string? nullPath = null;

            // Act & Assert
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await FileWriter.WriteByteArrayToFileAsync(nullPath, data));
        }

        [TestMethod]
        public async Task WriteByteArrayToFileAsync_ThrowsArgumentNullException_WhenPathIsEmptyAsync()
        {
            // Arrange
            string TestData = "This is some test data";

            byte[] data = Encoding.UTF8.GetBytes(TestData);
            string emptyPath = "";

            // Act & Assert
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await FileWriter.WriteByteArrayToFileAsync(emptyPath, data));
        }
    }
}