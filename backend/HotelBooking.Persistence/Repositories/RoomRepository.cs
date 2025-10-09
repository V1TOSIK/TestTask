using HotelBooking.Application.Dtos;
using HotelBooking.Application.Interfaces.Repositories;
using HotelBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Common;

namespace HotelBooking.Persistence.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly HotelBookingDbContext _context;
        public RoomRepository(HotelBookingDbContext context)
        {
            _context = context;
        }

        public async Task<Room?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Rooms.FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
        }

        public IQueryable<Room> AsQueryable(Specification<Room> spec, CancellationToken cancellationToken)
        {
            IQueryable<Room> query = _context.Rooms.AsNoTracking();

            foreach (var criteria in spec.Criteria)
                query = query.Where(criteria);

            foreach (var include in spec.Includes)
                query = query.Include(include);

            if (spec.OrderBy != null)
                query = query.OrderBy(spec.OrderBy);

            else if (spec.OrderByDescending != null)
                query = query.OrderByDescending(spec.OrderByDescending);

            return query;
        }
    }
}
