using SharedKernel.Common;

namespace HotelBooking.Domain.Entities
{
    public class Booking : Entity<long>
    {
        private Booking() { }
        public Booking(Guid userId, Guid roomId, DateTime checkInDate, DateTime checkOutDate)
        {
            UserId = userId;
            RoomId = roomId;
            CheckInDate = checkInDate;
            CheckOutDate = checkOutDate;
            CreatedAt = DateTime.UtcNow;
        }

        public Guid UserId { get; init; }
        public Guid RoomId { get; init; }
        public DateTime CheckInDate { get; init; }
        public DateTime CheckOutDate { get; init; }
        public DateTime CreatedAt { get; init; }
        public DateTime? CancelledAt { get; private set; }

        public static Booking Create(Guid userId, Guid roomId, DateTime checkInDate, DateTime checkOutDate)
        {
            return new Booking(userId, roomId, checkInDate, checkOutDate);
        }

        public Result Cancel()
        {
            if (CancelledAt != null)
                return Result.Failure("Booking is already cancelled");
            CancelledAt = DateTime.UtcNow;
            return Result.Success();
        }
    }
}
