using HotelBooking.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using SharedKernel.Common;

namespace HotelBooking.Domain.Entities
{
    public class Role : IdentityRole<Guid>
    {
        public static Result<UserRole> ParseUserRole(string? role)
        {
            if (!string.IsNullOrWhiteSpace(role))
                return Result<UserRole>.Failure("Role is empty or null");

            var parseResult = Enum.TryParse<UserRole>(role, out var parsedRole);

            if (parseResult == false)
                return Result<UserRole>.Failure("Invalid role");

            return Result<UserRole>.Success(parsedRole);
        }
    }
}