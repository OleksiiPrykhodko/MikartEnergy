using MikartEnergy.Common.DTO.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikartEnergy.Common.DTO.Configurator
{
    public class TiaStProductsOrderDTO
    {
        public Guid Id { get; set; }
        public IEnumerable<KeyValuePair<ProductMinimalDTO, int>> ExistingInDbProducts { get; set; }
        public IEnumerable<KeyValuePair<KeyValuePair<string, string>, int>> NotExistingInDbProducts { get; set; }
    }
}
