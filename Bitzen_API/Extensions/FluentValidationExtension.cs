using Bitzen_API.ORM.Model.Reservation;
using Bitzen_API.ORM.Model.User;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace Bitzen_API.Extensions
{
    public static class FluentValidationExtension
    {
        public static IServiceCollection AddFluentValidationConfiguration(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();
            
            services.AddScoped<IValidator<CreateReservationModel>, CreateReservationModelValidator>();
            services.AddScoped<IValidator<CreateUserModel>, CreateUserModelValidator>();

            return services;
        }
    }
}
