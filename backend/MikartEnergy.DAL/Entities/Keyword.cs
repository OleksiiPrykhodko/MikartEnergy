using MikartEnergy.DAL.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikartEnergy.DAL.Entities
{
    public class Keyword: BaseEntity
    {
        public string Word { get; set; } = String.Empty;
        public List<Product> Products { get; set; } = new();
    }
}
