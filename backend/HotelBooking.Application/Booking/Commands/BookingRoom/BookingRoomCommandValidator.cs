using FluentValidation;

namespace HotelBooking.Application.Booking.Commands.BookingRoom
{
    public class BookingRoomCommandValidator : AbstractValidator<BookingRoomCommand>
    {
        public BookingRoomCommandValidator()
        {
            RuleFor(x => x.Request)
                .NotEmpty().WithMessage("At least one booking request is required.")
                .SetValidator(new BookingRoomRequestValidator());
        }
    }
}
