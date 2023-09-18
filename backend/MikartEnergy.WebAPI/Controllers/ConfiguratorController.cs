using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MikartEnergy.BLL.Services;
using MikartEnergy.Common.DTO.Configurator;
using MikartEnergy.Common.Models.Result;
using MikartEnergy.WebAPI.ModelBinders;

namespace MikartEnergy.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ConfiguratorController : ControllerBase
    {
        private readonly ConfiguratorResultService _configuratorService;

        public ConfiguratorController(ConfiguratorResultService configuratorService)
        {
            _configuratorService = configuratorService;
        }

        // POST api/<ConfiguratorController>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post([ModelBinder(typeof(TiaStOrderModelBinder))] TiaStResultDTO[] result)
        {
            var id = await _configuratorService.CreateConfiguratorResultAsync(result);

            var redirect = new RedirectResult($"http://localhost:4200/shop/configurator/{id}", true); // Add ID 
            return redirect;
        }

        // GET api/<ConfiguratorController>/id
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<ResultModel<TiaStProductsOrderDTO>>> Get(Guid id)
        {
            var result = await _configuratorService.GetConfiguratorResultByIdAsync(id);
            return Ok(result);
        }
    }
}
