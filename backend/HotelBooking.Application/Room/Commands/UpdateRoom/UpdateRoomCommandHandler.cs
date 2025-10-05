using HotelBooking.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using SharedKernel.Common;
using SharedKernel.Interfaces;

namespace HotelBooking.Application.Room.Commands.UpdateRoom
{
    public class UpdateRoomCommandHandler : IRequestHandler<UpdateRoomCommand, Result>
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UpdateRoomCommandHandler> _logger;
        public UpdateRoomCommandHandler(IRoomRepository roomRepository,
            IUnitOfWork unitOfWork,
            ILogger<UpdateRoomCommandHandler> logger)
        {
            _roomRepository = roomRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Result> Handle(UpdateRoomCommand command, CancellationToken cancellationToken)
        {
            var room = await _roomRepository.GetByIdAsync(command.RoomId, cancellationToken);

            if (room == null)
            {
                _logger.LogWarning("[UpdateRoomHandler] Hotel with Id: {hotelId} not found", command.RoomId);
                return Result.Failure("Hotel not found");
            }

            var result = room.UpdateDetails(command.Request.Capacity, command.Request.PricePerNight);
            if (result.IsFailure)
            {
                _logger.LogWarning("[UpdateRoomHandler] Update result is failure");
                return Result.Failure(result.Error ?? "Invalid update data");
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return result;
        }
    }
}
