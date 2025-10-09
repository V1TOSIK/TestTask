using MediatR;
using SharedKernel.Common;

namespace HotelBooking.Application.User.Commands.LoginUser
{
    public class LoginUserRequest : IRequest<Result<string>>
    {
        public string Credential { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
