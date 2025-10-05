using MediatR;
using SharedKernel.Common;

namespace HotelBooking.Application.Room.Commands.UpdateRoom
{
    public class UpdateRoomCommand : IRequest<Result>
    {
        public Guid RoomId { get; set; }
        public UpdateRoomRequest Request { get; set; }

        public UpdateRoomCommand(Guid roomId, UpdateRoomRequest request)
        {
            RoomId = roomId;
            Request = request;
        }
    }
}
