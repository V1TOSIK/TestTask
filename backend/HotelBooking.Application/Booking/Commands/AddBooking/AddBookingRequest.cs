namespace HotelBooking.Application.Booking.Commands.AddBooking
{
    public class AddBookingRequest
    {
        public Guid UserId { get; set; }
        public Guid RoomId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
    }
}
