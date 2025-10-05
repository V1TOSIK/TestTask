using MediatR;
using SharedKernel.Common;

namespace HotelBooking.Application.Hotel.Commands.UpdateHotel
{
    public class UpdateHotelCommand : IRequest<Result>
    {
        public Guid HotelId { get; set; }
        public UpdateHotelRequest Request { get; set; }

        public UpdateHotelCommand(Guid hotelId, UpdateHotelRequest request)
        {
            HotelId = hotelId;
            Request = request;
        }
    }
}
