using FluentValidation;

namespace HotelBooking.Application.Hotel.Commands.AddHotel
{
    public class AddHotelCommandValidator : AbstractValidator<AddHotelCommand>
    {
        public AddHotelCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Hotel name is required.")
                .MaximumLength(100).WithMessage("Hotel name must be less than 100 characters.");

            RuleFor(x => x.Country)
                .NotEmpty().WithMessage("Country is required.")
                .MaximumLength(100).WithMessage("Country name must be less than 100 characters.");

            RuleFor(x => x.City)
                .NotEmpty().WithMessage("City is required.")
                .MaximumLength(100).WithMessage("City name must be less than 100 characters.");

            RuleFor(x => x.Street)
                .NotEmpty().WithMessage("Street is required.")
                .MaximumLength(100).WithMessage("Street name must be less than 100 characters.");

            RuleFor(x => x.Building)
                .NotEmpty().WithMessage("Building number is required.")
                .MaximumLength(20).WithMessage("Building number must be less than 20 characters.");

            RuleFor(x => x.ZipCode)
                .MaximumLength(20).WithMessage("Zip code must be less than 20 characters.")
                .Matches(@"^\d{4,10}$").When(x => !string.IsNullOrWhiteSpace(x.ZipCode))
                .WithMessage("Zip code must be numeric and 4–10 digits long.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(1000).WithMessage("Description must be less than 1000 characters.");
        }
    }
}
