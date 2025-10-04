using FluentValidation;
using SharedKernel.Pagination;

namespace HotelBooking.Application.Hotel.Queries.GetHotels
{
    public class GetHotelsQueryValidator : AbstractValidator<GetHotelsQuery>
    {
        public GetHotelsQueryValidator()
        {
            Include(new PaginationRequestValidator());

            RuleForEach(x => x.Cities)
                .NotEmpty().WithMessage("City names cannot be empty.")
                .MaximumLength(100).WithMessage("City names cannot exceed 100 characters.");
        }
    }
}
