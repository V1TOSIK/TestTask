namespace HotelBooking.Application.Dtos
{
    public class RoomDto
    {
        public RoomDto(Guid id, string hotelName, int number, int capacity, decimal pricePerNight)
        {
            Id = id;
            Number = number;
            Capacity = capacity;
            PricePerNight = pricePerNight;
            HotelName = hotelName;
        }
        public Guid Id { get; set; }
        public string HotelName { get; set; }
        public int Number { get; set; }
        public int Capacity { get; set; }
        public decimal PricePerNight { get; set; }

    }
}
