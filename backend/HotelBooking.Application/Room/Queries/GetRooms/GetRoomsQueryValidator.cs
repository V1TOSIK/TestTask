using FluentValidation;
using SharedKernel.Pagination;

namespace HotelBooking.Application.Room.Queries.GetRooms
{
    public class GetRoomsQueryValidator : AbstractValidator<GetRoomsQuery>
    {
        public GetRoomsQueryValidator()
        {
            Include(new PaginationRequestValidator());

            When(x => x.Cities != null && x.Cities.Any(), () =>
            {
                RuleForEach(x => x.Cities)
                    .NotEmpty().WithMessage("City names cannot be empty.")
                    .MaximumLength(100).WithMessage("City names cannot exceed 100 characters.");
            });

            When(x => x.CheckInDate != null, () =>
            {
                RuleFor(x => x.CheckInDate)
                    .Must(date => date == null || date.Value.Date >= DateTime.UtcNow.Date)
                    .WithMessage("Check-in date cannot be in the past.");

            });
        }
    }
}
