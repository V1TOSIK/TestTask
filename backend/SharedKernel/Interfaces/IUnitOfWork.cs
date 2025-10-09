using Microsoft.EntityFrameworkCore;

namespace SharedKernel.Interfaces
{
    public interface IUnitOfWork
    {
        Task SaveChangesAsync(CancellationToken cancellationToken);
        Task ExecuteInTransactionAsync(Func<Task> action, CancellationToken cancellationToken);
    }
}
