using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikartEnergy.Common.DTO.Configurator
{
    public class TiaStResultDTO
    {
        public string SUPPLIER_ID_DUNS { get; set; }
        public string MANUFACTURER_TYPE_DESCR { get; set; }
        public string INTERNATIONAL_PID { get; set; }
        public string MANUFACTURER_PID { get; set; }
        public string ORDER_UNIT { get; set; }
        public string QUANTITY { get; set; }
    }
}
