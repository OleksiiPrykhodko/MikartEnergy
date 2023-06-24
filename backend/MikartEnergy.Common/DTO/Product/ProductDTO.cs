using MikartEnergy.Common.DTO.Abstract;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MikartEnergy.Common.DTO.Product
{
    public class ProductDTO : ResponseStatusDTO
    {
        public string Id { get; set; }
        public string ManufacturerName { get; set; }
        public string OrderNumber { get; set; }
        public string ProductName { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { set; get; }
        public IEnumerable<KeyValuePair<string, IEnumerable<string>>> TechnicalData { get; set; }

        public string ImageLowQualityURL { get; set; }
        public string ImageHighQualityURL { get; set; }
        public string PdfWith3dURL { get; set; }

        public string LinkToProductPage { get; set; }
        public string LinkToManuals { get; set; }
        public string LinkToFAQ { get; set; }
        public string LinkToTechnicalData { get; set; }
        public string LinkToApplicationExample { get; set; }
        public string LinkToVideo { get; set; }

        public IEnumerable<string> RelatedProductIDs { get; set; }

        public int MinimalOrderQuantity { get; set; } = 1;
        public int MaximalOrderQuantity { get; set; } = int.MaxValue;
        public int OrderQuantityMultiplier { get; set; } = 1;
        public bool InStock { get; set; } = true;
        public decimal Price { get; set; } = 0.00m;
        public string PriceCurrency { get; set; }

    }
}
