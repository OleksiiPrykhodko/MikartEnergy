using Microsoft.EntityFrameworkCore;
using MikartEnergy.DAL.Context;
using MikartEnergy.DAL.Context.ETIM_files_reading;
using MikartEnergy.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MikartEnergy.BLL.Services
{
    /// <summary>
    /// DbSeederService is service wich is called onсe at app startup for DB seeding.
    /// </summary>
    public class DbSeederService
    {
        private readonly MikartContext _context;
        private readonly IEtimProductsXmlReader _etimProductsXmlReader;
        private readonly IEtimFeaturesAndValuesXmlReader _etimFeaturesAndValuesXmlReader;

        public DbSeederService(
            MikartContext context,
            IEtimProductsXmlReader etimProductsXmlReader,
            IEtimFeaturesAndValuesXmlReader etimFeaturesAndValuesXmlReader)
        {
            _context = context;
            _etimProductsXmlReader = etimProductsXmlReader;
            _etimFeaturesAndValuesXmlReader = etimFeaturesAndValuesXmlReader;
        }

        /// <summary>
        /// Method seeds Data Base.
        /// Call it after call WebApplicationBuilder method build - Build().
        /// </summary>
        public void Seed()
        {
            // Skip seeding if DB contains any entity
            if (_context.Products.Any() ||
                _context.Keywords.Any() ||
                _context.TechnicalFeatures.Any() ||
                _context.TechnicalValues.Any() ||
                _context.TechnicalDatas.Any() ||
                _context.CallbackRequests.Any())
            {
                return;
            }

            // Get all products from etim file.
            var etimProducts = _etimProductsXmlReader.GetProducts().ToList();
            if (_etimProductsXmlReader.Count() == 0)
            {
                throw new Exception("Xml file with all Products can't be empty.");
            }

            // Adding of Keywords entities to the DB on app loading.
            var keywords = etimProducts.SelectMany(eP => eP.KeyWords)
                .Distinct().Select(kw => new Keyword
                {
                    Id = Guid.NewGuid(),
                    Word = kw
                });

            _context.Keywords.AddRange(keywords);
            _context.SaveChanges();


            // Adding of TechnicalFeature entities to the DB on app loading.
            var etimFeatures = _etimFeaturesAndValuesXmlReader.GetFeatures().ToList();
            if (_etimFeaturesAndValuesXmlReader.CountFeatures() == 0)
            {
                throw new Exception("Xml file with all Features and all Values can't be empty.");
            }

            var technicalEtimFeatures = etimFeatures.Select(f => new TechnicalFeature
            {
                Id = Guid.NewGuid(),
                Description = f.Description,
                EtimCode = f.Code,
                EtimType = f.Type,
                EtimDeprecated = f.Deprecated,
            }).ToList();

            var technicalNotEtimFeatures = etimProducts.SelectMany(p => p.TechnicalData)
                .Select(td => td.Key).Distinct()
                .ExceptBy(technicalEtimFeatures.Select(tF => tF.EtimCode), k => k)
                .Select(someFeature => new TechnicalFeature
                {
                    Id = Guid.NewGuid(),
                    Description = someFeature,
                    EtimCode = string.Empty,
                    EtimType = string.Empty,
                    EtimDeprecated = false
                });

            technicalEtimFeatures.AddRange(technicalNotEtimFeatures);
            _context.TechnicalFeatures.AddRange(technicalEtimFeatures);
            _context.SaveChanges();


            // Adding of TechnicalValue entities to the DB on app loading.
            var etimValues = _etimFeaturesAndValuesXmlReader.GetValues().ToList();
            if (_etimFeaturesAndValuesXmlReader.CountValues() == 0)
            {
                throw new Exception("Xml file with all Features and all Values can't be empty.");
            }

            var technicalEtimValues = etimValues.Select(v => new TechnicalValue
            {
                Id = Guid.NewGuid(),
                Description = v.Description,
                EtimCode = v.Code,
                EtimDeprecated = v.Deprecated
            }).ToList();

            var technicalNotEtimValues = etimProducts.SelectMany(p => p.TechnicalData)
                .SelectMany(td => td.Value).Distinct()
                .ExceptBy(technicalEtimValues.Select(tV => tV.EtimCode), k => k)
                .Select(someValue => new TechnicalValue
                {
                    Id = Guid.NewGuid(),
                    Description = someValue,
                    EtimCode = string.Empty,
                    EtimDeprecated = false
                });

            technicalEtimValues.AddRange(technicalNotEtimValues);
            _context.TechnicalValues.AddRange(technicalEtimValues);
            _context.SaveChanges();


            // First step of Product entities adding to the DB on app loading.
            var products = etimProducts.Select(etimP =>
            {
                var p = new Product
                {
                    Id = Guid.NewGuid(),
                    SupplierPID = etimP.SupplierPID,
                    ManufacturerName = etimP.ManufacturerName,
                    OrderNumber = etimP.OrderNumber,
                    ProductName = etimP.ProductName,
                    ShortDescription = etimP.ShortDescription,
                    LongDescription = etimP.LongDescription,
                    ImageLowQualityURL = etimP.ImageLowQualityURL,
                    ImageHighQualityURL = etimP.ImageHighQualityURL,
                    PdfWith3dURL = etimP.PdfWith3dURL,
                    LinkToProductPage = etimP.LinkToProductPage,
                    LinkToManuals = etimP.LinkToManuals,
                    LinkToFAQ = etimP.LinkToFAQ,
                    LinkToTechnicalData = etimP.LinkToTechnicalData,
                    LinkToApplicationExample = etimP.LinkToApplicationExample,
                    LinkToVideo = etimP.LinkToVideo,
                    MinimalOrderQuantity = etimP.MinimalOrderQuantity,
                    MaximalOrderQuantity = etimP.MaximalOrderQuantity,
                    OrderQuantityMultiplier = etimP.OrderQuantityMultiplier,
                    Price = etimP.Price,
                    PriceCurrency = etimP.PriceCurrency,
                    InStock = etimP.InStock
                };

                var productKeywords = etimP.KeyWords.Select(w => keywords.First(kw => kw.Word == w));
                p.Keywords.AddRange(productKeywords);

                // Create new TechnicalData of current product, add it to the DB table
                // and bind it with current product.
                // Skip all technical data with empty value (Value == "-").
                var technicalDatas = etimP.TechnicalData.ToList();
                foreach (var data in technicalDatas)
                {
                    // Skip technical data with empty value(Value == "-").
                    if (data.Value.Any(v => v == "-"))
                    {
                        continue;
                    }

                    var productTechnicalData = new TechnicalData
                    {
                        Id = Guid.NewGuid(),                   
                        TechnicalFeature = technicalEtimFeatures.First(f => f.EtimCode == data.Key || f.Description == data.Key)
                    };

                    var productTechnicalValues = data.Value
                        .Select(v => technicalEtimValues
                            .First(tv => tv.EtimCode == v || tv.Description == v));
                    productTechnicalData.TechnicalValues.AddRange(productTechnicalValues);

                    p.TechnicalData.Add(productTechnicalData);
                }

                return p;
            }).ToList();

            _context.Products.AddRange(products);

            // Adding of related products to each products.
            products.ForEach(product =>
            {
                var etimProduct = etimProducts.First(etimProduct => etimProduct.SupplierPID == product.SupplierPID);
                etimProduct.RelatedProductIDs.ToList().ForEach(relatedProductId =>
                {
                    var relatedProduct = products.First(prod => prod.SupplierPID == relatedProductId);
                    product.RelatedProducts.Add(relatedProduct); 
                });
            });

            // Adding of CallbackRequests entities to the DB on app loading.
            var callbackRequests = new List<CallbackRequest>
            {
                new CallbackRequest()
                {
                    Id = Guid.NewGuid(),
                    AuthorFirstName = "Jim",
                    AuthorLastName = "Bim",
                    AuthorEmail = "j.bim@bim.us",
                    AuthorPhone = "+380668012710",
                    Message = "I want to build the best machine in the world.",
                    IntrerestedIn = "Project work.",
                    Budget = 30000,
                    IsDeleted = false,
                    InWork = true
                },
                new CallbackRequest()
                {
                    Id = Guid.NewGuid(),
                    AuthorFirstName = "Bill",
                    AuthorLastName = "Gates",
                    AuthorEmail = "bill@microsoft.com",
                    AuthorPhone = "+78123435675",
                    Message = "I want to cooperate with you.",
                    IntrerestedIn = "Cooperating",
                    Budget = 1000000,
                    IsDeleted = false,
                    InWork = false
                },
                new CallbackRequest()
                {
                    Id = Guid.NewGuid(),
                    AuthorFirstName = "Olga",
                    AuthorLastName = "Pupa",
                    AuthorEmail = "olga.pup@gmail.com",
                    AuthorPhone = "88008882525",
                    Message = "I am Ola Pupa and I want pup.",
                    IntrerestedIn = "I want to fiend job.",
                    Budget = 10000,
                    IsDeleted = true,
                    InWork = false
                }
            };
            _context.CallbackRequests.AddRange(callbackRequests);
            _context.SaveChanges();
        }
    }
}
