namespace HotelBooking.Application.Room.Commands.UpdateRoom
{
    public class UpdateRoomRequest
    {
        public int Capacity { get; set; }
        public decimal PricePerNight { get; set; }
    }
}
