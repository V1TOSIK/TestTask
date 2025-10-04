using HotelBooking.Application.Dtos;
using HotelBooking.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using SharedKernel.Common;
using SharedKernel.Extensions;
using SharedKernel.Pagination;
using DomainRoom = HotelBooking.Domain.Entities.Room;

namespace HotelBooking.Application.Room.Queries.GetRooms
{
    public class GetRoomsQueryHandler : IRequestHandler<GetRoomsQuery, Result<PaginationResponse<RoomDto>>>
    {
        private readonly IRoomRepository _roomRepository;
        private readonly ILogger<GetRoomsQueryHandler> _logger;
        public GetRoomsQueryHandler(IRoomRepository roomRepository, ILogger<GetRoomsQueryHandler> logger)
        {
            _roomRepository = roomRepository;
            _logger = logger;
        }
        public async Task<Result<PaginationResponse<RoomDto>>> Handle(GetRoomsQuery query, CancellationToken cancellationToken)
        {
            var spec = new Specification<DomainRoom>();
            //add criteria to spec if needed

            var queryable = _roomRepository.AsQueryable(cancellationToken);

            var paginatedResult = await queryable.ToPaginatedListAsync(query.PageNumber, query.PageSize, cancellationToken);

            var items = paginatedResult.Items.Select(r => new RoomDto
            (
                r.Id,
                r.Number,
                r.Capacity,
                r.PricePerNight
            )).ToList();

            return Result<PaginationResponse<RoomDto>>.Success(new PaginationResponse<RoomDto>(items, paginatedResult.TotalCount, paginatedResult.PageNumber, paginatedResult.PageSize));
        }
    }
}
