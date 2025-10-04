using HotelBooking.Application.Dtos;
using HotelBooking.Application.Interfaces.Repositories;
using HotelBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Persistence.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly HotelBookingDbContext _context;
        public RoomRepository(HotelBookingDbContext context)
        {
            _context = context;
        }

        public IQueryable<Room> AsQueryable(CancellationToken cancellationToken)
        {
            return _context.Rooms.AsQueryable();
        }

        public async Task<List<RoomDto>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Rooms
                .Select(r => new RoomDto
                (
                    r.Id,
                    r.Number,
                    r.Capacity,
                    r.PricePerNight
                ))
                .ToListAsync(cancellationToken);
        }
    }
}
