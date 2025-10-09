using HotelBooking.Domain.Exceptions;
using SharedKernel.Common;
using System.Text.RegularExpressions;

namespace HotelBooking.Domain.ValueObjects
{
    public class Email : ValueObject
    {
        public string Value { get; init; }

        private static readonly Regex emailRegex = new(
            @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

        private Email() { }

        public Email(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
               throw new NullableEmailException("Email cannot be empty.");
            if (!emailRegex.IsMatch(value))
                throw new InvalidEmailFormatException("Invalid email format.");
            Value = value;
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString() => Value;

        public static implicit operator string(Email email) => email.Value;
        public static explicit operator Email(string value) => new Email(value);
    }
}
