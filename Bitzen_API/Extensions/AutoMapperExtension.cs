using Bitzen_API.ORM.Mappings.Reservation;
using Bitzen_API.ORM.Mappings.Room;
using Bitzen_API.ORM.Mappings.User;
using Microsoft.Extensions.DependencyInjection;

namespace Bitzen_API.Extensions
{
    public static class AutoMapperExtension
    {
        public static IServiceCollection AddAutoMapperConfiguration(this IServiceCollection services)
        {            
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            
            services.AddAutoMapper(typeof(RoomMappingProfile));
            services.AddAutoMapper(typeof(UserMappingProfile));
            services.AddAutoMapper(typeof(ReservationMappingProfile));
            services.AddAutoMapper(typeof(Program));

            return services;
        }
    }
}
