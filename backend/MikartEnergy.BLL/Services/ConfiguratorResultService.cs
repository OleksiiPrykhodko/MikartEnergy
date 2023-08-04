using MikartEnergy.BLL.Mapping;
using MikartEnergy.Common.DTO.CallbackRequest;
using MikartEnergy.Common.DTO.Configurator;
using MikartEnergy.Common.DTO.Product;
using MikartEnergy.Common.Enums;
using MikartEnergy.Common.Models.Result;
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
        private readonly List<TiaStConfiguratorResult> _configuratorResults = new List<TiaStConfiguratorResult>();
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
            var notExistingProducts = new List<KeyValuePair<KeyValuePair<string, string>, int>>();
            foreach (var result in tiaStResult)
            {
                
                if (_productsList.Any(p => p.OrderNumber.ToUpper() == result.MANUFACTURER_PID.ToUpper()))
                {
                    var pair = new KeyValuePair<string, int>(result.MANUFACTURER_PID, int.Parse(result.QUANTITY));
                    existingProducts.Add(pair);
                }
                else
                {
                    var pair = new KeyValuePair<KeyValuePair<string, string>, int>(new KeyValuePair<string, string>(result.MANUFACTURER_PID, result.MANUFACTURER_TYPE_DESCR), int.Parse(result.QUANTITY));
                    notExistingProducts.Add(pair);
                }
            }

            // Create unique Id with check.
            var resultGuid = Guid.NewGuid();
            while (_configuratorResults.Any(r => r.Id.Equals(resultGuid)))
            {
                resultGuid = Guid.NewGuid();
            }
            
            createdResult.Id = resultGuid;
            createdResult.ExistingInDbProducts = existingProducts;
            createdResult.NotExistingInDbProducts = notExistingProducts;

            _configuratorResults.Add(createdResult);

            return createdResult.Id;
        }

        public ResultModel<TiaStProductsOrderDTO> GetConfiguratorResultByID(Guid id)
        {
            var configuratorResult = _configuratorResults.FirstOrDefault(r => r.Id == id);
            
            if (configuratorResult is null)
            {
                var emptyResult = new TiaStProductsOrderDTO() { Id = id };
                var resultWithError = new ResultModel<TiaStProductsOrderDTO>(emptyResult);
                resultWithError.AddErrorToDTO(ResponseError.NotFound.ToString(), "Configuratin result was not found by ID.");
                return resultWithError;
            }

            var order = new TiaStProductsOrderDTO() { Id = id };
            order.ExistingInDbProducts = configuratorResult.ExistingInDbProducts
                .Select(p => new KeyValuePair<ProductMinimalDTO, int>(_productsList.First(product => product.OrderNumber.ToUpper() == p.Key.ToUpper()).ToProductMinimalDTO(), p.Value));
            order.NotExistingInDbProducts = configuratorResult.NotExistingInDbProducts;

            return new ResultModel<TiaStProductsOrderDTO>(order);
        }

    }
}
