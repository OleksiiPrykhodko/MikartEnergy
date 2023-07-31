using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MikartEnergy.Common.DTO.Configurator;
using MikartEnergy.WebAPI.ModelBinders;

namespace MikartEnergy.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ConfiguratorController : ControllerBase
    {
        // POST api/<ConfiguratorController>
        [HttpPost]
        [Consumes("multipart/form-data")] // Do we need this ? 
        [AllowAnonymous]
        public IActionResult Post([ModelBinder(typeof(TiaStOrderModelBinder))] TiaStResultDTO[] result)
        {


            var redirect = new RedirectResult($"http://localhost:4200/shop/configurator/", true); // Add ID 
            return redirect;
        }

        // GET api/<ConfiguratorController>/id
        [HttpGet("{id}")]
        [AllowAnonymous]
        public string Get(Guid id)
        {
            return "value";
        }
    }
}
