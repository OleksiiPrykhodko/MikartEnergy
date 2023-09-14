using MikartEnergy.DAL.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikartEnergy.DAL.Entities
{
    public class TechnicalValue : BaseEntity
    {
        public string Description { get; set; } = string.Empty;
        public string EtimCode { get; set; } = string.Empty;
        public bool EtimDeprecated { get; set; } = false;
        public List<TechnicalData> TechnicalData { get; set; } = new();
    }
}
