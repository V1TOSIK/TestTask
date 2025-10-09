using MediatR;
using SharedKernel.Common;

namespace HotelBooking.Application.Hotel.Queries.GetCities
{
    public class GetCitiesQuery : IRequest<Result<IEnumerable<string>>>
    {
    }
}
