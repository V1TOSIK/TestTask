using HotelBooking.Application.Hotel.Commands.DeleteHotel;
using HotelBooking.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using SharedKernel.Common;
using SharedKernel.Interfaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace HotelBooking.Application.Room.Commands.DeleteRoom
{
    public class DeleteRoomCommandHandler : IRequestHandler<DeleteRoomCommand, Result>
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DeleteRoomCommandHandler> _logger;

        public DeleteRoomCommandHandler(IHotelRepository hotelRepository,
            IUnitOfWork unitOfWork,
            IRoomRepository roomRepository,
            ILogger<DeleteRoomCommandHandler> logger)
        {
            _hotelRepository = hotelRepository;
            _unitOfWork = unitOfWork;
            _roomRepository = roomRepository;
            _logger = logger;
        }

        public async Task<Result> Handle(DeleteRoomCommand command, CancellationToken cancellationToken)
        {
            var room = await _roomRepository.GetByIdAsync(command.RoomId, cancellationToken);

            if (room == null)
            {
                _logger.LogWarning("[DeleteRoomHandler] Hotel with Id: {hotelId} not found", command.RoomId);
                return Result.Failure("Hotel not found");
            }

            var hotel = await _hotelRepository.GetByIdAsync(room.HotelId, true, cancellationToken);

            if (hotel == null)
            {
                _logger.LogWarning("[DeleteRoomHandler] Hotel with Id: {hotelId} not found", room.HotelId);
                return Result.Failure("Hotel not found");
            }

            hotel.RemoveRoom(room);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("[DeleteRoomHandler] Room with Id: {roomId} deleted", room.Id);
            return Result.Success();
        }
    }
}
