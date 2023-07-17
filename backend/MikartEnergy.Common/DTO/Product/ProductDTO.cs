namespace MikartEnergy.Common.DTO.Product
{
    public class ProductDTO
    {
        public string Id { get; set; } = string.Empty;
        public string ManufacturerName { get; set; } = string.Empty;
        public string OrderNumber { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public string ShortDescription { get; set; } = string.Empty;
        public string LongDescription { set; get; } = string.Empty;
        public IEnumerable<KeyValuePair<string, IEnumerable<string>>> TechnicalData { get; set; }

        public string ImageLowQualityURL { get; set; } = string.Empty;
        public string ImageHighQualityURL { get; set; } = string.Empty;
        public string PdfWith3dURL { get; set; } = string.Empty;

        public string LinkToProductPage { get; set; } = string.Empty;
        public string LinkToManuals { get; set; } = string.Empty;
        public string LinkToFAQ { get; set; } = string.Empty;
        public string LinkToTechnicalData { get; set; } = string.Empty;
        public string LinkToApplicationExample { get; set; } = string.Empty;
        public string LinkToVideo { get; set; } = string.Empty;

        public IEnumerable<ProductMinimalDTO> RelatedProductIDs { get; set; } = new ProductMinimalDTO[0];

        public int MinimalOrderQuantity { get; set; } = 1;
        public int MaximalOrderQuantity { get; set; } = int.MaxValue;
        public int OrderQuantityMultiplier { get; set; } = 1;
        public bool InStock { get; set; } = true;
        public decimal Price { get; set; } = 0.00m;
        public string PriceCurrency { get; set; } = string.Empty;

    }
}
