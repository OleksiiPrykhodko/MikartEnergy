using Microsoft.EntityFrameworkCore;
using MikartEnergy.DAL.Entities;
using System;
using System.Collections.Generic;
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
            // Use it for not in memory DB
            // modelBuilder.Entity<CallbackRequest>().Property(p => p.Id).HasDefaultValueSql("NEWID()");
        }

        // DB seeding.
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CallbackRequest>().HasData(
                new CallbackRequest()
                {
                    Id = Guid.NewGuid(),
                    AuthorFirstName = "Jim",
                    AuthorLastName = "Bim",
                    AuthorEmail = "j.bim@bim.us",
                    AuthorPhone = "+380668012710",
                    Message = "I want to build the best machine in the world.",
                    IntrerestedIn = "Project work.",
                    Budget = 30000,
                    IsDeleted = false,
                    InWork = true
                },
                new CallbackRequest()
                {
                    Id = Guid.NewGuid(),
                    AuthorFirstName = "Bill",
                    AuthorLastName = "Gates",
                    AuthorEmail = "bill@microsoft.com",
                    AuthorPhone = "+78123435675",
                    Message = "I want to cooperate with you.",
                    IntrerestedIn = "Cooperating",
                    Budget = 1000000,
                    IsDeleted = false,
                    InWork = false
                },
                new CallbackRequest()
                {
                    Id = Guid.NewGuid(),
                    AuthorFirstName = "Olga",
                    AuthorLastName = "Pupa",
                    AuthorEmail = "olga.pup@gmail.com",
                    AuthorPhone = "88008882525",
                    Message = "I am Ola Pupa and I want pup.",
                    IntrerestedIn = "I want to fiend job.",
                    Budget = 10000,
                    IsDeleted = true,
                    InWork = false
                });
        }


    }
}
