using HotelBooking.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using SharedKernel.Common;
using SharedKernel.Interfaces;
using DomainBooking = HotelBooking.Domain.Entities.Booking;

namespace HotelBooking.Application.Booking.Commands.BookingRoom
{
    public class BookingRoomCommandHandler : IRequestHandler<BookingRoomCommand, Result<long>>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<BookingRoomCommandHandler> _logger;
        public BookingRoomCommandHandler(IBookingRepository bookingRepository,
            IUnitOfWork unitOfWork,
            ILogger<BookingRoomCommandHandler> logger)
        {
            _bookingRepository = bookingRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Result<long>> Handle(BookingRoomCommand command, CancellationToken cancellationToken)
        {
            var booking = DomainBooking.Create(command.Request.UserId, command.Request.RoomId, command.Request.CheckInDate, command.Request.CheckOutDate);
            await _bookingRepository.CreateAsync(booking, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result<long>.Success(booking.Id);
        }
    }
}
