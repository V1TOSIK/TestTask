namespace HotelBooking.Infrastructure.Options
{
    public class JwtOptions
    {
        public string SecretKey { get; set; } = string.Empty;
        // in minutes
        public int AccessTokenExpirationTime { get; set; }
    }
}
