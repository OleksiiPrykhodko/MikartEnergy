using FluentValidation;
using MikartEnergy.BLL.Services;
using MikartEnergy.Common.DTO.CallbackRequest;
using MikartEnergy.DAL.Context.ETIM_files_reading;
using MikartEnergy.WebAPI.Validators;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace MikartEnergy.WebAPI.Extensions
{
    public static class ServiceExtensions
    {
        /// <summary>
        /// Extension method for registering business logic services.
        /// </summary>
        /// <param name="services"></param>
        public static void RegisterCustomServices(this IServiceCollection services, WebApplicationBuilder builder)
        {
            // ETIM file reading service registration.
            var etimFilePath = builder.Configuration["EtimXmlFilePath"];
            var pathToAssembly = Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location);
            services.AddSingleton<IEtimProductsFileReader, EtimProductsXmlReader>(
                x => new EtimProductsXmlReader(pathToAssembly + "\\" + etimFilePath));

            services.AddSingleton<ProductService>();
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
