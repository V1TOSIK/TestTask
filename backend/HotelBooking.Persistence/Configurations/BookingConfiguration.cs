using HotelBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelBooking.Persistence.Configurations
{
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.HasKey(b => b.Id);
            
            builder.Property(b => b.Id)
                .ValueGeneratedNever();
            
            builder.Property(b => b.UserId)
                .IsRequired();
            
            builder.Property(b => b.RoomId)
                .IsRequired();
            
            builder.Property(b => b.StartDate)
                .IsRequired();
            
            builder.Property(b => b.EndDate)
                .IsRequired();
            
            builder.Property(b => b.CreatedAt)
                .IsRequired();
            
            builder.Property(b => b.CancelledAt)
                .IsRequired(false);
        }
    }
}
