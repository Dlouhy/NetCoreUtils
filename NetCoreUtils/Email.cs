using CSharpFunctionalExtensions;
using System.Text.RegularExpressions;

namespace NetCoreUtils
{
    /// <summary>
    /// This class represents an email value object and provides various methods for manipulating
    /// and validating email addresses.
    /// </summary>
    public sealed class Email : ValueObject
    {
        private const string EmailFormat = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";

        /// <summary>
        /// Initializes a new instance of the <see cref="Email"/> class
        /// </summary>
        /// <param name="value">The value of email address</param>
        private Email(string value)
        {
            EmailAddress = value;
            UserName = value.Split('@')[0];
            Domain = value.Split('@')[1];
        }

        /// <summary>
        /// Creates an Email object by validating the provided email address.
        /// </summary>
        /// <param name="emailValue">
        /// The email address to be validated and used for creating the Email object.
        /// </param>
        /// <returns>
        /// A Result object.
        /// - Success: Contains the created Email object if the email address is valid.
        /// - Failure: Contains an error message explaining the validation failure.
        /// </returns>
        public static Result<Email> Create(string emailValue)
        {
            if (string.IsNullOrWhiteSpace(emailValue))
                return Result.Failure<Email>("Email can not be empty.");

            if (emailValue.Length > 254)
                return Result.Failure<Email>("Email has more than 254 characters.");

            if (!Regex.IsMatch(emailValue, EmailFormat, RegexOptions.IgnoreCase))
                return Result.Failure<Email>("Invalid email format.");

            return Result.Success(new Email(emailValue));
        }

        /// <summary>
        /// Gets the entire email address as a string
        /// </summary>
        public string EmailAddress { get; }

        /// <summary>
        /// Gets the domain name of email
        /// </summary>
        public string Domain { get; }

        /// <summary>
        /// Gets the user name of email
        /// </summary>
        public string UserName { get; }

        public override string ToString()
        {
            return EmailAddress;
        }

        protected override IEnumerable<IComparable> GetEqualityComponents()
        {
            yield return EmailAddress;
        }
    }
}