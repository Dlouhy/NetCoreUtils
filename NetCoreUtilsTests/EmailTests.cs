using System.Text;

namespace NetCoreUtils.Tests
{
    [TestClass]
    public class EmailTests
    {
        [TestMethod]
        public void Create_EmptyEmail_ReturnsFailure()
        {
            // Arrange (Set up test data)
            string emailValue = "";

            // Act (Call the method to test)
            var result = Email.Create(emailValue);

            // Assert (Verify the expected outcome)
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Email can not be empty.", result.Error);
        }

        [TestMethod]
        public void Create_ValidEmail_ReturnsSuccess()
        {
            // Arrange
            string emailValue = "john.doe@example.com";

            // Act
            var result = Email.Create(emailValue);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(emailValue, result.Value.EmailAddress);
        }

        [TestMethod]
        public void Create_EmailExceedingLength_ReturnsFailure()
        {
            // Arrange
            StringBuilder longEmail = new StringBuilder();
            for (int i = 0; i < 255; i++)
            {
                longEmail.Append('a');
            }
            longEmail.Append("@example.com");

            // Act
            var result = Email.Create(longEmail.ToString());

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Email has more than 254 characters.", result.Error);
        }

        [TestMethod]
        public void Create_InvalidEmailFormat_ReturnsFailure()
        {
            // Arrange
            var invalidEmail = "invalidemail";

            // Act
            var result = Email.Create(invalidEmail.ToString());

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Invalid email format.", result.Error);
        }

        [TestMethod]
        public void Initializes_Correctly_With_Valid_Email()
        {
            // Arrange
            string email = "john.doe@example.com";
            string expectedUserName = "john.doe";
            string expectedDomain = "example.com";

            // Act
            var newEmail = Email.Create(email);

            // Assert
            Assert.AreEqual(email, newEmail.Value.EmailAddress);
            Assert.AreEqual(expectedUserName, newEmail.Value.UserName);
            Assert.AreEqual(expectedDomain, newEmail.Value.Domain);
        }

        [TestMethod]
        public void Implicit_Conversion_Returns_Email_Value()
        {
            string emailValue = "jane.doe@email.com";

            // Arrange
            var email = Email.Create(emailValue);

            // Act
            string emailString = email.Value.EmailAddress;

            // Assert
            Assert.AreEqual(emailValue, emailString);
        }

        [TestMethod]
        public void Equals_Compares_Email_Value()
        {
            // Arrange
            var email1 = Email.Create("john.doe@example.com");
            var email2 = Email.Create("john.doe@example.com");

            // Act & Assert
            Assert.IsTrue(email1.Value.Equals(email2.Value));
            Assert.AreEqual(email1.Value.GetHashCode(), email2.Value.GetHashCode());
        }

        [TestMethod]
        public void Equals_Returns_False_For_Different_Emails()
        {
            // Arrange
            var email1 = Email.Create("john.doe@example.com");
            var email2 = Email.Create("jane.doe@example.com");

            // Act & Assert
            Assert.IsFalse(email1.Value.Equals(email2.Value));
        }
    }
}