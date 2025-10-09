using FluentValidation;

namespace HotelBooking.Application.Room.Commands.DeleteRoom
{
    public class DeleteRoomCommandValidator : AbstractValidator<DeleteRoomCommand>
    {
        public DeleteRoomCommandValidator()
        {
            RuleFor(x => x.RoomId)
                .NotEmpty().WithMessage("Room Id cannot be empty");
        }
    }
}
