using MediatR;
using SharedKernel.Common;

namespace HotelBooking.Application.Hotel.Commands.AddHotel
{
    public class AddHotelCommand : IRequest<Result<Guid>>
    {
        public string Name { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Street { get; set; } = null!;
        public string Building { get; set; } = null!;
        public string? ZipCode { get; set; }
        public string Description { get; set; } = null!;

    }
}
