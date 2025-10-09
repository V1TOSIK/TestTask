using HotelBooking.Api.Extensions;
using HotelBooking.Application.User.Commands.LoginUser;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace HotelBooking.Api.Controllers.User
{
    [Route("api/auth")]
    [ApiController]
    public class LoginUserController : ControllerBase
    {
        private readonly IMediator _mediator;
        public LoginUserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new LoginUserCommand(request), cancellationToken);

            return this.ToActionResult(result);
        }
    }
}
