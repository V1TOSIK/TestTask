using MediatR;
using SharedKernel.Common;

namespace HotelBooking.Application.Room.Commands.DeleteRoom
{
    public class DeleteRoomCommand : IRequest<Result>
    {
        public Guid RoomId { get; set; }
        public DeleteRoomCommand(Guid roomId)
        {
            RoomId = roomId;
        }
    }
}
