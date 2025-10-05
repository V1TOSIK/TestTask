using HotelBooking.Domain.ValueObjects;
using SharedKernel.Common;

namespace HotelBooking.Domain.Entities
{
    public class Hotel : Entity<Guid>
    {
        private Hotel() { }

        private Hotel(string name, Address address, string description)
        {
            Id = Guid.NewGuid();
            Name = name;
            Address = address;
            Description = description;
        }

        public string Name { get; private set; } = null!;
        public Address Address { get; private set; } = null!;
        public string Description { get; private set; } = null!;

        private List<Room> _rooms = new();
        public IReadOnlyCollection<Room> Rooms => _rooms.AsReadOnly();

        public static Hotel Create(string name, Address address, string description)
        {
            return new Hotel(name, address, description);
        }

        public Result AddRoom(Room room)
        {
            var existRoom = _rooms.FirstOrDefault(r => r.Number == room.Number);
            if (existRoom != null)
                return Result.Failure($"Room with Number: {room.Number} already exist");

            _rooms.Add(room);
            return Result.Success();
        }

        public void RemoveRoom(Room room)
        {
            _rooms.Remove(room);
        }

        public Result UpdateDetails(string name, Address address, string description)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Result.Failure("Hotel name cannot be empty");
            if (string.IsNullOrWhiteSpace(description))
                return Result.Failure("Description cannot be empty");

            Name = name;
            Address = address;
            Description = description;

            return Result.Success();
        }
    }
}
