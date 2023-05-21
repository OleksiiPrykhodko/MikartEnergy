using FluentValidation;
using MikartEnergy.BLL.Services;
using MikartEnergy.Common.DTO.CallbackRequest;
using MikartEnergy.WebAPI.Validators;

namespace MikartEnergy.WebAPI.Extensions
{
    public static class ServiceExtensions
    {
        /// <summary>
        /// Extension method for registering business logic services.
        /// </summary>
        /// <param name="services"></param>
        public static void RegisterCustomServices(this IServiceCollection services)
        {
            services.AddScoped<CallbackRequestService>();
        }

        /// <summary>
        /// Extension method for registering FluentValidation.
        /// </summary>
        /// <param name="services"></param>
        public static void RegisterCustomValidators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<NewCallbackRequestDTO>, NewCallbackRequestDTOValidator>();
            services.AddScoped<IValidator<CallbackRequestDTO>, CallbackRequestDTOValidator>();
        }
    }
}
