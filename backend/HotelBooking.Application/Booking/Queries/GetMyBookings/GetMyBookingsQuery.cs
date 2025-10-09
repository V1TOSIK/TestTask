using HotelBooking.Application.Dtos;
using MediatR;
using SharedKernel.Common;
using SharedKernel.Pagination;

namespace HotelBooking.Application.Booking.Queries.GetMyBookings
{
    public class GetMyBookingsQuery : PaginationRequest, IRequest<Result<PaginationResponse<BookingDto>>>
    {
        public GetMyBookingsQuery(Guid userId)
        {
            UserId = userId;
        }
        public Guid UserId { get; set; }
    }
}
