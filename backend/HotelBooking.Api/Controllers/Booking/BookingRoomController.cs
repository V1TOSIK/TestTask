using HotelBooking.Api.Extensions;
using HotelBooking.Application.Booking.Commands.BookingRoom;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Api.Controllers.Booking
{
    [Route("api/user/booking")]
    [ApiController]
    public class BookingRoomController : ControllerBase
    {
        private readonly IMediator _mediator;
        public BookingRoomController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody] BookingRoomRequest request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new BookingRoomCommand(request), cancellationToken);

            return this.ToActionResult(result);
        }
    }
}
