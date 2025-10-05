using FluentValidation;

namespace HotelBooking.Application.Booking.Commands.AddBooking
{
    public class AddBookingRequestValidator : AbstractValidator<AddBookingRequest>
    {
        public AddBookingRequestValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("User ID is required.");

            RuleFor(x => x.RoomId)
                .NotEmpty().WithMessage("Room ID is required.");

            RuleFor(x => x.CheckInDate)
                .NotEmpty().WithMessage("Check-in date is required.")
                .Must(date => date.Date >= DateTime.UtcNow.Date)
                .WithMessage("Check-in date cannot be in the past.");

            RuleFor(x => x.CheckOutDate)
                .NotEmpty().WithMessage("Check-out date is required.")
                .Must((request, checkOutDate) => checkOutDate > request.CheckInDate)
                .WithMessage("Check-out date must be after check-in date.");
        }
    }
}
