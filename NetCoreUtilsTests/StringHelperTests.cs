namespace NetCoreUtils.Tests
{
    [TestClass()]
    public class StringHelperTests
    {
        [TestMethod]
        public void RemoveDiacritics_Removes_Accents()
        {
            // Arrange
            var input = "áéÍóúñÑç";
            var expectedOutput = "aeIounNc";

            // Act
            var output = input.RemoveDiacritics();

            // Assert
            Assert.AreEqual(expectedOutput, output);
        }

        [TestMethod]
        public void RemoveDiacritics_Handles_String_Without_Diacritics()
        {
            // Arrange
            var input = "This string has no accents";

            // Act
            var output = input.RemoveDiacritics();

            // Assert
            Assert.AreEqual(input, output);
        }

        [TestMethod]
        public void FirstCharToUpper_Throws_On_Null_Input()
        {
            // Arrange
            string nullInput = null;

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => nullInput.FirstCharToUpper());
        }

        [TestMethod]
        public void FirstCharToUpper_Throws_On_Empty_Input()
        {
            // Arrange
            string? emptyInput = "";

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => emptyInput.FirstCharToUpper());
        }

        [TestMethod]
        public void FirstCharToUpper_Capitalizes_First_Letter()
        {
            // Arrange
            var input = "hello world";
            var expectedOutput = "Hello world";

            // Act
            var output = input.FirstCharToUpper();

            // Assert
            Assert.AreEqual(expectedOutput, output);
        }

        [TestMethod]
        public void FirstCharToUpper_Handles_String_Already_Capitalized()
        {
            // Arrange
            var input = "Hello world";

            // Act
            var output = input.FirstCharToUpper();

            // Assert
            Assert.AreEqual(input, output);
        }
    }
}