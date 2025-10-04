using SharedKernel.Common;

namespace HotelBooking.Domain.Entities
{
    public class Room : Entity<Guid>
    {
        private Room() { }

        private Room(int number, int capacity, decimal pricePerNight, Guid hotelId)
        {
            Id = Guid.NewGuid();
            Number = number;
            Capacity = capacity;
            PricePerNight = pricePerNight;
            HotelId = hotelId;
        }
        public int Number { get; private set; }
        public int Capacity { get; private set; }
        public decimal PricePerNight { get; private set; }

        public Guid HotelId { get; private set; }
        public Hotel Hotel { get; private set; } = null!;

        public static Room Create(int number, int capacity, decimal pricePerNight, Guid hotelId)
        {
            return new Room(number, capacity, pricePerNight, hotelId);
        }

        public Result UpdateDetails(int number, int capacity, decimal pricePerNight)
        {
            if (number <= 0)
                Result.Failure("Room number must be greater than zero");
            if (capacity <= 0)
                Result.Failure("Room capacity must be greater than zero");
            if (pricePerNight < 0)
                Result.Failure("Price per night cannot be negative");

            Number = number;
            Capacity = capacity;
            PricePerNight = pricePerNight;

            return Result.Success();
        }
    }
}
