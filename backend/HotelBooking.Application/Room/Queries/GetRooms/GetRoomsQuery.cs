using HotelBooking.Application.Dtos;
using MediatR;
using SharedKernel.Common;
using SharedKernel.Pagination;

namespace HotelBooking.Application.Room.Queries.GetRooms
{
    public class GetRoomsQuery : PaginationRequest, IRequest<Result<PaginationResponse<RoomDto>>>
    {
        public List<string>? Cities { get; set; } = new();
        public DateTime? CheckInDate { get; set; }
    }
}
