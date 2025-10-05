using FluentValidation;

namespace HotelBooking.Application.Hotel.Commands.DeleteHotel
{
    public class DeleteHotelCommandValidator : AbstractValidator<DeleteHotelCommand>
    {
        public DeleteHotelCommandValidator()
        {
            RuleFor(x => x.HotelId)
                .NotEmpty().WithMessage("Hotel Id cannot be empty");
        }
    }
}
