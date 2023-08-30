using Microsoft.EntityFrameworkCore;
using MikartEnergy.DAL.Context.ETIM_files_reading;
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
        public static void Seed(this ModelBuilder modelBuilder, IEtimFeaturesAndValuesXmlReader etimFeaturesAndValuesXmlReader)
        {
            // Adding of CallbackRequest entities to the base on app loading.
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

            // Adding of EtimFeature entities to the base on app loading.
            var _etimFeatures = etimFeaturesAndValuesXmlReader.GetFeatures().ToList();
            _etimFeatures.ForEach(f => f.Id = Guid.NewGuid());

            if (etimFeaturesAndValuesXmlReader.CountFeatures() == 0)
            {
                throw new Exception("Xml file with all Features and all Values can't be empty.");
            }
            modelBuilder.Entity<EtimFeature>().HasData(_etimFeatures);

            // Adding of EtimValue entities to the base on app loading.
            var _etimValues = etimFeaturesAndValuesXmlReader.GetValues().ToList();
            _etimValues.ForEach(v => v.Id = Guid.NewGuid());

            if (etimFeaturesAndValuesXmlReader.CountValues() == 0)
            {
                throw new Exception("Xml file with all Features and all Values can't be empty.");
            }
            modelBuilder.Entity<EtimValue>().HasData(_etimValues);
        }


    }
}
