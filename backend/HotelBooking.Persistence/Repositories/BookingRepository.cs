using HotelBooking.Application.Dtos;
using HotelBooking.Application.Interfaces.Repositories;
using HotelBooking.Domain.Entities;
using Microsoft.Extensions.Logging;
using SharedKernel.Extensions;
using SharedKernel.Pagination;
using System.Data;

namespace HotelBooking.Persistence.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly HotelBookingDbContext _context;
        private readonly IDbConnection _dbConnection;
        private readonly ILogger<BookingRepository> _logger;
        public BookingRepository(HotelBookingDbContext context,
            IDbConnection dbConnection,
            ILogger<BookingRepository> logger)
        {
            _context = context;
            _dbConnection = dbConnection;
            _logger = logger;
        }

        public IQueryable<Booking> AsQueryable(CancellationToken cancellationToken)
        {
            return _context.Bookings.AsQueryable();
        }

        public async Task<Guid> CreateAsync(Booking booking, CancellationToken cancellationToken)
        {
            await _context.Bookings.AddAsync(booking, cancellationToken);
            _logger.LogInformation("Booking created with ID: {BookingId}", booking.Id);
            return booking.Id;
        }

        public async Task<PaginationResponse<BookingDto>> GetPaginatedBookingsByUserIdAsync(
            Guid userId, int page, int pageSize, CancellationToken cancellationToken)
        {
            var baseSql = @"
                SELECT 
                    b.Id,
                    u.Id AS UserId,
                    u.UserName,
                    b.RoomId,
                    r.Number AS RoomNumber,
                    b.CheckInDate,
                    b.CheckOutDate,
                    b.TotalPrice,
                    b.CreatedAt
                FROM Bookings b
                JOIN Rooms r ON b.RoomId = r.Id
                JOIN User u ON b.UserId = u.Id
                WHERE b.UserId = @UserId
                ORDER BY b.CreatedAt DESC
            ";

            var parameters = new { UserId = userId };

            var result = await _dbConnection.QueryPagedAsync<BookingDto>(
                baseSql, parameters, page, pageSize);

            return result;
        }

        public async Task<PaginationResponse<BookingDto>> GetPaginatedBookingsAsync(int page, int pageSize, CancellationToken cancellationToken)
        {
            var baseSql = @"
                SELECT 
                    b.Id,
                    u.Id AS UserId,
                    u.UserName,
                    b.RoomId,
                    r.Number AS RoomNumber,
                    b.CheckInDate,
                    b.CheckOutDate,
                    b.TotalPrice,
                    b.CreatedAt
                FROM Bookings b
                JOIN Rooms r ON b.RoomId = r.Id
                JOIN User u ON b.UserId = u.Id
                ORDER BY b.CreatedAt DESC
            ";


            var result = await _dbConnection.QueryPagedAsync<BookingDto>(
                baseSql, new object(), page, pageSize);

            return result;
        }
    }
}
