using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using MikartEnergy.BLL.Services;
using MikartEnergy.DAL.Context;
using MikartEnergy.DAL.Context.ETIM_files_reading;
using MikartEnergy.WebAPI.Extensions;
using System.Reflection;

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

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                // Configure the HTTP request pipeline.
                app.UseSwagger();
                app.UseSwaggerUI();

                // Allow any origin in dev mode.
                app.UseCors(option => option.AllowAnyOrigin());

                // Call DbSeederService method Seed() for DB seeding on app start. 
                using (var scope = app.Services.CreateScope())
                {
                    var seederService = scope.ServiceProvider.GetService<DbSeederService>();
                    seederService!.Seed();
                }
            }
            if (app.Environment.IsProduction())
            {
                // Allow only frontend origin in prodaction mode.
                // TODO: specifie here frontend URL on production mode for CORS
                app.UseCors(option => option.WithOrigins(""));
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}