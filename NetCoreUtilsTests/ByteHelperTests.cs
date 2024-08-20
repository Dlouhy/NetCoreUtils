namespace NetCoreUtils.Tests
{
    [TestClass()]
    public class ByteHelperTests
    {
        [TestMethod]
        public void ThrowsArgumentNullException_WhenJaggedArrayIsNull()
        {
            // Arrange
            byte[][]? jaggedArray = null;

            // Act
            Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await ByteHelper.JaggedArrayToSingleArrayAsync(jaggedArray));

            // Assert (exception thrown is verified by Assert.ThrowsException)
        }

        [TestMethod]
        public void ThrowsArgumentNullException_WhenJaggedArrayContainsNullSubArray()
        {
            // Arrange
            byte[][] jaggedArray = new byte[][] { new byte[] { 1, 2, 3 }, null, new byte[] { 4, 5 } };

            // Act
            Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await ByteHelper.JaggedArrayToSingleArrayAsync(jaggedArray));

            // Assert (exception thrown is verified by Assert.ThrowsException)
        }

        [TestMethod]
        public async Task ReturnsSingleArray_WithCombinedElementsAsync()
        {
            // Arrange
            byte[][] jaggedArray = new byte[][] { new byte[] { 1, 2, 3 }, new byte[] { 4, 5, 6 } };
            byte[] expectedResult = new byte[] { 1, 2, 3, 4, 5, 6 };

            // Act
            byte[] result = await ByteHelper.JaggedArrayToSingleArrayAsync(jaggedArray);

            // Assert
            CollectionAssert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void ThrowsArgumentNullException_WhenStringIsNull()
        {
            // Arrange (nothing to arrange)
            string? hex = null;

            // Act
            Assert.ThrowsException<ArgumentNullException>(() => hex.HexStringToByteArray());

            // Assert (exception thrown is verified by Assert.ThrowsException)
        }

        [TestMethod]
        public void ThrowsArgumentNullException_WhenStringIsEmpty()
        {
            // Arrange
            string hex = "";

            // Act
            Assert.ThrowsException<ArgumentNullException>(() => hex.HexStringToByteArray());

            // Assert (exception thrown is verified by Assert.ThrowsException)
        }

        [TestMethod]
        public void ThrowsFormatException_WhenStringHasOddLength()
        {
            // Arrange
            string hex = "F";

            // Act
            Assert.ThrowsException<FormatException>(() => hex.HexStringToByteArray());

            // Assert (exception thrown is verified by Assert.ThrowsException)
        }

        [TestMethod]
        public void ConvertsValidHexString_ToByteArray()
        {
            // Arrange
            string hex = "FF0A1B";
            byte[] expectedResult = new byte[] { 255, 10, 27 };

            // Act
            byte[] result = hex.HexStringToByteArray();

            // Assert
            CollectionAssert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void ConvertsHexStringWithUpperCase_ToByteArray()
        {
            // Arrange
            string hex = "FFAA3C";
            byte[] expectedResult = new byte[] { 255, 170, 60 };

            // Act
            byte[] result = hex.HexStringToByteArray();

            // Assert
            CollectionAssert.AreEqual(expectedResult, result);
        }
    }
}