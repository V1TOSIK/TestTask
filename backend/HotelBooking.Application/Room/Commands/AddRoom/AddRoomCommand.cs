using MediatR;
using SharedKernel.Common;

namespace HotelBooking.Application.Room.Commands.AddRoom
{
    public class AddRoomCommand : IRequest<Result<Guid?>>
    {
        public Guid HotelId { get; set; }
        public int Number { get; set; }
        public int Capacity { get; set; }
        public decimal PricePerNight { get; set; }
    }
}
