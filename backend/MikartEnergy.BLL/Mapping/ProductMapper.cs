using MikartEnergy.Common.DTO.CallbackRequest;
using MikartEnergy.Common.DTO.Product;
using MikartEnergy.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikartEnergy.BLL.Mapping
{
    public static class ProductMapper
    {
        public static ProductDTO ToProductDTO(this EtimProduct entity)
        {
            return new ProductDTO
            {
                SupplierPID = entity.SupplierPID,
                ManufacturerName = entity.ManufacturerName,
                OrderNumber = entity.OrderNumber,
                ProductName = entity.ProductName,
                ShortDescription = entity.ShortDescription,
                LongDescription = entity.LongDescription,
                TechnicalData = entity.TechnicalData,

                ImageLowQualityURL = entity.ImageLowQualityURL,
                ImageHighQualityURL = entity.ImageHighQualityURL,
                PdfWith3dURL = entity.PdfWith3dURL,

                LinkToProductPage = entity.LinkToProductPage,
                LinkToManuals = entity.LinkToManuals,
                LinkToFAQ = entity.LinkToFAQ,
                LinkToTechnicalData = entity.LinkToTechnicalData,
                LinkToApplicationExample = entity.LinkToApplicationExample,
                LinkToVideo = entity.LinkToVideo,

                MinimalOrderQuantity = entity.MinimalOrderQuantity,
                MaximalOrderQuantity = entity.MaximalOrderQuantity,
                OrderQuantityMultiplier = entity.OrderQuantityMultiplier,
                InStock = entity.InStock,
                Price = entity.Price,
                PriceCurrency = entity.PriceCurrency,
            };
        }

        public static ProductMinimalDTO ToProductMinimalDTO(this EtimProduct entity)
        {
            return new ProductMinimalDTO
            {
                SupplierPID = entity.SupplierPID,
                ManufacturerName = entity.ManufacturerName,
                OrderNumber = entity.OrderNumber,
                ProductName = entity.ProductName,
                ShortDescription = entity.ShortDescription,
                LongDescription = entity.LongDescription,

                ImageLowQualityURL = entity.ImageLowQualityURL,

                InStock = entity.InStock,
                Price = entity.Price,
                PriceCurrency = entity.PriceCurrency
            };
        }

        public static ProductDTO ToProductDTO(this Product entity)
        {
            return new ProductDTO
            {
                SupplierPID = entity.SupplierPID,
                ManufacturerName = entity.ManufacturerName,
                OrderNumber = entity.OrderNumber,
                ProductName = entity.ProductName,
                ShortDescription = entity.ShortDescription,
                LongDescription = entity.LongDescription,
                TechnicalData = entity.TechnicalData.Select(td =>
                {
                    var technicalFeature = td.TechnicalFeature.Description;
                    var technicalValues = td.TechnicalValues.Select(tv => tv.Description);
                    var dataLine = new KeyValuePair<string, IEnumerable<string>>(technicalFeature, technicalValues);
                    return dataLine;
                }),

                ImageLowQualityURL = entity.ImageLowQualityURL,
                ImageHighQualityURL = entity.ImageHighQualityURL,
                PdfWith3dURL = entity.PdfWith3dURL,

                LinkToProductPage = entity.LinkToProductPage,
                LinkToManuals = entity.LinkToManuals,
                LinkToFAQ = entity.LinkToFAQ,
                LinkToTechnicalData = entity.LinkToTechnicalData,
                LinkToApplicationExample = entity.LinkToApplicationExample,
                LinkToVideo = entity.LinkToVideo,

                MinimalOrderQuantity = entity.MinimalOrderQuantity,
                MaximalOrderQuantity = entity.MaximalOrderQuantity,
                OrderQuantityMultiplier = entity.OrderQuantityMultiplier,
                InStock = entity.InStock,
                Price = entity.Price,
                PriceCurrency = entity.PriceCurrency,

                RelatedProducts = entity.RelatedProducts.Select(rp => rp.ToProductMinimalDTO())
            };
        }

        public static ProductMinimalDTO ToProductMinimalDTO(this Product entity)
        {
            return new ProductMinimalDTO
            {
                SupplierPID = entity.SupplierPID,
                ManufacturerName = entity.ManufacturerName,
                OrderNumber = entity.OrderNumber,
                ProductName = entity.ProductName,
                ShortDescription = entity.ShortDescription,
                LongDescription = entity.LongDescription,

                ImageLowQualityURL = entity.ImageLowQualityURL,

                InStock = entity.InStock,
                Price = entity.Price,
                PriceCurrency = entity.PriceCurrency
            };
        }

    }
}
