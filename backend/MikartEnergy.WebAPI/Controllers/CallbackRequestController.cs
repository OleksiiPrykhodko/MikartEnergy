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
            return Ok(await _callbackRequestService.GetAllCallbackRequests(false));
        }

        [HttpGet("all")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<CallbackRequestDTO>>> GetAll()
        {
            return Ok(await _callbackRequestService.GetAllCallbackRequests(true));
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<CallbackRequestDTO>> Create([FromBody] NewCallbackRequestDTO dto)
        {
            if(ModelState.IsValid) 
            {
                return Ok(await _callbackRequestService.CreateCallbackRequest(dto));
            }
            
            return BadRequest();
        }

        [HttpPut]
        [AllowAnonymous]
        public async Task<ActionResult<CallbackRequestDTO>> Put([FromBody] CallbackRequestDTO dto)
        {
            if (ModelState.IsValid)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult> Delete(Guid id)
        {
            if (ModelState.IsValid)
            {
                return await _callbackRequestService.DeleteCallbackRequests(id) ? NoContent() : NotFound();
            }
            
            return BadRequest();
            
        }
    }
}
