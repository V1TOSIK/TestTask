using HotelBooking.Domain.Exceptions;
using SharedKernel.Common;
using System.Text.RegularExpressions;

namespace HotelBooking.Domain.ValueObjects
{
    public class PhoneNumber : ValueObject
    {
        public string Value { get; init; }

        private static readonly Regex phoneRegex = new(
            @"^\+?\d{10,15}$",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

        private PhoneNumber() { }

        public PhoneNumber(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new NullablePhoneNumberException("phone cannot be empty.");
            if (!phoneRegex.IsMatch(value))
                throw new InvalidPhoneNumberFormatException("Invalid phone format.");
            Value = value;
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString() => Value;

        public static implicit operator string(PhoneNumber email) => email.Value;
        public static explicit operator PhoneNumber(string value) => new PhoneNumber(value);
    }
}
