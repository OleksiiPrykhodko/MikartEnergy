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

            // Add DB Context.
            builder.Services.AddDbContext<MikartContext>(options => options.UseInMemoryDatabase("MikartInMemoryDB"));

            // Add business logic services
            builder.Services.RegisterCustomServices();
            // Add FluentValidation 
            builder.Services.RegisterCustomValidators();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}