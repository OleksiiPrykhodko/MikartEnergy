using MikartEnergy.DAL.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikartEnergy.DAL.Entities
{
    public class ProductOrderQuantity : BaseEntity
    {
        public Guid TiaStConfiguratorResultId { get; set; }
        public TiaStConfiguratorResult TiaStConfiguratorResult { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }

    }
}
