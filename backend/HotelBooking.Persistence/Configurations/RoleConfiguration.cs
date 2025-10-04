using HotelBooking.Domain.Entities;
using HotelBooking.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelBooking.Persistence.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasData(
                new Role { Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), Name = UserRole.Admin.ToString(), NormalizedName = "ADMIN" },
                new Role { Id = Guid.Parse("22222222-2222-2222-2222-222222222222"), Name = UserRole.User.ToString(), NormalizedName = "USER" }
            );
        }
    }
}
