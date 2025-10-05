using HotelBooking.Application.Dtos;
using HotelBooking.Application.Interfaces.Repositories;
using MediatR;
using SharedKernel.Common;
using SharedKernel.Pagination;

namespace HotelBooking.Application.Booking.Queries.GetBookings
{
    public class GetBookingsQueryHandler : IRequestHandler<GetBookingsQuery, Result<PaginationResponse<BookingDto>>>
    {
        private readonly IBookingRepository _bookingRepository;
        public GetBookingsQueryHandler(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<Result<PaginationResponse<BookingDto>>> Handle(GetBookingsQuery query, CancellationToken cancellationToken)
        {
            var paginatedBookings = await _bookingRepository.GetPaginatedBookingsAsync(query.PageNumber, query.PageSize, cancellationToken);

            return Result<PaginationResponse<BookingDto>>.Success(paginatedBookings);
        }
    }
}
