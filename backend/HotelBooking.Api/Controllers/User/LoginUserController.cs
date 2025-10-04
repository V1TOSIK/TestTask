using HotelBooking.Application.Dtos;
using HotelBooking.Application.User.Commands.LoginUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Common;

namespace HotelBooking.Api.Controllers.User
{
    [Route("api/users")]
    [ApiController]
    public class LoginUserController : ControllerBase
    {
        private readonly IMediator _mediator;
        public LoginUserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<ActionResult<Result<AuthorizeResponse>>> Login([FromBody] LoginUserRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new LoginUserCommand(request), cancellationToken);

            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result.Error);
        }
    }
}
