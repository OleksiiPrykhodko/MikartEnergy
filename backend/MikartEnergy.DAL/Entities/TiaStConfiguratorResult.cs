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
        public IEnumerable<KeyValuePair<string, int>> ExistingInDbProducts { get; set; }
        public IEnumerable<KeyValuePair<string, int>> NotExistingInDbProducts { get; set; }
    }
}
