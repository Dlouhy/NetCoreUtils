namespace NetCoreUtils.Tests
{
    [TestClass()]
    public class IPAddressHelperTests
    {
        [TestMethod]
        public void ConvertFromIntegerToIPAddress_ValidInput_ReturnsCorrectString()
        {
            // Arrange
            uint testIPAddress = 0x01020304; // Represents IP 1.2.3.4
            string expectedIpAddress = "1.2.3.4";

            // Act
            string actualResult = testIPAddress.ConvertFromIntegerToIPAddress();

            // Assert
            Assert.AreEqual(expectedIpAddress, actualResult);
        }

        [TestMethod]
        public void ConvertFromIPAddressToInteger_ValidInput_ReturnsCorrectValue()
        {
            // Arrange
            string validIPAddress = "192.168.1.1";
            uint expectedIPAddress = 3232235777;

            // Act
            uint actualResult = validIPAddress.ConvertFromIPAddressToInteger();

            // Assert
            Assert.AreEqual(expectedIPAddress, actualResult);
        }

        [TestMethod]
        public void ConvertFromIPAddressToInteger_NullInput_ThrowsArgumentNullException()
        {
            // Arrange (null input)
            string? nullInput = null;

            // Act (expecting exception)
            Assert.ThrowsException<ArgumentNullException>(() => nullInput.ConvertFromIPAddressToInteger());

            // Assert (handled by ThrowsException method)
        }

        [TestMethod]
        public void ConvertFromIPAddressToInteger_InvalidInput_ThrowsFormatException()
        {
            // Arrange (invalid input format)
            string invalidIPAddress = "invalid";

            // Act (expecting exception)
            Assert.ThrowsException<FormatException>(() => invalidIPAddress.ConvertFromIPAddressToInteger());

            // Assert (handled by ThrowsException method)
        }
    }
}