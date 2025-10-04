using DomainUser = HotelBooking.Domain.Entities.User;

namespace HotelBooking.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<DomainUser?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task AddAsync(DomainUser user, CancellationToken cancellationToken);
    }
}
