using SharedKernel.Common;

namespace HotelBooking.Domain.Entities
{
    public class Booking : Entity<Guid>
    {
        private Booking() { }
        public Booking(Guid userId, Guid roomId, decimal totalPrice, DateTime checkInDate, DateTime checkOutDate)
        {
            UserId = userId;
            RoomId = roomId;
            TotalPrice = totalPrice;
            CheckInDate = checkInDate;
            CheckOutDate = checkOutDate;
            CreatedAt = DateTime.UtcNow;
        }

        public Guid UserId { get; init; }
        public Guid RoomId { get; init; }
        public decimal TotalPrice { get; set; }
        public DateTime CheckInDate { get; init; }
        public DateTime CheckOutDate { get; init; }
        public DateTime CreatedAt { get; init; }
        public DateTime? CancelledAt { get; private set; }

        public static Booking Create(Guid userId, Guid roomId, decimal totalPrice, DateTime checkInDate, DateTime checkOutDate)
        {
            return new Booking(userId, roomId, totalPrice, checkInDate, checkOutDate);
        }

        public Result Cancel()
        {
            if (CancelledAt != null)
                return Result.Failure("Booking is already cancelled");
            CancelledAt = DateTime.UtcNow;
            return Result.Success();
        }

        public static Result<decimal> CalculateTotalPrice(decimal pricePerNight, DateTime checkInDate, DateTime checkOutDate)
        {
            if (checkOutDate <= checkInDate)
                return Result<decimal>.Failure("Check-out date must be after check-in date");
            var totalNights = (checkOutDate - checkInDate).Days;
            var totalPrice = totalNights * pricePerNight;
            return Result<decimal>.Success(totalPrice);
        }
    }
}
