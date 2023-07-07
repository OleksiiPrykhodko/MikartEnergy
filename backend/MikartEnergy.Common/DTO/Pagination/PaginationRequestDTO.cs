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
        private int _pageSize = 50;
        private int _pageIndex = 1;

        public int PageIndex
        {
            get { return _pageIndex; }
            set { _pageIndex = value < 1 ? 1 : value; }
        }
        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value > _pageSize || value < 1 ? _pageSize : value; }
        }
    }

}
