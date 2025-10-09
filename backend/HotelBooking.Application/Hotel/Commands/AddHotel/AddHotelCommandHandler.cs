using HotelBooking.Application.Interfaces.Repositories;
using HotelBooking.Domain.ValueObjects;
using MediatR;
using Microsoft.Extensions.Logging;
using SharedKernel.Common;
using SharedKernel.Interfaces;

using DomainHotel = HotelBooking.Domain.Entities.Hotel;

namespace HotelBooking.Application.Hotel.Commands.AddHotel
{
    public class AddHotelCommandHandler : IRequestHandler<AddHotelCommand, Result<Guid>>
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AddHotelCommandHandler> _logger;
        public AddHotelCommandHandler(IHotelRepository hotelRepository,
            IUnitOfWork unitOfWork,
            ILogger<AddHotelCommandHandler> logger)
        {
            _hotelRepository = hotelRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<Result<Guid>> Handle(AddHotelCommand command, CancellationToken cancellationToken)
        {
            var addres = new Address(command.Country,
                command.City,
                command.Street,
                command.Building,
                command.ZipCode);

            var hotel = DomainHotel.Create(command.Name, addres, command.Description);

            var result = await _hotelRepository.AddAsync(hotel, cancellationToken);
            if (result.IsFailure)
            {
                _logger.LogWarning("[AddHotelHandler] Add result is failure");
                return Result<Guid>.Failure(result.Error ?? "User already Added");
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return result;
        }
    }
}
