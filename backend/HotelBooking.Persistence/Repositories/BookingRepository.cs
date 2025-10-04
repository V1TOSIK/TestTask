using HotelBooking.Application.Interfaces.Repositories;
using HotelBooking.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace HotelBooking.Persistence.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly HotelBookingDbContext _context;
        private readonly ILogger<BookingRepository> _logger;
        public BookingRepository(HotelBookingDbContext context, ILogger<BookingRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IQueryable<Booking> AsQueryable(CancellationToken cancellationToken)
        {
            return _context.Bookings.AsQueryable();
        }

        public async Task<long> CreateAsync(Booking booking, CancellationToken cancellationToken)
        {
            await _context.Bookings.AddAsync(booking, cancellationToken);
            _logger.LogInformation("Booking created with ID: {BookingId}", booking.Id);
            return booking.Id;
        }
    }
}
