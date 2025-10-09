using FluentValidation;

namespace HotelBooking.Application.Room.Commands.UpdateRoom
{
    public class UpdateRoomCommandValidator : AbstractValidator<UpdateRoomCommand>
    {
        public UpdateRoomCommandValidator()
        {
            RuleFor(x => x.RoomId)
                .NotEmpty().WithMessage("Room ID is required.");

            RuleFor(x => x.Request)
                .NotNull().WithMessage("UpdateRoomRequest cannot be null")
                .SetValidator(new UpdateRoomRequestValidator());
        }
    }
}
