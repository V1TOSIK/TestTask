using HotelBooking.Application.Dtos;
using MediatR;
using SharedKernel.Common;

namespace HotelBooking.Application.User.Commands.LoginUser
{
    public class LoginUserCommand : IRequest<Result<AuthorizeResponse>>
    {
        public LoginUserRequest Request { get; set; }
        public LoginUserCommand(LoginUserRequest loginUserRequest)
        {
            Request = loginUserRequest;
        }
    }
}
