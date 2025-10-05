using HotelBooking.Application.Dtos;
using SharedKernel.Common;
using DomainRoom = HotelBooking.Domain.Entities.Room;

namespace HotelBooking.Application.Interfaces.Repositories
{
    public interface IRoomRepository
    {
        Task<DomainRoom?> GetByIdAsync(Guid roomId, CancellationToken cancellationToken);
        IQueryable<DomainRoom> AsQueryable(Specification<DomainRoom> spec, CancellationToken cancellationToken);
        Task<List<RoomDto>> GetAllAsync(CancellationToken cancellationToken);
    }
}
