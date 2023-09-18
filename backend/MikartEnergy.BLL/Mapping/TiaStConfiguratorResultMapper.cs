using MikartEnergy.Common.DTO.Configurator;
using MikartEnergy.Common.DTO.Product;
using MikartEnergy.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MikartEnergy.BLL.Mapping
{
    public static class TiaStConfiguratorResultMapper
    {
        public static TiaStProductsOrderDTO ToTiaStProductsOrderDTO(this TiaStConfiguratorResult entity)
        {
            return new TiaStProductsOrderDTO()
            {
                Id = entity.Id,
                ExistingInDbProducts = entity.ProductOrderQuantitys.Select(poq =>
                {
                    var productMinimalDTO = poq.Product.ToProductMinimalDTO();
                    return new KeyValuePair<ProductMinimalDTO, int>(productMinimalDTO, poq.Quantity);
                }),
                NotExistingInDbProducts = entity.UnknownProducts.Select(unknownP =>
                {
                    var unknownProductNameAndDescription = new KeyValuePair<string, string>(unknownP.Name, unknownP.Description);
                    return new KeyValuePair<KeyValuePair<string, string>, int>(unknownProductNameAndDescription, unknownP.Quantity);
                })
            };
        }
    }
}
