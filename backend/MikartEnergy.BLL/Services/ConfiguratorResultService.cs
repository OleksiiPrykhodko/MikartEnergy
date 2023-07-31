using MikartEnergy.Common.DTO.Configurator;
using MikartEnergy.DAL.Context.ETIM_files_reading;
using MikartEnergy.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikartEnergy.BLL.Services
{
    public class ConfiguratorResultService
    {
        private List<TiaStConfiguratorResult> ConfiguratorResults = new List<TiaStConfiguratorResult>();
        private readonly IEtimProductsFileReader _etimProductsFileReader;
        private readonly List<Product> _productsList;

        public ConfiguratorResultService(IEtimProductsFileReader etimProductsFileReader) 
        {
            _etimProductsFileReader = etimProductsFileReader;
            _productsList = _etimProductsFileReader.GetProducts().ToList();
        }

        public Guid CreateConfiguratorResult(TiaStResultDTO[] tiaStResult)
        {
            var createdResult = new TiaStConfiguratorResult();

            var existingProducts = new List<KeyValuePair<string, int>>();
            var notExistingProducts = new List<KeyValuePair<string, int>>();
            foreach (var result in tiaStResult)
            {
                var pair = new KeyValuePair<string, int>(result.MANUFACTURER_PID, int.Parse(result.QUANTITY));
                if (_productsList.Any(p => p.OrderNumber.ToUpper() == result.MANUFACTURER_PID.ToUpper()))
                {
                    existingProducts.Add(pair);
                }
                else
                {
                    notExistingProducts.Add(pair);
                }
            }

            // Create unique Id.
            var resultGuid = Guid.NewGuid();
            while (ConfiguratorResults.Any(r => r.Id.Equals(resultGuid)))
            {
                resultGuid = Guid.NewGuid();
            }
            
            createdResult.Id = resultGuid;
            createdResult.ExistingProducts = existingProducts;
            createdResult.NotExistingProducts = notExistingProducts;

            return createdResult.Id;
        }

    }
}
