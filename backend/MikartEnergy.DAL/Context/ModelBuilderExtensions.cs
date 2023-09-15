using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using MikartEnergy.DAL.Context.ETIM_files_reading;
using MikartEnergy.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;

namespace MikartEnergy.DAL.Context
{
    public static class ModelBuilderExtensions
    {

        // Specific configurations for each entity.
        public static void Configure(this ModelBuilder modelBuilder)
        {
            // Any product cann have related products of same type. 
            // many to many with self-referencing
            modelBuilder.Entity<Product>()
                .HasMany(p => p.RelatedProducts)
                .WithMany();
        }

        // DB seeding.
        public static void Seed(this ModelBuilder modelBuilder)
        {

        }

    }
}
