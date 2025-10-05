using FluentValidation;

namespace HotelBooking.Application.Booking.Queries.GetMyBookings
{
    public class GetMyBookingsQueryValidator : AbstractValidator<GetMyBookingsQuery>
    {
        public GetMyBookingsQueryValidator()
        {
            Include(new SharedKernel.Pagination.PaginationRequestValidator());

            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("User ID is required.");
        }
    }
}
