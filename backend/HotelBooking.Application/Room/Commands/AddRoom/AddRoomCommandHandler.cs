using HotelBooking.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using SharedKernel.Common;
using SharedKernel.Interfaces;

using DomainRoom = HotelBooking.Domain.Entities.Room;

namespace HotelBooking.Application.Room.Commands.AddRoom
{
    public class AddRoomCommandHandler : IRequestHandler<AddRoomCommand, Result<Guid?>>
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AddRoomCommandHandler> _logger;

        public AddRoomCommandHandler(IHotelRepository hotelRepository,
            IUnitOfWork unitOfWork,
            ILogger<AddRoomCommandHandler> logger)
        {
            _hotelRepository = hotelRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<Result<Guid?>> Handle(AddRoomCommand command, CancellationToken cancellationToken)
        {
            var hotel = await _hotelRepository.GetByIdAsync(command.HotelId, true, cancellationToken);

            if (hotel == null)
            {
                _logger.LogWarning("[AddRoomHandler] Hotel with Id: {hotelId} not found", command.HotelId);
                return Result<Guid?>.Failure("Hotel not found");
            }

            var room = DomainRoom.Create(command.Number, command.Capacity, command.PricePerNight, command.HotelId);
            var addResult = hotel.AddRoom(room);
            if (addResult.IsFailure)
            {
                _logger.LogWarning("[AddRoomHandler] Room with that number already has");
                return Result<Guid?>.Failure($"Room with this number: {room.Number} already has");
            }
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("[AddRoomHandler] Room with Id: {roomId} added to hotel with Id: {hotelId}", room.Id, hotel.Id);
            return Result<Guid?>.Success(room.Id);
        }
    }
}
