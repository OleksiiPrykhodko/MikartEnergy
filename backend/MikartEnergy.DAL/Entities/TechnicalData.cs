using MikartEnergy.DAL.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikartEnergy.DAL.Entities
{
    public class TechnicalData : BaseEntity
    {
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public Guid TechnicalFeatureId { get; set; }
        public TechnicalFeature TechnicalFeature { get; set; }
        public List<TechnicalValue> TechnicalValues { get; set; } = new();
    }
}
