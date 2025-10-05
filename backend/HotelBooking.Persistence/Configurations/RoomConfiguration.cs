using HotelBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelBooking.Persistence.Configurations
{
    public class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Id)
                .ValueGeneratedNever();

            builder.Property(r => r.Number)
                .IsRequired();
            
            builder.Property(r => r.Capacity)
                .IsRequired();
            
            builder.Property(r => r.PricePerNight)
                .IsRequired()
                .HasColumnType("decimal(18,2)");
            
            builder.HasOne(r => r.Hotel)
                .WithMany(h => h.Rooms)
                .HasForeignKey(r => r.HotelId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(r => r.Bookings)
                .WithOne()
                .HasForeignKey(r => r.RoomId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(r => new { r.HotelId, r.Number }).IsUnique();
        }
    }
}
