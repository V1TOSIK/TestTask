using MediatR;
using SharedKernel.Common;

namespace HotelBooking.Application.Booking.Commands.AddBooking
{
    public class AddBookingCommand : IRequest<Result<Guid>>
    {
        public AddBookingCommand(AddBookingRequest request)
        {
            Request = request;
        }
        public AddBookingRequest Request { get; set; } = null!;
    }
}
