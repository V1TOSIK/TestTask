using FluentValidation;
using SharedKernel.Pagination;

namespace HotelBooking.Application.Room.Queries.GetRooms
{
    public class GetRoomsQueryValidator : AbstractValidator<GetRoomsQuery>
    {
        public GetRoomsQueryValidator()
        {
            Include(new PaginationRequestValidator());

            RuleForEach(x => x.Cities)
                .NotEmpty().WithMessage("City names cannot be empty.")
                .MaximumLength(100).WithMessage("City names cannot exceed 100 characters.");

            RuleFor(x => x.CheckInDate)
                .Must(date => date == null || date.Value.Date >= DateTime.UtcNow.Date)
                .WithMessage("Check-in date cannot be in the past.");
        }
    }
}
