using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using MikartEnergy.BLL.Services;
using MikartEnergy.DAL.Context;
using MikartEnergy.WebAPI.Extensions;

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

            // Add DB Context.
            builder.Services.AddDbContext<MikartContext>(options => options.UseInMemoryDatabase("MikartInMemoryDB"));

            // Add business logic services.
            builder.Services.RegisterCustomServices();
            // Add FluentValidation.
            builder.Services.RegisterCustomValidators();

            var app = builder.Build();


            if (app.Environment.IsDevelopment())
            {
                // Configure the HTTP request pipeline.
                app.UseSwagger();
                app.UseSwaggerUI();

                // Allow any origin in dev mode.
                app.UseCors(option => option.AllowAnyOrigin());
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