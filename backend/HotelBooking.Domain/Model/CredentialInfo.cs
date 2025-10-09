using HotelBooking.Domain.Enums;

namespace HotelBooking.Domain.Model
{
    public class CredentialInfo
    {
        public CredentialType Type { get; init; }
        public string Value { get; init; }

        public CredentialInfo(CredentialType type, string value)
        {
            Type = type;
            Value = value;
        }
    }
}
