using HotelBooking.Api.Extensions;
using HotelBooking.Application.Room.Commands.AddRoom;
using HotelBooking.Application.Room.Commands.DeleteRoom;
using HotelBooking.Application.Room.Commands.UpdateRoom;
using HotelBooking.Application.Room.Queries.GetRooms;
using HotelBooking.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Api.Controllers.Room
{
    [Route("api/rooms")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IMediator _mediator;
        public RoomController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetRoomsQuery query, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);

            return this.ToActionResult(result);
        }

        [Authorize(Roles = nameof(UserRole.Admin))]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddRoomCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return this.ToActionResult(result);
        }

        [Authorize(Roles = nameof(UserRole.Admin))]
        [HttpPatch("{roomId}")]
        public async Task<IActionResult> Update([FromRoute] Guid roomId, [FromBody] UpdateRoomRequest request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new UpdateRoomCommand(roomId, request), cancellationToken);
            return Ok(result);
        }

        [Authorize(Roles = nameof(UserRole.Admin))]
        [HttpDelete("{roomId}")]
        public async Task<IActionResult> Delete([FromRoute] Guid roomId, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new DeleteRoomCommand(roomId), cancellationToken);
            return NoContent();
        }
    }
}