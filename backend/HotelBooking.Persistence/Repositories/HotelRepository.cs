using HotelBooking.Application.Dtos;
using HotelBooking.Application.Interfaces.Repositories;
using HotelBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SharedKernel.Common;
using System.Linq;

namespace HotelBooking.Persistence.Repositories
{
    public class HotelRepository : IHotelRepository
    {
        private readonly HotelBookingDbContext _context;
        private readonly ILogger<HotelRepository> _logger;
        public HotelRepository(HotelBookingDbContext context,
            ILogger<HotelRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IQueryable<Hotel> AsQueryable(Specification<Hotel> spec, CancellationToken cancellationToken)
        {
            IQueryable<Hotel> query = _context.Hotels.AsNoTracking();

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

        public async Task<Hotel?> GetByIdAsync(Guid hotelId, bool includeRooms, CancellationToken cancellationToken = default)
        {
            if (includeRooms)
                return await _context.Hotels
                    .Include(h => h.Rooms)
                    .FirstOrDefaultAsync(h => h.Id == hotelId, cancellationToken);
            else
                return await _context.Hotels
                    .FirstOrDefaultAsync(h => h.Id == hotelId, cancellationToken);
        }

        public async Task<Result<IEnumerable<string>>> GetHotelCitiesAsync(CancellationToken cancellationToken)
        {
            var cities = await _context.Hotels
                .Select(h => h.Address.City)
                .ToListAsync(cancellationToken);

            Console.WriteLine($"Found {cities.Count} cities: {string.Join(", ", cities)}");
            return Result<IEnumerable<string>>.Success(cities);
        }

        public async Task<Result<Guid>> AddAsync(Hotel hotel, CancellationToken cancellationToken)
        {
            var user = await GetByIdAsync(hotel.Id, false, cancellationToken);
            if (user != null)
            {
                _logger.LogWarning("[Hotel Repository] User already has in db");
                return Result<Guid>.Failure("Hotel already has in db");
            }

            await _context.Hotels.AddAsync(hotel, cancellationToken);
            _logger.LogInformation("Hotel with Id: {hotelId} added successful", hotel.Id);
            return Result<Guid>.Success(hotel.Id);
        }

        public async Task<Result> DeleteAsync(Guid hotelId, CancellationToken cancellationToken)
        {
            var hotel = await GetByIdAsync(hotelId, true, cancellationToken);
            if (hotel == null)
            {
                _logger.LogWarning("[Hotel Repository] Hotel not found");
                return Result.Failure("Hotel not found");
            }
            _context.Remove(hotel);
            return Result.Success();
        }
    }
}
