using Microsoft.EntityFrameworkCore;
using MikartEnergy.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikartEnergy.DAL.Context
{
    public static class ModelBuilderExtensions
    {
        // Specific configurations for each entity.
        public static void Configure(this ModelBuilder modelBuilder)
        {
            // Use it for not in memory DB
            // modelBuilder.Entity<CallbackRequest>().Property(p => p.Id).HasDefaultValueSql("NEWID()");
        }

        // DB seeding.
        public static void Seed(this ModelBuilder modelBuilder)
        {

        }
    }
}
