using HotelBooking.Application.Dtos;
using SharedKernel.Common;
using DomainHotel = HotelBooking.Domain.Entities.Hotel;

namespace HotelBooking.Application.Interfaces.Repositories
{
    public interface IHotelRepository
    {
        IQueryable<DomainHotel> AsQueryable(Specification<DomainHotel> spec, CancellationToken cancellationToken);
        Task<List<HotelDto>> GetAllAsync(CancellationToken cancellationToken);
        Task<DomainHotel?> GetByIdAsync(Guid hotelId, bool includeRooms, CancellationToken cancellationToken);
        Task<Result<Guid>> AddAsync(DomainHotel hotel, CancellationToken cancellationToken);
        Task<Result> DeleteAsync(Guid hotelId, CancellationToken cancellationToken);
    }
}
