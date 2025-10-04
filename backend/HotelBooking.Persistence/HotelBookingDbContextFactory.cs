using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
namespace HotelBooking.Persistence
{
    public class HotelBookingDbContextFactory : IDesignTimeDbContextFactory<HotelBookingDbContext>
    {
        public HotelBookingDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../HotelBooking.Api"))
                .AddJsonFile("appsettings.json", optional: true)
                .Build();

            var db = configuration.GetSection("use_db").Get<string>();

            if (db == "Postgres")
            {
                var connectionString = configuration.GetConnectionString("Postgres");
                var optionsBuilder = new DbContextOptionsBuilder<HotelBookingDbContext>();
                optionsBuilder.UseNpgsql(
                    connectionString,
                    b => b.MigrationsAssembly(typeof(HotelBookingDbContext).Assembly.FullName));
                return new HotelBookingDbContext(optionsBuilder.Options);
            }
            else if (db == "MySql")
            {
                var connectionString = configuration.GetConnectionString("MySql");
                var optionsBuilder = new DbContextOptionsBuilder<HotelBookingDbContext>();
                optionsBuilder.UseMySql(
                    connectionString,
                    ServerVersion.AutoDetect(connectionString),
                    b => b.MigrationsAssembly(typeof(HotelBookingDbContext).Assembly.FullName));
                return new HotelBookingDbContext(optionsBuilder.Options);
            }
            else
            {
                throw new InvalidOperationException("Database provider not configured.");
            }

        }
    }
}
