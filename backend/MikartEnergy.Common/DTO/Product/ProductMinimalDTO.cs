using MikartEnergy.Common.DTO.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikartEnergy.Common.DTO.Product
{
    public class ProductMinimalDTO
    {
        public string Id { get; set; } = string.Empty;
        public string ManufacturerName { get; set; } = string.Empty;
        public string OrderNumber { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public string ShortDescription { get; set; } = string.Empty;
        public string LongDescription { set; get; } = string.Empty;

        public string ImageLowQualityURL { get; set; } = string.Empty;

        public bool InStock { get; set; } = true;
        public decimal Price { get; set; } = 0.00m;
        public string PriceCurrency { get; set; } = string.Empty;
    }
}
