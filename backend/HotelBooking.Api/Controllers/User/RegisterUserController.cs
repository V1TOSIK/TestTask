using HotelBooking.Application.Dtos;
using HotelBooking.Application.User.Commands.RegisterUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Common;

namespace HotelBooking.Api.Controllers.User
{
    [Route("api/users")]
    [ApiController]
    public class RegisterUserController : ControllerBase
    {
        private readonly IMediator _mediator;
        public RegisterUserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("/register")]
        public async Task<ActionResult<Result<AuthorizeResponse>>> Register([FromBody] RegisterUserRequest request, CancellationToken cancellationToken = default)
        {
            var command = new RegisterUserCommand(request);
            var result = await _mediator.Send(command, cancellationToken);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result.Error);
        }
    }
}
