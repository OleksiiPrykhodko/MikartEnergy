using Microsoft.EntityFrameworkCore;
using MikartEnergy.DAL.Context;
using MikartEnergy.WebAPI.Extensions;
using Serilog;

namespace MikartEnergy.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Add CORS.
            builder.Services.AddCors();

            // Add services for reading data from permanent files like xml.
            builder.Services.RegisterCustomPermanentFilesReaders(builder);

            // Add DB Context.
            builder.Services.AddDbContext<MikartContext>(options => options.UseInMemoryDatabase("MikartInMemoryDB"));

            // Add business logic services.
            builder.Services.RegisterCustomServices(builder);

            // Add FluentValidation.
            builder.Services.RegisterCustomValidators();

            builder.Services.RegisterCustomDataBaseSeeder();

            // Add Serilog. This call will redirect all log events through Serilog pipeline.
            builder.Host.UseSerilog((context, configuration) =>
            {
                configuration.ReadFrom.Configuration(context.Configuration);
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                // Configure the HTTP request pipeline.
                app.UseSwagger();
                app.UseSwaggerUI();

                // Allow any origin in dev mode.
                app.UseCors(option => option.AllowAnyOrigin());

                // Calling of DbSeederService method Seed() for DB seeding in extension method. 
                app.UseCustomDbSeederService();

                //Logging of all http/s requests in Development mode if it needed.
                //app.UseSerilogRequestLogging();
            }
            if (app.Environment.IsProduction())
            {
                // Allow only frontend origin in prodaction mode.
                // TODO: specifie here frontend URL on production mode for CORS
                app.UseCors(option => option.WithOrigins(""));

                // Call extension method for adding custom GlobalExceptionHandlingMiddleware.
                app.UseGlobalExceptionHandler();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}