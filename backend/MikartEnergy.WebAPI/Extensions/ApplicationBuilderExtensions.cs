using MikartEnergy.BLL.Services;
using MikartEnergy.WebAPI.Middlewares;

namespace MikartEnergy.WebAPI.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GlobalExceptionHandlingMiddleware>();
        }

        public static void UseCustomDbSeederService(this IApplicationBuilder builder)
        {
            using (var scope = builder.ApplicationServices.CreateScope())
            {
                var seederService = scope.ServiceProvider.GetService<DbSeederService>();
                seederService!.Seed();
            }
        }
    }
}
