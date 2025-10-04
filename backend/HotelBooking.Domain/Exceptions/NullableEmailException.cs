using SharedKernel.Common;
using System.Net;

namespace HotelBooking.Domain.Exceptions
{
    public class NullableEmailException : BaseException
    {
        public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

        public NullableEmailException(string message)
            : base(message) { }

        public NullableEmailException(string message, Exception innerException)
            : base(message, innerException) { }

        public NullableEmailException()
            : base("Email cannot be null.") { }

        public NullableEmailException(Exception innerException)
            : base("Email cannot be null.", innerException) { }
    }
}
