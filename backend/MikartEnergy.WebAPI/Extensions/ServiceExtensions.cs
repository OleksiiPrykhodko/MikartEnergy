using FluentValidation;
using MikartEnergy.BLL.Services;
using MikartEnergy.Common.DTO.CallbackRequest;
using MikartEnergy.Common.DTO.Pagination;
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
            // ETIM product file reading service registration.
            var etimFilePath = builder.Configuration["EtimXmlFilePath"];
            var pathToAssembly = Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location);
            services.AddSingleton<IEtimProductsXmlReader, EtimProductsXmlReader>(
                x => new EtimProductsXmlReader(pathToAssembly + "\\" + etimFilePath));

            services.AddScoped<CallbackRequestService>();
            services.AddScoped<ProductService>();
            services.AddScoped<ConfiguratorResultService>();
        }

        /// <summary>
        /// Extension method for register FluentValidation.
        /// </summary>
        /// <param name="services"></param>
        public static void RegisterCustomValidators(this IServiceCollection services)
        {
            // CallbackRequests validators.
            services.AddScoped<IValidator<NewCallbackRequestDTO>, NewCallbackRequestDTOValidator>();
            services.AddScoped<IValidator<CallbackRequestDTO>, CallbackRequestDTOValidator>();

            // Pagination validator.
            services.AddScoped<IValidator<PaginationRequestDTO>, PaginationRequestDTOValidator>();
        }

        /// <summary>
        /// Extension method for register file reader services.
        /// </summary>
        /// <param name="services"></param>
        public static void RegisterCustomPermanentFilesReaders(this IServiceCollection services, WebApplicationBuilder builder)
        {
            // ETIM Features and Values file reading service registration.
            var etimFeaturesAndValuesFilePath = builder.Configuration["EtimFeaturesAndValuesXmlFilePath"];
            var pathToAssembly = Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location);

            services.AddSingleton<IEtimFeaturesAndValuesXmlReader, EtimFeaturesAndValuesXmlReader>(
                f => new EtimFeaturesAndValuesXmlReader(pathToAssembly + "\\" + etimFeaturesAndValuesFilePath));
        }

        /// <summary>
        /// Extension method for register Data Base Seeder. 
        /// Register it after DbContext.
        /// </summary>
        /// <param name="services"></param>
        public static void RegisterCustomDataBaseSeeder(this IServiceCollection services)
        {
            services.AddScoped<DbSeederService>();
        }
    }
}
