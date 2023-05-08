using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MikartEnergy.BLL.Services;
using MikartEnergy.Common.DTO.CallbackRequest;
using MikartEnergy.DAL.Context;
using MikartEnergy.DAL.Entities;

namespace MikartEnergy.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class CallbackRequestController : Controller
    {
        private readonly CallbackRequestService _callbackRequestService;
        public CallbackRequestController(CallbackRequestService callbackRequestService) 
        { 
            _callbackRequestService = callbackRequestService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<CallbackRequestDTO>>> Get()
        {
            return Ok(await _callbackRequestService.GetAllCallbackRequestsAsync(false));
        }

        [HttpGet("all")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<CallbackRequestDTO>>> GetAll()
        {
            return Ok(await _callbackRequestService.GetAllCallbackRequestsAsync(true));
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<CallbackRequestDTO>> Create([FromBody] NewCallbackRequestDTO dto)
        {
            if(ModelState.IsValid) 
            {
                return Ok(await _callbackRequestService.CreateCallbackRequestAsync(dto));
            }

            var errorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
            return BadRequest( await _callbackRequestService.CreateBadRequestResponseAsync(errorMessages) );
        }

        [HttpPut]
        [AllowAnonymous]
        public async Task<ActionResult<bool>> Put([FromBody] CallbackRequestDTO dto)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _callbackRequestService.UpdateCallbackRequestAsync(dto));
            }

            var errorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
            return BadRequest(await _callbackRequestService.CreateBadRequestResponseAsync(errorMessages));
        }

        [HttpDelete("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult> Delete(Guid id)
        {
            if (ModelState.IsValid)
            {
                return await _callbackRequestService.DeleteCallbackRequestAsync(id) ? NoContent() : NotFound();
            }
            
            return BadRequest();            
        }
    }
}
