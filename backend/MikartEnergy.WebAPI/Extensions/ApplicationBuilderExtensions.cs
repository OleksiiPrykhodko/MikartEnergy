using MikartEnergy.WebAPI.Middlewares;

namespace MikartEnergy.WebAPI.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder AddGlobalErrorHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GlobalExceptionHandlingMiddleware>();
        }
    }
}
