using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySqlConnector;
using Npgsql;
using System.Data;

namespace HotelBooking.Persistence.DependencyInjection
{
    public static class DatabaseInjection
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
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

                services.AddTransient<IDbConnection>(sp =>
                {
                    return new NpgsqlConnection(connectionString);
                });
            }
            else if (db == "MySql")
            {
                var connectionString = configuration.GetConnectionString("MySql");
                Console.WriteLine(connectionString);
                services.AddDbContext<HotelBookingDbContext>(options =>
                    options.UseMySql(
                        connectionString,
                        ServerVersion.AutoDetect(connectionString),
                        b => b.MigrationsAssembly(typeof(HotelBookingDbContext).Assembly.FullName)));

                services.AddTransient<IDbConnection>(sp =>
                {
                    return new MySqlConnection(connectionString);
                });
            }

            return services;
        }
    }
}
