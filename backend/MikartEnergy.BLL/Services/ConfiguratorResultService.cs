using MikartEnergy.Common.DTO.Configurator;
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
        List<TiaStConfiguratorResult> ConfiguratorResults = new List<TiaStConfiguratorResult>();

        public ConfiguratorResultService() 
        { 
        
        }

        public Guid CreateConfiguratorResult(TiaStResultDTO[] tiaStResult)
        {


            var resultGuid = Guid.NewGuid();
            while(ConfiguratorResults.Any(r => r.Id.Equals(resultGuid)))
            {
                resultGuid = Guid.NewGuid();
            }
            var createdResult = new TiaStConfiguratorResult()
            {
                Id = resultGuid

            };

            return resultGuid;
        }

    }
}
