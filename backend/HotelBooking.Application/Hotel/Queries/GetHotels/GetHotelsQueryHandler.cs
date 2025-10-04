using HotelBooking.Application.Dtos;
using HotelBooking.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using SharedKernel.Common;
using SharedKernel.Extensions;
using SharedKernel.Pagination;

using DomainHotel = HotelBooking.Domain.Entities.Hotel;

namespace HotelBooking.Application.Hotel.Queries.GetHotels
{
    public class GetHotelsQueryHandler : IRequestHandler<GetHotelsQuery, Result<PaginationResponse<HotelDto>>>
    {
        private readonly ILogger<GetHotelsQueryHandler> _logger;
        private readonly IHotelRepository _hotelRepository;
        public GetHotelsQueryHandler(ILogger<GetHotelsQueryHandler> logger, IHotelRepository hotelRepository)
        {
            _logger = logger;
            _hotelRepository = hotelRepository;
        }
        public async Task<Result<PaginationResponse<HotelDto>>> Handle(GetHotelsQuery query, CancellationToken cancellationToken)
        {
            var spec = new Specification<DomainHotel>();
            //add criteria to spec if needed

            var queryable = _hotelRepository.AsQueryable(cancellationToken);

            var paginatedResult = await queryable.ToPaginatedListAsync(query.PageNumber, query.PageSize, cancellationToken);

            var items = paginatedResult.Items.Select(r => new HotelDto
            (
                r.Id,
                r.Name,
                $"{r.Address.Street} {r.Address.Building}",
                r.Address.City,
                r.Address.Country
            )).ToList();

            return Result<PaginationResponse<HotelDto>>.Success(new PaginationResponse<HotelDto>(items, paginatedResult.TotalCount, paginatedResult.PageNumber, paginatedResult.PageSize));
        }
    }
}
