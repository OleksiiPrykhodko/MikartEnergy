using Microsoft.EntityFrameworkCore;
using MikartEnergy.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikartEnergy.DAL.Context
{
    public class MikartContext : DbContext
    {
        public DbSet<CallbackRequest> callbackRequests { get; private set; }

        public MikartContext(DbContextOptions<MikartContext> options): base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            try
            {
                // Setting up entities using extension method.
                modelBuilder.Configure();

                // Seeding data using extension method.
                // TODO: Is this method will be called every time after adding a new migration? 
                modelBuilder.Seed();
            }
            catch (System.Exception e)
            {
                var text = e.Message;
                throw;
            }
        }
    }
}
