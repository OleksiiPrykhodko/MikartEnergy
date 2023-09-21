using MikartEnergy.DAL.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikartEnergy.DAL.Entities
{
    public class TiaStConfiguratorResult : BaseEntity
    {
        public List<ProductOrderQuantity> ProductOrderQuantitys { get; set; } = new();
        public List<UnknownProduct> UnknownProducts { get; set; } = new();
    }
}
