using HotelBooking.Application.Dtos;
using MediatR;
using SharedKernel.Common;

namespace HotelBooking.Application.User.Commands.RegisterUser
{
    public class RegisterUserCommand : IRequest<Result<AuthorizeResponse>>
    {
        public RegisterUserRequest Request { get; }

        public RegisterUserCommand(RegisterUserRequest request)
        {
            Request = request;
        }
    }
}
