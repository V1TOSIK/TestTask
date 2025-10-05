using HotelBooking.Api.Extensions;
using HotelBooking.Application.Hotel.Commands.AddHotel;
using HotelBooking.Application.Hotel.Commands.DeleteHotel;
using HotelBooking.Application.Hotel.Commands.UpdateHotel;
using HotelBooking.Application.Hotel.Queries.GetHotels;
using HotelBooking.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Api.Controllers.Hotel
{
    [Route("api/hotels")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IMediator _mediator;
        public HotelController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetHotels([FromQuery] GetHotelsQuery query, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);

            return this.ToActionResult(result);
        }

        [Authorize(Roles = nameof(UserRole.Admin))]
        [HttpPost]
        public async Task<IActionResult> AddHotel([FromBody] AddHotelCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);

            return this.ToActionResult(result);
        }

        [Authorize(Roles = nameof(UserRole.Admin))]
        [HttpPatch("{hotelId}")]
        public async Task<IActionResult> UpdateHotelData([FromRoute] Guid hotelId, [FromBody] UpdateHotelRequest request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new UpdateHotelCommand(hotelId, request), cancellationToken);

            return Ok(result);
        }

        [Authorize(Roles = nameof(UserRole.Admin))]
        [HttpDelete("{hotelId}")]
        public async Task<IActionResult> DeleteHotel([FromRoute] Guid hotelId, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new DeleteHotelCommand(hotelId), cancellationToken);
            return NoContent();
        }
    }
}
