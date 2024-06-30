using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
            var mikartCorsPolicy = "corsPolicy";
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(mikartCorsPolicy, builder =>
                {
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                    builder.AllowAnyOrigin();
                });
            });

            // Add services for reading data from permanent files like xml.
            builder.Services.RegisterCustomPermanentFilesReaders(builder.Configuration);

            // Add DB Context.
            if (builder.Environment.IsDevelopment())
            {
                // Context for development and testing.
                builder.Services.AddDbContext<MikartContext>(options => 
                    options.UseInMemoryDatabase("MikartInMemoryDB"));
            }
            if (builder.Environment.IsProduction())
            {
                // Context for Production.
                builder.Services.AddDbContext<MikartContext>(options =>
                    options.UseSqlServer(builder.Configuration.GetConnectionString("MikartEnergyDatabase")));
            }

            builder.Services.RegisterCustomDataBaseSeeder();

            // Add business logic services.
            builder.Services.RegisterCustomServices();

            // Add FluentValidation.
            builder.Services.RegisterCustomValidators();

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
                app.UseCors(option => option
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());

                // Calling of DbSeederService method Seed() for DB seeding in extension method. 
                app.UseCustomDbSeederService();

                // Logging of all http/s requests in Development mode if it needed.
                //app.UseSerilogRequestLogging();
            }
            if (app.Environment.IsProduction())
            {
                // Call extension method for adding custom GlobalExceptionHandlingMiddleware.
                app.UseGlobalExceptionHandler();

                // Middleware for NGINX reverse proxy. This middleware updates the Request.Scheme, using the X-Forwarded-Proto header, so that redirect URIs and other security policies work correctly.
                // https://learn.microsoft.com/en-us/aspnet/core/host-and-deploy/linux-nginx?view=aspnetcore-8.0&tabs=linux-ubuntu
                app.UseForwardedHeaders(new ForwardedHeadersOptions
                {
                    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
                });

                // Allow only frontend origin in prodaction mode.
                // TODO: specifie here frontend URL on production mode for CORS
                // app.UseCors(option => option.WithOrigins(""));
                app.UseCors(mikartCorsPolicy);

                app.UseCustomDbSeederService();
            }

            // We don't need it because NGINX reverse proxy are used and is responsible for certificate and https.
            //app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            try
            {
                app.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "App failed to start correctly.");
            }
            finally
            {
                Log.CloseAndFlush();
            }

        }
    }
}