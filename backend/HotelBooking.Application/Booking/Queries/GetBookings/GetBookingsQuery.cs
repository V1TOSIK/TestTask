using HotelBooking.Application.Dtos;
using MediatR;
using SharedKernel.Common;
using SharedKernel.Pagination;

namespace HotelBooking.Application.Booking.Queries.GetBookings
{
    public class GetBookingsQuery : PaginationRequest, IRequest<Result<PaginationResponse<BookingDto>>>
    {
    }
}
