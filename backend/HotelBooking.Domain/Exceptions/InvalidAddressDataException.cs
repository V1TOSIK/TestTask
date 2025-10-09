using SharedKernel.Common;
using System.Net;

namespace HotelBooking.Domain.Exceptions
{
    public class InvalidAddressDataException : BaseException
    {
        public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

        public InvalidAddressDataException(string message)
            : base(message) { }

        public InvalidAddressDataException(string message, Exception innerException)
            : base(message, innerException) { }

        public InvalidAddressDataException()
            : base("Invalid address data.") { }

        public InvalidAddressDataException(Exception innerException)
            : base("Invalid address data.", innerException) { }
    }
}
