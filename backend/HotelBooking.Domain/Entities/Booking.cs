using SharedKernel.Common;

namespace HotelBooking.Domain.Entities
{
    public class Booking : Entity<long>
    {
        private Booking() { }
        public Booking(Guid userId, Guid roomId, DateTime startDate, DateTime endDate)
        {
            UserId = userId;
            RoomId = roomId;
            StartDate = startDate;
            EndDate = endDate;
            CreatedAt = DateTime.UtcNow;
        }

        public Guid UserId { get; init; }
        public Guid RoomId { get; init; }
        public DateTime StartDate { get; init; }
        public DateTime EndDate { get; init; }
        public DateTime CreatedAt { get; init; }
        public DateTime? CancelledAt { get; private set; }

        public static Booking Create(Guid userId, Guid roomId, DateTime startDate, DateTime endDate)
        {
            return new Booking(userId, roomId, startDate, endDate);
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
