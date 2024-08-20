namespace NetCoreUtils.FileUtilities
{
    [TestClass()]
    public class FilesHelperTests
    {
        [TestMethod]
        public void RemoveInvalidFilenameCharacters_Should_Remove_Invalid_Characters()
        {
            // Arrange
            string fileName = "te#st.txt";
            string expectedResult = "test.txt";

            // Act
            string actualResult = fileName.RemoveInvalidFilenameCharacters();

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void RemoveInvalidFilenameCharacters_Should_ReturnEmpty_When_InputIsEmpty()
        {
            string fileName = "";
            string expectedResult = "";

            string actualResult = fileName.RemoveInvalidFilenameCharacters();

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void RemoveInvalidFilenameCharacters_Should_RemoveInvalidChars_When_InputContainsInvalidChars()
        {
            string fileName = "te#st.txt";
            string expectedResult = "test.txt";

            string actualResult = fileName.RemoveInvalidFilenameCharacters();

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void RemoveInvalidFilenameCharacters_Should_NotModify_When_InputHasNoInvalidChars()
        {
            string fileName = "valid_file_name.txt";
            string expectedResult = fileName;

            string actualResult = fileName.RemoveInvalidFilenameCharacters();

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void HasFileNameInvalidChars_Should_ReturnTrue_When_InputContainsInvalidChars()
        {
            string fileName = "file*.txt";
            bool expectedResult = true;

            bool actualResult = fileName.HasFileNameInvalidChars();

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void HasFileNameInvalidChars_Should_ReturnFalse_When_InputHasNoInvalidChars()
        {
            string validFileName = "validfilename.txt";
            bool expectedResult = false;

            bool actualResult = validFileName.HasFileNameInvalidChars();

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void HasFileNameInvalidChars_Should_ReturnFalse_When_InputIsEmpty()
        {
            string emptyFileName = "";
            bool expectedResult = false;

            bool actualResult = emptyFileName.HasFileNameInvalidChars();

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void HasFileNameInvalidChars_Should_ReturnFalse_When_InputIsNull()
        {
            string? nullFileName = null;
            bool expectedResult = false;

            bool actualResult = nullFileName.HasFileNameInvalidChars();

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void GetFileSizeInBytes_Should_ReturnZero_When_FileDoesNotExist()
        {
            string filePath = "ThisFileDoesNotExist.txt";
            long expectedResult = 0;

            long actualResult = filePath.GetFileSizeInBytes();

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void FileSizeWithBytesAbbreviation_Should_ThrowArgumentNullException_When_InputIsNull()
        {
            string? filePath = null;

            Assert.ThrowsException<ArgumentNullException>(() => filePath.FileSizeWithBytesAbbreviation());
        }

        [TestMethod]
        public void FileSizeWithBytesAbbreviation_Should_Throw_On_Empty_Path()
        {
            // Arrange
            string emptyPath = "";

            // Act & Assert

            Assert.ThrowsException<ArgumentNullException>(() => emptyPath.FileSizeWithBytesAbbreviation());
        }
    }
}