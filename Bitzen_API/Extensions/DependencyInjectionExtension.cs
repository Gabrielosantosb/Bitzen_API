using Bitzen_API.Application.Services.Reservation;
using Bitzen_API.Application.Services.Room;
using Bitzen_API.Application.Services.Token;
using Bitzen_API.Application.Services.User;
using Bitzen_API.ORM.Entity;
using Bitzen_API.ORM.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Bitzen_API.Extensions
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            
            services.AddScoped<BaseRepository<UserModel>>();
            services.AddScoped<BaseRepository<RoomModel>>();
            services.AddScoped<BaseRepository<ReservationModel>>();


            
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<IReservationService, ReservationService>();

            return services;
        }
    }
}
