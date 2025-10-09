using HotelBooking.Api.Extensions;
using HotelBooking.Application.Booking.Commands.AddBooking;
using HotelBooking.Application.Booking.Queries.GetBookings;
using HotelBooking.Application.Booking.Queries.GetMyBookings;
using HotelBooking.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Interfaces;

namespace HotelBooking.Api.Controllers.Booking
{
    [Route("api/bookings")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ICurrentUserService _currentUserService;
        public BookingController(IMediator mediator, ICurrentUserService currentUserService)
        {
            _mediator = mediator;
            _currentUserService = currentUserService;
        }

        [Authorize(Roles = nameof(UserRole.Admin))]
        [HttpGet]
        public async Task<IActionResult> GetBookings([FromQuery] GetBookingsQuery query, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);

            return this.ToActionResult(result);
        }

        [Authorize]
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetMyBookings([FromRoute] Guid userId, CancellationToken cancellationToken)
        {
            if (userId != _currentUserService.UserId)
                return Forbid();

            var result = await _mediator.Send(new GetMyBookingsQuery(userId), cancellationToken);
            return this.ToActionResult(result);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody] AddBookingRequest request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new AddBookingCommand(request), cancellationToken);

            return Ok(result);
        }
    }
}
