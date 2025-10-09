using HotelBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelBooking.Persistence.Configurations
{
    public class HotelConfiguration : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {
            builder.HasKey(h => h.Id);
            
            builder.Property(h => h.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.OwnsOne(x => x.Address, a =>
            {
                a.Property(p => p.Country).HasColumnName("Country").IsRequired();
                a.Property(p => p.City).HasColumnName("City").IsRequired();
                a.Property(p => p.Street).HasColumnName("Street").IsRequired();
                a.Property(p => p.Building).HasColumnName("Building").IsRequired();
                a.Property(p => p.ZipCode).HasColumnName("ZipCode");
            });

            builder.Property(h => h.Description)
                .IsRequired()
                .HasMaxLength(1000);

            builder.HasMany(h => h.Rooms)
                .WithOne(r => r.Hotel)
                .HasForeignKey(r => r.HotelId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Navigation(cg => cg.Rooms)
                .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
