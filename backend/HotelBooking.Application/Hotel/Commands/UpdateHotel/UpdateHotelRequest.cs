namespace HotelBooking.Application.Hotel.Commands.UpdateHotel
{
    public class UpdateHotelRequest
    {
        public string? Name { get; set; } = null!;
        public string? Country { get; set; } = null!;
        public string? City { get; set; } = null!;
        public string? Street { get; set; } = null!;
        public string? Building { get; set; } = null!;
        public string? ZipCode { get; set; }
        public string? Description { get; set; } = null!;
    }
}
