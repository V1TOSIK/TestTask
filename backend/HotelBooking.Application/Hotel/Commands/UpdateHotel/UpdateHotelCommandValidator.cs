using FluentValidation;

namespace HotelBooking.Application.Hotel.Commands.UpdateHotel
{
    public class UpdateHotelCommandValidator : AbstractValidator<UpdateHotelCommand>
    {
        public UpdateHotelCommandValidator()
        {
            RuleFor(x => x.HotelId)
                .NotEmpty().WithMessage("Hotel Id cannot be empty");

            RuleFor(x => x.Request)
                .NotNull().WithMessage("Request cannot be null")
                .SetValidator(new UpdateHotelRequestValidator());
        }
    }
}
