using HotelBooking.Application.Dtos;
using HotelBooking.Application.Interfaces.Repositories;
using MediatR;
using SharedKernel.Common;
using SharedKernel.Pagination;

namespace HotelBooking.Application.Booking.Queries.GetMyBookings
{
    public class GetMyBookingsQueryHandler : IRequestHandler<GetMyBookingsQuery, Result<PaginationResponse<BookingDto>>>
    {
        private readonly IBookingRepository _bookingRepository;
        public GetMyBookingsQueryHandler(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<Result<PaginationResponse<BookingDto>>> Handle(GetMyBookingsQuery query, CancellationToken cancellationToken)
        {
            var paginatedBookings = await _bookingRepository.GetPaginatedBookingsByUserIdAsync(query.UserId, query.PageNumber, query.PageSize, cancellationToken);

            return Result<PaginationResponse<BookingDto>>.Success(paginatedBookings);
        }
    }
}
