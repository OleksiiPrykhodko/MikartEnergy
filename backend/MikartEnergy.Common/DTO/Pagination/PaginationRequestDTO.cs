using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace MikartEnergy.Common.DTO.Pagination
{
    public class PaginationRequestDTO
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

}
