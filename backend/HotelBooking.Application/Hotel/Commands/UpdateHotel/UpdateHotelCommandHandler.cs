using HotelBooking.Application.Interfaces.Repositories;
using HotelBooking.Domain.ValueObjects;
using MediatR;
using Microsoft.Extensions.Logging;
using SharedKernel.Common;
using SharedKernel.Interfaces;

namespace HotelBooking.Application.Hotel.Commands.UpdateHotel
{
    public class UpdateHotelCommandHandler : IRequestHandler<UpdateHotelCommand, Result>
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UpdateHotelCommandHandler> _logger;

        public UpdateHotelCommandHandler(IHotelRepository hotelRepository,
            IUnitOfWork unitOfWork,
            ILogger<UpdateHotelCommandHandler> logger)
        {
            _hotelRepository = hotelRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Result> Handle(UpdateHotelCommand command, CancellationToken cancellationToken)
        {
            var hotel = await _hotelRepository.GetByIdAsync(command.HotelId, false, cancellationToken);
            if (hotel == null)
            {
                _logger.LogWarning("[UpdateHotelHandler] Hotel not found");
                return Result.Failure("Hotel not found");
            }

            command.Request.Name = string.IsNullOrWhiteSpace(command.Request.Name) ? hotel.Name : command.Request.Name;
            command.Request.Country = string.IsNullOrWhiteSpace(command.Request.Country) ? hotel.Address.Country : command.Request.Country;
            command.Request.City = string.IsNullOrWhiteSpace(command.Request.City) ? hotel.Address.City : command.Request.City;
            command.Request.Street = string.IsNullOrWhiteSpace(command.Request.Street) ? hotel.Address.Street : command.Request.Street;
            command.Request.Building = string.IsNullOrWhiteSpace(command.Request.Building) ? hotel.Address.Building : command.Request.Building;
            command.Request.ZipCode = string.IsNullOrWhiteSpace(command.Request.ZipCode) ? hotel.Address.ZipCode : command.Request.ZipCode;
            command.Request.Description = string.IsNullOrWhiteSpace(command.Request.Description) ? hotel.Description : command.Request.Description;

            var newAddress = new Address(command.Request.Country,
                command.Request.City,
                command.Request.Street,
                command.Request.Building,
                command.Request.ZipCode);

            var result = hotel.UpdateDetails(command.Request.Name, newAddress, command.Request.Description);

            if (result.IsFailure)
            {
                _logger.LogWarning("[UpdateHotelHandler] Update hotel result is failure");
                return result;
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("[UpdateHotelHandler] Hotel with Id: {hotelId} was updated", hotel.Id);
            return Result.Success();
        }
    }
}
