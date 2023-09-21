using Microsoft.EntityFrameworkCore;
using MikartEnergy.BLL.Mapping;
using MikartEnergy.BLL.Services.Abstract;
using MikartEnergy.Common.DTO.CallbackRequest;
using MikartEnergy.Common.DTO.Configurator;
using MikartEnergy.Common.DTO.Product;
using MikartEnergy.Common.Enums;
using MikartEnergy.Common.Models.Result;
using MikartEnergy.DAL.Context;
using MikartEnergy.DAL.Context.ETIM_files_reading;
using MikartEnergy.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikartEnergy.BLL.Services
{
    public class ConfiguratorResultService : BaseService
    {
        private readonly MikartContext _context;

        public ConfiguratorResultService(MikartContext context) : base()
        {
            _context = context;
        }

        public async Task<Guid> CreateConfiguratorResultAsync(TiaStResultDTO[] tiaStResults)
        {
            var createdResult = new TiaStConfiguratorResult() 
            { 
                Id = Guid.NewGuid()
            };

            foreach(var tiaStResult in tiaStResults)
            {
                var product = await _context.Products.FirstOrDefaultAsync(p => p.OrderNumber.ToUpper() == tiaStResult.MANUFACTURER_PID.ToUpper());

                if (product is not null)
                {
                    var productOrderQuantity = new ProductOrderQuantity()
                    {
                        Id = Guid.NewGuid(),
                        Product = product,
                        Quantity = int.Parse(tiaStResult.QUANTITY)
                    };
                    createdResult.ProductOrderQuantitys.Add(productOrderQuantity);
                }
                else
                {
                    var unknownProduct = new UnknownProduct()
                    {
                        Id = Guid.NewGuid(),
                        Name = tiaStResult.MANUFACTURER_PID,
                        Description = tiaStResult.MANUFACTURER_TYPE_DESCR,
                        Quantity = int.Parse(tiaStResult.QUANTITY)
                    };
                    createdResult.UnknownProducts.Add(unknownProduct);
                }
            }

            await _context.TiaStConfiguratorResults.AddAsync(createdResult);
            await _context.SaveChangesAsync();

            return createdResult.Id;
        }

        public async Task<ResultModel<TiaStProductsOrderDTO>> GetConfiguratorResultByIdAsync(Guid id)
        {
            var configuratorResult = await _context.TiaStConfiguratorResults
                .Include(tia => tia.ProductOrderQuantitys)
                .ThenInclude(poq => poq.Product)
                .Include(tia => tia.UnknownProducts)
                .FirstOrDefaultAsync(result => result.Id == id);

            if (configuratorResult is null)
            {
                var emptyResult = new TiaStProductsOrderDTO() { Id = id };
                var resultWithError = new ResultModel<TiaStProductsOrderDTO>(emptyResult);
                resultWithError.AddErrorToDTO(ResponseError.NotFound.ToString(), "Configuratin result was not found by ID.");
                return resultWithError;
            }

            var configuratorResultDTO = configuratorResult.ToTiaStProductsOrderDTO();
            var resultModel = new ResultModel<TiaStProductsOrderDTO>(configuratorResultDTO);
            return resultModel;
        }

    }
}
