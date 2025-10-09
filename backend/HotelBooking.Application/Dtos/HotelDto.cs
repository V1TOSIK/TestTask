namespace HotelBooking.Application.Dtos
{
    public class HotelDto
    {
        public HotelDto(Guid id,
            string name,
            string address,
            string city,
            string country)
        {
            Id = id;
            Name = name;
            Address = address;
            City = city;
            Country = country;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
