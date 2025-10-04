using HotelBooking.Api.Extensions;
using HotelBooking.Application.Room.Queries.GetRooms;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace HotelBooking.Api.Controllers.Room
{
    [Route("api/rooms")]
    [ApiController]
    public class GetRoomsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public GetRoomsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetRooms([FromQuery] GetRoomsQuery query, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);
            
            return this.ToActionResult(result);
        }
    }
}
