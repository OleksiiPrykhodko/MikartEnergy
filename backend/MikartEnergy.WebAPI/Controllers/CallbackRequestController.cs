using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MikartEnergy.BLL.Services;
using MikartEnergy.Common.DTO.CallbackRequest;
using MikartEnergy.DAL.Context;
using MikartEnergy.DAL.Entities;
using MikartEnergy.WebAPI.Validators;
using System;

namespace MikartEnergy.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class CallbackRequestController : Controller
    {
        private readonly CallbackRequestService _callbackRequestService;
        private readonly IValidator<NewCallbackRequestDTO> _newCallbackRequestValidator;
        private readonly IValidator<CallbackRequestDTO> _callbackRequestValidator;

        public CallbackRequestController(
            CallbackRequestService callbackRequestService,
            IValidator<NewCallbackRequestDTO> newCallbackRequestValidator,
            IValidator<CallbackRequestDTO> validator) 
        { 
            _callbackRequestService = callbackRequestService;
            _newCallbackRequestValidator = newCallbackRequestValidator;
            _callbackRequestValidator = validator;
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
        public async Task<ActionResult<CallbackRequestDTO>> Post([FromBody] NewCallbackRequestDTO dto)
        {
            var validationResult = await _newCallbackRequestValidator.ValidateAsync(dto);

            if (validationResult.IsValid)
            {
                return Ok(await _callbackRequestService.CreateCallbackRequestAsync(dto));
            }

            var errorsMessages = validationResult.Errors
                .Select(e => new KeyValuePair<string, string>(e.PropertyName, e.ErrorMessage));
            return BadRequest( await _callbackRequestService.CreateBadRequestResponseAsync(errorsMessages) );
        }

        [HttpPut]
        [AllowAnonymous]
        public async Task<ActionResult<bool>> Put([FromBody] CallbackRequestDTO dto)
        {
            var validationResult = await _callbackRequestValidator.ValidateAsync(dto);

            if (validationResult.IsValid)
            {
                return Ok(await _callbackRequestService.UpdateCallbackRequestAsync(dto));
            }

            var errorsMessages = validationResult.Errors
                .Select(e => new KeyValuePair<string, string>(e.PropertyName, e.ErrorMessage));
            return BadRequest(await _callbackRequestService.CreateBadRequestResponseAsync(errorsMessages));
           
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
