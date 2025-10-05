using HotelBooking.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using SharedKernel.Common;
using SharedKernel.Interfaces;
using DomainBooking = HotelBooking.Domain.Entities.Booking;

namespace HotelBooking.Application.Booking.Commands.AddBooking
{
    public class AddBookingCommandHandler : IRequestHandler<AddBookingCommand, Result<Guid>>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AddBookingCommandHandler> _logger;
        public AddBookingCommandHandler(IBookingRepository bookingRepository,
            IRoomRepository roomRepository,
            IUnitOfWork unitOfWork,
            ILogger<AddBookingCommandHandler> logger)
        {
            _bookingRepository = bookingRepository;
            _roomRepository = roomRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Result<Guid>> Handle(AddBookingCommand command, CancellationToken cancellationToken)
        {
            var room = await _roomRepository.GetByIdAsync(command.Request.RoomId, cancellationToken);
            if (room == null)
            {
                _logger.LogWarning("Room with ID {RoomId} not found", command.Request.RoomId);
                return Result<Guid>.Failure("Room not found");
            }
            var totalPrice = DomainBooking.CalculateTotalPrice(room.PricePerNight, command.Request.CheckInDate, command.Request.CheckOutDate);
            if (totalPrice.IsFailure)
            {
                _logger.LogWarning("Failed to calculate total price for booking: {Reason}", totalPrice.Error);
                return Result<Guid>.Failure(totalPrice.Error ?? "error in AddBooking");
            }
            var booking = DomainBooking.Create(command.Request.UserId, command.Request.RoomId, totalPrice.Value, command.Request.CheckInDate, command.Request.CheckOutDate);
            await _bookingRepository.CreateAsync(booking, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result<Guid>.Success(booking.Id);
        }
    }
}
