using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
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
        public DbSet<CallbackRequest> CallbackRequests { get; private set; }

        public MikartContext(DbContextOptions<MikartContext> options): base(options) 
        {
            // Without this call, Entity Framework do not seed data into the InMemoryDB.
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            try
            {
                // Setting up entities using extension method.
                modelBuilder.Configure();
                
                // Seeding data using extension method.
                // TODO: Is this method will be called every time after adding a new migration? 
                modelBuilder.Seed();

                base.OnModelCreating(modelBuilder);
            }
            catch (System.Exception e)
            {
                var text = e.Message;
                throw;
            }
        }
        

        // Overwriting methods to avoid removing entities from the database. 
        // Use extension method SetAuditProperties() implemented in ChangeTrackerExtensions for it.
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ChangeTracker.SetAuditProperties();
            return await base.SaveChangesAsync(cancellationToken);
        }
        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            ChangeTracker.SetAuditProperties();
            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
        public override int SaveChanges()
        {
            ChangeTracker.SetAuditProperties();
            return base.SaveChanges();
        }
        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            ChangeTracker.SetAuditProperties();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }
    }
}
