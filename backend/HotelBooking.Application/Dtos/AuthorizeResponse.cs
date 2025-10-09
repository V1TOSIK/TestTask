namespace HotelBooking.Application.Dtos
{
    public class AuthorizeResponse
    {
        public Guid UserId { get; set; }
        public string Token { get; set; } = string.Empty;
        public AuthorizeResponse(Guid userId, string token)
        {
            UserId = userId;
            Token = token;
        }
    }
}
