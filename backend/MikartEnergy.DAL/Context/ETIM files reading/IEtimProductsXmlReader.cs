using MikartEnergy.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikartEnergy.DAL.Context.ETIM_files_reading
{
    public interface IEtimProductsXmlReader
    {
        IEnumerable<EtimProduct> GetProducts();
        int Count();
    }
}
