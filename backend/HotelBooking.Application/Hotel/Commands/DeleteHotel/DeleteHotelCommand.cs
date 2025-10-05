using MediatR;
using SharedKernel.Common;

namespace HotelBooking.Application.Hotel.Commands.DeleteHotel
{
    public class DeleteHotelCommand : IRequest<Result>
    {
        public Guid HotelId { get; set; }
    
        public DeleteHotelCommand(Guid hotelId)
        {
            HotelId = hotelId;
        }
    }
}
