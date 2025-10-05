namespace SharedKernel.Interfaces
{
    public interface ICurrentUserService
    {
        string? Role { get; }
        Guid? UserId { get; }

    }
}
