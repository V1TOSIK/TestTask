using HotelBooking.Application.Dtos;
using HotelBooking.Application.Interfaces.Repositories;
using HotelBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Persistence.Repositories
{
    public class HotelRepository : IHotelRepository
    {
        private readonly HotelBookingDbContext _context;
        public HotelRepository(HotelBookingDbContext context)
        {
            _context = context;
        }

        public IQueryable<Hotel> AsQueryable(CancellationToken cancellationToken)
        {
            return _context.Hotels.AsQueryable();
        }

        public async Task<List<HotelDto>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Hotels
                .Select(h => new HotelDto
                (
                    h.Id,
                    h.Name,
                    $"{h.Address.Street} {h.Address.Building}",
                    h.Address.City,
                    h.Address.Country
                ))
                .ToListAsync(cancellationToken);
        }
    }
}
