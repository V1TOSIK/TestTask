using HotelBooking.Application.Dtos;

using DomainHotel = HotelBooking.Domain.Entities.Hotel;

namespace HotelBooking.Application.Interfaces.Repositories
{
    public interface IHotelRepository
    {
        IQueryable<DomainHotel> AsQueryable(CancellationToken cancellationToken);
        Task<List<HotelDto>> GetAllAsync(CancellationToken cancellationToken);
    }
}
