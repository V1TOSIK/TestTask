namespace HotelBooking.Application.User.Commands.RegisterUser
{
    public class RegisterUserRequest
    {
        public string Credential { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
