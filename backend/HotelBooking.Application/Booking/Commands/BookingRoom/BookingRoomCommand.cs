using MediatR;
using SharedKernel.Common;

namespace HotelBooking.Application.Booking.Commands.BookingRoom
{
    public class BookingRoomCommand : IRequest<Result<long>>
    {
        public BookingRoomCommand(BookingRoomRequest request)
        {
            Request = request;
        }
        public BookingRoomRequest Request { get; set; } = null!;
    }
}
