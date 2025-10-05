using FluentValidation;

namespace HotelBooking.Application.Hotel.Commands.UpdateHotel
{
    public class UpdateHotelRequestValidator : AbstractValidator<UpdateHotelRequest>
    {
        public UpdateHotelRequestValidator()
        {
            When(x => !string.IsNullOrWhiteSpace(x.Name), () =>
            {
                RuleFor(x => x.Name)
                    .MaximumLength(200).WithMessage("Hotel name must be shorter than 200 characters");
            });

            When(x => !string.IsNullOrWhiteSpace(x.Country), () =>
            {
                RuleFor(x => x.Country)
                    .MaximumLength(100).WithMessage("Country must be shorter than 100 characters");
            });

            When(x => !string.IsNullOrWhiteSpace(x.City), () =>
            {
                RuleFor(x => x.City)
                    .MaximumLength(100).WithMessage("City must be shorter than 100 characters");
            });

            When(x => !string.IsNullOrWhiteSpace(x.Street), () =>
            {
                RuleFor(x => x.Street)
                    .MaximumLength(100).WithMessage("Street must be shorter than 100 characters");
            });

            When(x => !string.IsNullOrWhiteSpace(x.Building), () =>
            {
                RuleFor(x => x.Building)
                    .MaximumLength(20).WithMessage("Building must be shorter than 20 characters");
            });

            When(x => !string.IsNullOrWhiteSpace(x.ZipCode), () =>
            {
                RuleFor(x => x.ZipCode)
                    .MaximumLength(20).WithMessage("Zip code must be shorter than 20 characters")
                    .Matches(@"^\d{4,10}$").WithMessage("Zip code must be numeric and 4–10 digits long");
            });

            When(x => !string.IsNullOrWhiteSpace(x.Description), () =>
            {
                RuleFor(x => x.Description)
                    .MaximumLength(1000).WithMessage("Hotel description must be shorter than 1000 characters");
            });
        }
    }
}
