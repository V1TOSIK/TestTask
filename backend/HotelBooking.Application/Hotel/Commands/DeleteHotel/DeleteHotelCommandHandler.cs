using HotelBooking.Application.Hotel.Commands.UpdateHotel;
using HotelBooking.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using SharedKernel.Common;
using SharedKernel.Interfaces;

namespace HotelBooking.Application.Hotel.Commands.DeleteHotel
{
    public class DeleteHotelCommandHandler : IRequestHandler<DeleteHotelCommand, Result>
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DeleteHotelCommandHandler> _logger;

        public DeleteHotelCommandHandler(IHotelRepository hotelRepository,
            IUnitOfWork unitOfWork,
            ILogger<DeleteHotelCommandHandler> logger)
        {
            _hotelRepository = hotelRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Result> Handle(DeleteHotelCommand command, CancellationToken cancellationToken)
        {
            var result = await _hotelRepository.DeleteAsync(command.HotelId, cancellationToken);
            if (result.IsFailure)
            {
                _logger.LogWarning("[DeleteHotelHandler] Delete user result is failure");
                return result;
            }
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("[DeleteHotelHandler] Hotel deleted successful");
            return Result.Success();
        }
    }
}
