using HotelBooking.Api.Extensions;
using HotelBooking.Application.Hotel.Queries.GetHotels;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace HotelBooking.Api.Controllers.Hotel
{
    [Route("api/hotels")]
    [ApiController]
    public class GetHotelsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public GetHotelsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetHotels([FromQuery] GetHotelsQuery query, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);
            
            return this.ToActionResult(result);
        }
    }
}
