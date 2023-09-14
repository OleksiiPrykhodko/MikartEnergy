using MikartEnergy.DAL.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikartEnergy.DAL.Entities
{
    public class UnknownProduct : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Guid TiaStConfiguratorResultId { get; set; }
        public TiaStConfiguratorResult TiaStConfiguratorResult { get; set; }
        public int Quantity { get; set; }
    }
}
