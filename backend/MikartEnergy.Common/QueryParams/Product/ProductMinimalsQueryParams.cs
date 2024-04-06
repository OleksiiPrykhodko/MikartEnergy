using MikartEnergy.Common.QueryParams.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikartEnergy.Common.QueryParams.Product
{
    public class ProductMinimalsQueryParams : PaginationQueryParams
    {
        public string? OrderNumber { get; set; } = null;
        public string? ProductName { get; set; } = null;
        public string? SortBy { get; set; } = null;
        public bool OrderIsDescending { get; set; } = false;
    }
}
