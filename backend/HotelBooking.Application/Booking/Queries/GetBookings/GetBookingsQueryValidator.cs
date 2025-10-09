using FluentValidation;
using SharedKernel.Pagination;

namespace HotelBooking.Application.Booking.Queries.GetBookings
{
    public class GetBookingsQueryValidator : AbstractValidator<GetBookingsQuery>
    {
        public GetBookingsQueryValidator()
        {
            Include(new PaginationRequestValidator());
        }
    }
}
