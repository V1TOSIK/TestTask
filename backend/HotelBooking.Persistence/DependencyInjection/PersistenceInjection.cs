using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Common;
using SharedKernel.Interfaces;

namespace HotelBooking.Persistence.DependencyInjection
{
    public static class PersistenceInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var db = configuration.GetSection("use_db").Get<string>();
            if (db == "Postgres")
            {
                var connectionString = configuration.GetConnectionString("Postgres");
                Console.WriteLine(connectionString);
                services.AddDbContext<HotelBookingDbContext>(options =>
                    options.UseNpgsql(
                        connectionString,
                        b => b.MigrationsAssembly(typeof(HotelBookingDbContext).Assembly.FullName)));
            }
            else if(db == "MySql")
            {
                var connectionString = configuration.GetConnectionString("MySql");
                Console.WriteLine(connectionString);
                services.AddDbContext<HotelBookingDbContext>(options =>
                    options.UseMySql(
                        connectionString,
                        ServerVersion.AutoDetect(connectionString),
                        b => b.MigrationsAssembly(typeof(HotelBookingDbContext).Assembly.FullName)));     
            }
            services.AddScoped<IUnitOfWork, UnitOfWork<HotelBookingDbContext>>();

            services.AddIdentity(configuration);

            return services;
        }
    }
}
