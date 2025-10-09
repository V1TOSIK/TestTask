using FluentValidation;

namespace HotelBooking.Application.Room.Commands.UpdateRoom
{
    public class UpdateRoomRequestValidator : AbstractValidator<UpdateRoomRequest>
    {
        public UpdateRoomRequestValidator()
        {
            RuleFor(x => x.Capacity)
                .GreaterThan(0).WithMessage("Room capacity must be greater than 0.");

            RuleFor(x => x.PricePerNight)
                .GreaterThanOrEqualTo(0).WithMessage("Price per night cannot be negative.");
        }
    }
}
