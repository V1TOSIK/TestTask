using HotelBooking.Application.Dtos;

using DomainRoom = HotelBooking.Domain.Entities.Room;

namespace HotelBooking.Application.Interfaces.Repositories
{
    public interface IRoomRepository
    {
        IQueryable<DomainRoom> AsQueryable(CancellationToken cancellationToken);
        Task<List<RoomDto>> GetAllAsync(CancellationToken cancellationToken);
    }
}
