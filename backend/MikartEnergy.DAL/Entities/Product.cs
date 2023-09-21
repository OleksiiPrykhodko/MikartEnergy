using MikartEnergy.DAL.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikartEnergy.DAL.Entities
{
    public class Product : BaseEntity
    {
        [MaxLength(60)]
        public string SupplierPID { get; set; } = string.Empty;
        [MaxLength(20)]
        public string ManufacturerName { get; set; } = string.Empty;
        [MaxLength(60)]
        public string OrderNumber { get; set; } = string.Empty;
        [MaxLength(256)]
        public string ProductName { get; set; } = string.Empty;
        public string ShortDescription { get; set; } = string.Empty;
        public string LongDescription { set; get; } = string.Empty;

        public string ImageLowQualityURL { get; set; } = string.Empty;
        public string ImageHighQualityURL { get; set; } = string.Empty;
        public string PdfWith3dURL { get; set; } = string.Empty;

        public string LinkToProductPage { get; set; } = string.Empty;
        public string LinkToManuals { get; set; } = string.Empty;
        public string LinkToFAQ { get; set; } = string.Empty;
        public string LinkToTechnicalData { get; set; } = string.Empty;
        public string LinkToApplicationExample { get; set; } = string.Empty;
        public string LinkToVideo { get; set; } = string.Empty;

        public int MinimalOrderQuantity { get; set; } = 1;
        public int MaximalOrderQuantity { get; set; } = int.MaxValue;
        public int OrderQuantityMultiplier { get; set; } = 1;
        public bool InStock { get; set; } = true;
        public decimal Price { get; set; } = 0.00m;
        [MaxLength(10)]
        public string PriceCurrency { get; set; } = string.Empty;

        // Related entities.
        public List<Keyword> Keywords { get; set; } = new();
        public List<TechnicalData> TechnicalData { get; set; } = new();
        public List<ProductOrderQuantity> ProductOrderQuantitys { get; set; } = new();
        public List<Product> RelatedProducts { get; set; } = new();

    }
}
