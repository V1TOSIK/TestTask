using HotelBooking.Application.Dtos;
using DomainBooking = HotelBooking.Domain.Entities.Booking;

namespace HotelBooking.Application.Interfaces.Repositories
{
    public interface IBookingRepository
    {
        IQueryable<DomainBooking> AsQueryable(CancellationToken cancellationToken);
        Task<long> CreateAsync(DomainBooking booking, CancellationToken cancellationToken);
    }
}
