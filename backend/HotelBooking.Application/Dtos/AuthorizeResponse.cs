namespace HotelBooking.Application.Dtos
{
    public class AuthorizeResponse
    {
        public string Token { get; set; } = string.Empty;
        public AuthorizeResponse(string token)
        {
            Token = token;
        }
    }
}
