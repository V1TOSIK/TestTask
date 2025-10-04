using HotelBooking.Application.Interfaces.Repositories;
using HotelBooking.Persistence.Repositories;
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
            services.AddScoped<IUnitOfWork, UnitOfWork<HotelBookingDbContext>>();
            services.AddScoped<IHotelRepository, HotelRepository>();
            services.AddScoped<IRoomRepository, RoomRepository>();
            services.AddScoped<IBookingRepository, BookingRepository>();

            services.AddDatabase(configuration);
            services.AddIdentity(configuration);

            return services;
        }
    }
}
