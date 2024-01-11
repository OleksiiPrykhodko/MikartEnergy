using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MikartEnergy.BLL.Services;
using MikartEnergy.Common.DTO.Pagination;
using MikartEnergy.Common.DTO.Product;
using MikartEnergy.Common.Models.Result;

namespace MikartEnergy.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ProductsController : Controller
    {
        private readonly ProductService _productsService;
        private readonly IValidator<PaginationRequestDTO> _paginationValidator;

        public ProductsController(
            ProductService productService,
            IValidator<PaginationRequestDTO> paginationValidator)
        {
            _productsService = productService;
            _paginationValidator = paginationValidator;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<ResultModel<PaginationResponseDTO<ProductDTO>>>> Get([FromQuery] PaginationRequestDTO request)
        {
            var validationResult = await _paginationValidator.ValidateAsync(request);

            if (validationResult.IsValid)
            {
                return Ok(await _productsService.GetAllProductsAsync(request));
            }

            var errorsMessages = validationResult.Errors
                .Select(err => new KeyValuePair<string, string>(err.PropertyName, err.ErrorMessage));
            return BadRequest(await _productsService.CreateBadRequestResultAsync(request, errorsMessages));
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<ResultModel<ProductDTO>>> Get(string id)
        {
            return Ok(await _productsService.GetProductByIdAsync(id));
        }

        [HttpGet("minimal")]
        [AllowAnonymous]
        public async Task<ActionResult<ResultModel<PaginationResponseDTO<ProductMinimalDTO>>>> GetProductsMinamal([FromQuery] PaginationRequestDTO request)
        {
            var validationResult = await _paginationValidator.ValidateAsync(request);

            if (validationResult.IsValid)
            {
                return Ok(await _productsService.GetAllProductsMinamalAsync(request));
            }

            var errorsMessages = validationResult.Errors
                .Select(err => new KeyValuePair<string, string>(err.PropertyName, err.ErrorMessage));
            return BadRequest(await _productsService.CreateBadRequestResultAsync(request, errorsMessages));
        }

        [HttpGet("minimal/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<ResultModel<ProductMinimalDTO>>> GetProductMinamal(string id)
        {
            return Ok(await _productsService.GetProductMinamalByIdAsync(id));
        }

        [HttpGet("searchOrderNumbersByFirstChars/{firstCharsOfOrderNumber}")]
        [AllowAnonymous]
        public async Task<ActionResult<ResultModel<string>>> SearchOrderNumbersByFirstChars(string firstCharsOfOrderNumber)
        {
            return Ok(await _productsService.GetOrderNumbersOfProductsByFirstChars(firstCharsOfOrderNumber));
        }

    }

}
