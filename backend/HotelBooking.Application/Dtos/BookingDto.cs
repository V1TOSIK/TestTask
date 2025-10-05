namespace HotelBooking.Application.Dtos
{
    public class BookingDto
    {
        public BookingDto() { }
        public BookingDto(Guid id,
            Guid userId,
            string userName,
            Guid roomId,
            int roomNumber,
            DateTime checkInDate,
            DateTime checkOutDate,
            decimal totalPrice,
            DateTime createdAt)
        {
            Id = id;
            UserId = userId;
            UserName = userName;
            RoomId = roomId;
            RoomNumber = roomNumber;
            CheckInDate = checkInDate;
            CheckOutDate = checkOutDate;
            TotalPrice = totalPrice;
            CreatedAt = createdAt;
        }
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public Guid RoomId { get; set; }
        public int RoomNumber { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
