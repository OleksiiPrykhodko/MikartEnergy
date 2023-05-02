using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MikartEnergy.DAL.Entities.Abstract;

namespace MikartEnergy.DAL.Context
{
    public static class ChangeTrackerExtensions
    {
        public static void SetAuditProperties(this ChangeTracker changeTracker)
        {
            changeTracker.DetectChanges();
            IEnumerable<EntityEntry> entities =
                changeTracker
                    .Entries()
                    .Where(t => t.Entity is IBaseEntity && t.State == EntityState.Deleted);

            if (entities.Any())
            {
                foreach (EntityEntry entry in entities)
                {
                    IBaseEntity entity = (IBaseEntity)entry.Entity;
                    entity.IsDeleted = true;
                    entry.State = EntityState.Modified;
                }
            }
        }
    }
}
