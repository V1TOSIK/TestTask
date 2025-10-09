using HotelBooking.Application.Dtos;
using SharedKernel.Pagination;
using DomainBooking = HotelBooking.Domain.Entities.Booking;

namespace HotelBooking.Application.Interfaces.Repositories
{
    public interface IBookingRepository
    {
        IQueryable<DomainBooking> AsQueryable(CancellationToken cancellationToken);
        Task<PaginationResponse<BookingDto>>GetPaginatedBookingsAsync(int page, int pageSize, CancellationToken cancellationToken);
        Task<PaginationResponse<BookingDto>> GetPaginatedBookingsByUserIdAsync(Guid userId, int page, int pageSize, CancellationToken cancellationToken);
        Task<Guid> CreateAsync(DomainBooking booking, CancellationToken cancellationToken);
    }
}
