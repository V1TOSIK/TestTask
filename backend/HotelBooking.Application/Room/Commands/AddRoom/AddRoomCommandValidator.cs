using FluentValidation;

namespace HotelBooking.Application.Room.Commands.AddRoom
{
    public class AddRoomCommandValidator : AbstractValidator<AddRoomCommand>
    {
        public AddRoomCommandValidator()
        {
            RuleFor(x => x.HotelId)
                .NotEmpty().WithMessage("HotelId is required.");

            RuleFor(x => x.Number)
                .GreaterThan(0).WithMessage("Room number must be greater than 0.");

            RuleFor(x => x.Capacity)
                .GreaterThan(0).WithMessage("Room capacity must be greater than 0.");

            RuleFor(x => x.PricePerNight)
                .GreaterThanOrEqualTo(0).WithMessage("Price per night cannot be negative.");
        }
    }
}
