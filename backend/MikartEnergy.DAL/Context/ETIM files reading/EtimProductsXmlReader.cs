using MikartEnergy.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MikartEnergy.DAL.Context.ETIM_files_reading
{
    public class EtimProductsXmlReader : IEtimProductsFileReader
    {
        private readonly string _pathToFile;
        private IEnumerable<Product> _products;

        public EtimProductsXmlReader(string pathToFile)
        {
            _pathToFile = pathToFile;
        }

        public IEnumerable<Product> GetProducts()
        {
            if (_pathToFile is null)
            {
                throw new NullReferenceException($"{nameof(_pathToFile)} cann not be null.");
            }
            if (string.IsNullOrWhiteSpace(_pathToFile))
            {
                throw new ArgumentOutOfRangeException($"{nameof(_pathToFile)} cann not be empty or white space.");
            }

            if (_products is null)
            {
                _products = GetProductsFromEtimXmlFile();
                return _products;
            }
            return _products;
        }

        /*
        private async Task<IEnumerable<Product>> GetProductsFromEtimXmlFile1()
        {

            var xDocument = await XDocument.LoadAsync(_pathToFile);

            return xDocument.Descendants().Where(d => d.Name.LocalName == "PRODUCT")
                .Select(p => {
                    var elements = p.Elements();
                    return new
                    {
                        id = elements.First().Value,
                        RODUCT_DETAILS = elements.First(e => e.Name.LocalName == "PRODUCT_DETAILS"),
                        FEATURE = elements.First(e => e.Name.LocalName == "PRODUCT_FEATURES").Elements().Where(e => e.Name.LocalName == "FEATURE"),
                        PRODUCT_ORDER_DETAILS = elements.First(e => e.Name.LocalName == "PRODUCT_ORDER_DETAILS"),
                        PRODUCT_PRICE = elements.First(e => e.Name.LocalName == "PRODUCT_PRICE_DETAILS").Elements().First(e => e.Name.LocalName == "PRODUCT_PRICE"),
                        UDX_EDXF_MIME_INFO = elements.First(e => e.Name.LocalName == "USER_DEFINED_EXTENSIONS").Elements().FirstOrDefault(e => e.Name.LocalName == "UDX.EDXF.MIME_INFO"), // not all products have <UDX.EDXF.MIME_INFO> - 110330
                                                                                                                                                                                          // Not all products have PRODUCT_REFERENCE, so it give empty IEnumerable.
                        PRODUCT_REFERENCE = elements.Where(e => e.Name.LocalName == "PRODUCT_REFERENCE").Select(e => e.Elements().First().Value)
                    };
                }).Select(c => new Product()
                {
                    Id = c.id,
                    ManufacturerName = c.RODUCT_DETAILS.Elements().First(e => e.Name.LocalName == "MANUFACTURER_NAME").Value,
                    OrderNumber = c.RODUCT_DETAILS.Elements().First(e => e.Name.LocalName == "SUPPLIER_ALT_PID").Value,
                    ProductName = c.RODUCT_DETAILS.Elements().First(e => e.Name.LocalName == "MANUFACTURER_TYPE_DESCR").Value,
                    ShortDescription = c.RODUCT_DETAILS.Elements().First(e => e.Name.LocalName == "DESCRIPTION_SHORT").Value,
                    LongDescription = c.RODUCT_DETAILS.Elements().First(e => e.Name.LocalName == "DESCRIPTION_LONG").Value,

                    TechnicalData = c.FEATURE
                        .Select(f => new KeyValuePair<string, IEnumerable<string>>(f.Elements().First().Value, f.Elements().Where(e => e.Name.LocalName == "FVALUE").Select(e => e.Value))),

                    ImageHighQualityURL = c.UDX_EDXF_MIME_INFO?.Elements().FirstOrDefault(e => e.Elements().FirstOrDefault(e => e.Name.LocalName == "UDX.EDXF.MIME_SOURCE")?.Value.EndsWith("P.png") ?? false)?.Elements().First(e => e.Name.LocalName == "UDX.EDXF.MIME_SOURCE").Value ?? string.Empty,
                    ImageLowQualityURL = c.UDX_EDXF_MIME_INFO?.Elements().FirstOrDefault(e => e.Elements().FirstOrDefault(e => e.Name.LocalName == "UDX.EDXF.MIME_SOURCE")?.Value.EndsWith("I.jpg") ?? false)?.Elements().First(e => e.Name.LocalName == "UDX.EDXF.MIME_SOURCE").Value ?? string.Empty,
                    PdfWith3dURL = c.UDX_EDXF_MIME_INFO?.Elements().FirstOrDefault(e => e.Elements().FirstOrDefault(e => e.Name.LocalName == "UDX.EDXF.MIME_ALT")?.Value.ToLower().Contains("3d pdf") ?? false)?.Elements().First(e => e.Name.LocalName == "UDX.EDXF.MIME_SOURCE").Value ?? string.Empty,
                    LinkToProductPage = c.UDX_EDXF_MIME_INFO?.Elements().FirstOrDefault(e => e.Elements().FirstOrDefault(e => e.Name.LocalName == "UDX.EDXF.MIME_ALT")?.Value.ToLower().Contains("link to product page") ?? false)?.Elements().First(e => e.Name.LocalName == "UDX.EDXF.MIME_SOURCE").Value ?? string.Empty,
                    LinkToManuals = c.UDX_EDXF_MIME_INFO?.Elements().FirstOrDefault(e => e.Elements().FirstOrDefault(e => e.Name.LocalName == "UDX.EDXF.MIME_CODE")?.Value.ToLower().Contains("md32") ?? false)?.Elements().First(e => e.Name.LocalName == "UDX.EDXF.MIME_SOURCE").Value ?? string.Empty,
                    LinkToFAQ = c.UDX_EDXF_MIME_INFO?.Elements().FirstOrDefault(e => e.Elements().FirstOrDefault(e => e.Name.LocalName == "UDX.EDXF.MIME_ALT")?.Value.ToLower().Contains("link to faq") ?? false)?.Elements().First(e => e.Name.LocalName == "UDX.EDXF.MIME_SOURCE").Value ?? string.Empty,
                    LinkToTechnicalData = c.UDX_EDXF_MIME_INFO?.Elements().FirstOrDefault(e => e.Elements().FirstOrDefault(e => e.Name.LocalName == "UDX.EDXF.MIME_ALT")?.Value.ToLower().Contains("link to technical data") ?? false)?.Elements().First(e => e.Name.LocalName == "UDX.EDXF.MIME_SOURCE").Value ?? string.Empty,
                    LinkToApplicationExample = c.UDX_EDXF_MIME_INFO?.Elements().FirstOrDefault(e => e.Elements().FirstOrDefault(e => e.Name.LocalName == "UDX.EDXF.MIME_ALT")?.Value.ToLower().Contains("link to application example") ?? false)?.Elements().First(e => e.Name.LocalName == "UDX.EDXF.MIME_SOURCE").Value ?? string.Empty,
                    LinkToVideo = c.UDX_EDXF_MIME_INFO?.Elements().FirstOrDefault(e => e.Elements().FirstOrDefault(e => e.Name.LocalName == "UDX.EDXF.MIME_CODE")?.Value.ToLower().Contains("md45") ?? false)?.Elements().First(e => e.Name.LocalName == "UDX.EDXF.MIME_SOURCE").Value ?? string.Empty,

                    KeyWords = c.RODUCT_DETAILS.Elements().Where(e => e.Name.LocalName == "KEYWORD").Select(e => e.Value),

                    RelatedProductIDs = c.PRODUCT_REFERENCE,

                    MinimalOrderQuantity = int.Parse(c.PRODUCT_ORDER_DETAILS.Elements().First(e => e.Name.LocalName == "QUANTITY_MIN").Value),
                        OrderQuantityMultiplier = int.Parse(c.PRODUCT_ORDER_DETAILS.Elements().First(e => e.Name.LocalName == "QUANTITY_INTERVAL").Value),

                    Price = int.Parse(c.PRODUCT_PRICE.Elements().First(e => e.Name.LocalName == "PRICE_AMOUNT").Value),
                    PriceCurrency = c.PRODUCT_PRICE.Elements().First(e => e.Name.LocalName == "PRICE_CURRENCY").Value
                });
        }
        */

        private IEnumerable<Product> GetProductsFromEtimXmlFile()
        {
            var xDocument = XDocument.Load(_pathToFile);
            return xDocument.Descendants().Where(d => d.Name.LocalName == "PRODUCT")
                .Select(p => {
                    var elements = p.Elements();
                    return new Product
                    {
                        Id = elements.First().Value,
                        ManufacturerName = GetXElementsByParent(elements, "PRODUCT_DETAILS").First(e => e.Name.LocalName == "MANUFACTURER_NAME").Value,
                        OrderNumber = GetXElementsByParent(elements, "PRODUCT_DETAILS").First(e => e.Name.LocalName == "SUPPLIER_ALT_PID").Value,
                        ProductName = GetXElementsByParent(elements, "PRODUCT_DETAILS").First(e => e.Name.LocalName == "MANUFACTURER_TYPE_DESCR").Value,
                        ShortDescription = GetXElementsByParent(elements, "PRODUCT_DETAILS").First(e => e.Name.LocalName == "DESCRIPTION_SHORT").Value,
                        LongDescription = GetXElementsByParent(elements, "PRODUCT_DETAILS").First(e => e.Name.LocalName == "DESCRIPTION_LONG").Value,

                        TechnicalData = GetXElementsByParent(elements, "PRODUCT_FEATURES").Where(e => e.Name.LocalName == "FEATURE")
                            .Select(f => new KeyValuePair<string, IEnumerable<string>>(f.Elements().First().Value, f.Elements().Where(e => e.Name.LocalName == "FVALUE").Select(e => e.Value))),

                        ImageHighQualityURL = GetXElementsByParent(elements, "USER_DEFINED_EXTENSIONS").FirstOrDefault(e => e.Name.LocalName == "UDX.EDXF.MIME_INFO")?
                            .Elements().FirstOrDefault(e => e.Elements().FirstOrDefault(e => e.Name.LocalName == "UDX.EDXF.MIME_SOURCE")?.Value.EndsWith("P.png") ?? false)?
                            .Elements().First(e => e.Name.LocalName == "UDX.EDXF.MIME_SOURCE").Value ?? string.Empty,
                        ImageLowQualityURL = GetXElementsByParent(elements, "USER_DEFINED_EXTENSIONS").FirstOrDefault(e => e.Name.LocalName == "UDX.EDXF.MIME_INFO")?
                            .Elements().FirstOrDefault(e => e.Elements().FirstOrDefault(e => e.Name.LocalName == "UDX.EDXF.MIME_SOURCE")?.Value.EndsWith("I.jpg") ?? false)?
                            .Elements().First(e => e.Name.LocalName == "UDX.EDXF.MIME_SOURCE").Value ?? string.Empty,
                        PdfWith3dURL = GetXElementsByParent(elements, "USER_DEFINED_EXTENSIONS").FirstOrDefault(e => e.Name.LocalName == "UDX.EDXF.MIME_INFO")?
                            .Elements().FirstOrDefault(e => e.Elements().FirstOrDefault(e => e.Name.LocalName == "UDX.EDXF.MIME_ALT")?.Value.ToLower().Contains("3d pdf") ?? false)?
                            .Elements().First(e => e.Name.LocalName == "UDX.EDXF.MIME_SOURCE").Value ?? string.Empty,
                        LinkToProductPage = GetXElementsByParent(elements, "USER_DEFINED_EXTENSIONS").FirstOrDefault(e => e.Name.LocalName == "UDX.EDXF.MIME_INFO")?
                            .Elements().FirstOrDefault(e => e.Elements().FirstOrDefault(e => e.Name.LocalName == "UDX.EDXF.MIME_ALT")?.Value.ToLower().Contains("link to product page") ?? false)?
                            .Elements().First(e => e.Name.LocalName == "UDX.EDXF.MIME_SOURCE").Value ?? string.Empty,
                        LinkToManuals = GetXElementsByParent(elements, "USER_DEFINED_EXTENSIONS").FirstOrDefault(e => e.Name.LocalName == "UDX.EDXF.MIME_INFO")?
                            .Elements().FirstOrDefault(e => e.Elements().FirstOrDefault(e => e.Name.LocalName == "UDX.EDXF.MIME_CODE")?.Value.ToLower().Contains("md32") ?? false)?
                            .Elements().First(e => e.Name.LocalName == "UDX.EDXF.MIME_SOURCE").Value ?? string.Empty,
                        LinkToFAQ = GetXElementsByParent(elements, "USER_DEFINED_EXTENSIONS").FirstOrDefault(e => e.Name.LocalName == "UDX.EDXF.MIME_INFO")?
                            .Elements().FirstOrDefault(e => e.Elements().FirstOrDefault(e => e.Name.LocalName == "UDX.EDXF.MIME_ALT")?.Value.ToLower().Contains("link to faq") ?? false)?
                            .Elements().First(e => e.Name.LocalName == "UDX.EDXF.MIME_SOURCE").Value ?? string.Empty,
                        LinkToTechnicalData = GetXElementsByParent(elements, "USER_DEFINED_EXTENSIONS").FirstOrDefault(e => e.Name.LocalName == "UDX.EDXF.MIME_INFO")?
                            .Elements().FirstOrDefault(e => e.Elements().FirstOrDefault(e => e.Name.LocalName == "UDX.EDXF.MIME_ALT")?.Value.ToLower().Contains("link to technical data") ?? false)?
                            .Elements().First(e => e.Name.LocalName == "UDX.EDXF.MIME_SOURCE").Value ?? string.Empty,
                        LinkToApplicationExample = GetXElementsByParent(elements, "USER_DEFINED_EXTENSIONS").FirstOrDefault(e => e.Name.LocalName == "UDX.EDXF.MIME_INFO")?
                            .Elements().FirstOrDefault(e => e.Elements().FirstOrDefault(e => e.Name.LocalName == "UDX.EDXF.MIME_ALT")?.Value.ToLower().Contains("link to application example") ?? false)?
                            .Elements().First(e => e.Name.LocalName == "UDX.EDXF.MIME_SOURCE").Value ?? string.Empty,
                        LinkToVideo = GetXElementsByParent(elements, "USER_DEFINED_EXTENSIONS").FirstOrDefault(e => e.Name.LocalName == "UDX.EDXF.MIME_INFO")?
                            .Elements().FirstOrDefault(e => e.Elements().FirstOrDefault(e => e.Name.LocalName == "UDX.EDXF.MIME_CODE")?.Value.ToLower().Contains("md45") ?? false)?
                            .Elements().First(e => e.Name.LocalName == "UDX.EDXF.MIME_SOURCE").Value ?? string.Empty,

                        KeyWords = GetXElementsByParent(elements, "PRODUCT_DETAILS")
                            .Where(e => e.Name.LocalName == "KEYWORD").Select(e => e.Value),

                        RelatedProductIDs = elements.Where(e => e.Name.LocalName == "PRODUCT_REFERENCE").Select(e => e.Elements().First().Value),

                        MinimalOrderQuantity = int.Parse(GetXElementsByParent(elements, "PRODUCT_ORDER_DETAILS").First(e => e.Name.LocalName == "QUANTITY_MIN").Value),
                        OrderQuantityMultiplier = int.Parse(GetXElementsByParent(elements, "PRODUCT_ORDER_DETAILS").First(e => e.Name.LocalName == "QUANTITY_INTERVAL").Value),

                        Price = decimal.Parse(GetXElementsByParent(elements, "PRODUCT_PRICE_DETAILS").First(e => e.Name.LocalName == "PRODUCT_PRICE").Elements().First(e => e.Name.LocalName == "PRICE_AMOUNT").Value),
                        PriceCurrency = GetXElementsByParent(elements, "PRODUCT_PRICE_DETAILS").First(e => e.Name.LocalName == "PRODUCT_PRICE").Elements().First(e => e.Name.LocalName == "PRICE_CURRENCY").Value
                    };
                });
        }

        private IEnumerable<XElement> GetXElementsByParent(IEnumerable<XElement> parentXElements, string parentLocalName)
        {
            return parentXElements.First(e => e.Name.LocalName == parentLocalName).Elements();
        }
    }
}
