using SharedKernel.Common;
using System.Net;

namespace HotelBooking.Domain.Exceptions
{
    public class NullablePhoneNumberException : BaseException
    {
        public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

        public NullablePhoneNumberException(string message)
            : base(message) { }

        public NullablePhoneNumberException(string message, Exception innerException)
            : base(message, innerException) { }

        public NullablePhoneNumberException()
            : base("PhoneNumber cannot be null.") { }

        public NullablePhoneNumberException(Exception innerException)
            : base("PhoneNumber cannot be null.", innerException) { }
    }
}
