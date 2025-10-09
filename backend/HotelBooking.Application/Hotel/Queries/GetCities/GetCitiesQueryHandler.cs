using HotelBooking.Application.Interfaces.Repositories;
using MediatR;
using SharedKernel.Common;

namespace HotelBooking.Application.Hotel.Queries.GetCities
{
    public class GetCitiesQueryHandler : IRequestHandler<GetCitiesQuery, Result<IEnumerable<string>>>
    {
        private readonly IHotelRepository _hotelRepository;
        
        public GetCitiesQueryHandler(IHotelRepository hotelRepository)
        {
            _hotelRepository = hotelRepository;
        }

        public async Task<Result<IEnumerable<string>>> Handle(GetCitiesQuery query, CancellationToken cancellationToken)
        {
            return await _hotelRepository.GetHotelCitiesAsync(cancellationToken);
        }
    }
}
