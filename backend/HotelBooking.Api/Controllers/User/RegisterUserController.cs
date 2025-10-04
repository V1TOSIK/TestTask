using HotelBooking.Api.Extensions;
using HotelBooking.Application.User.Commands.RegisterUser;
using Microsoft.AspNetCore.Mvc;
using MediatR;

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
        public async Task<IActionResult> Register([FromBody] RegisterUserRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new RegisterUserCommand(request), cancellationToken);
            
            return this.ToActionResult(result);
        }
    }
}
