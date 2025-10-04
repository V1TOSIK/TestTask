namespace HotelBooking.Application.Interfaces.Services
{
    public interface IJwtService
    {
        string GenerateToken(Guid userId, IEnumerable<string> roles);
    }
}
