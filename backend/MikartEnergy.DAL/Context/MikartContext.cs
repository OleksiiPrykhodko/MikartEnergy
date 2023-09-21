using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using MikartEnergy.DAL.Context.ETIM_files_reading;
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
        public DbSet<TechnicalFeature> TechnicalFeatures { get; private set; }
        public DbSet<TechnicalValue> TechnicalValues { get; private set; }
        public DbSet<TechnicalData> TechnicalDatas { get; private set; }
        public DbSet<Product> Products { get; private set; }
        public DbSet<Keyword> Keywords { get; private set; }
        public DbSet<ProductOrderQuantity> ProductOrderQuantitys { get; private set; }
        public DbSet<TiaStConfiguratorResult> TiaStConfiguratorResults { get; private set; }
        public DbSet<UnknownProduct> UnknownProducts { get; private set; }

        public MikartContext(DbContextOptions<MikartContext> options) : base(options) 
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            try
            {
                // Setting up entities using extension method.
                modelBuilder.Configure();
                
                // Seeding data using extension method.
                modelBuilder.Seed();
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
