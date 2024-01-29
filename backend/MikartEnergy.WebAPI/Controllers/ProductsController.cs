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
        public async Task<ActionResult<ResultModel<ProductDTO>>> Get(Guid id)
        {
            //TODO: Create needed method in ProductsService
            //return Ok(await _productsService.GetProductById(id));
            throw new Exception();
        }

        [HttpGet("productBySupplierPID/{supplierPID}")]
        [AllowAnonymous]
        public async Task<ActionResult<ResultModel<ProductDTO>>> GetProductBySupplierPID(string supplierPID)
        {
            return Ok(await _productsService.GetProductBySupplierPidAsync(supplierPID));
        }

        [HttpGet("minimals")]
        [AllowAnonymous]
        public async Task<ActionResult<ResultModel<PaginationResponseDTO<ProductMinimalDTO>>>> GetProductsMinamals([FromQuery] PaginationRequestDTO request)
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
        public async Task<ActionResult<ResultModel<ProductMinimalDTO>>> GetProductMinamalById(Guid id)
        {
            //TODO: Create needed method in ProductsService
            //return Ok(await _productsService.GetProductMinamalById(id));
            throw new Exception();
        }

        [HttpGet("productMinimalBySupplierPID/{supplierPID}")]
        [AllowAnonymous]
        public async Task<ActionResult<ResultModel<ProductMinimalDTO>>> GetProductMinamalBySupplierPID(string supplierPID)
        {
            return Ok(await _productsService.GetProductMinamalBySupplierPidAsync(supplierPID));
        }

        [HttpGet("orderNumbersByFirstChars/{firstCharsOfOrderNumber}")]
        [AllowAnonymous]
        public async Task<ActionResult<ResultModel<string>>> SearchOrderNumbersByFirstChars(string firstCharsOfOrderNumber)
        {
            return Ok(await _productsService.GetOrderNumbersByFirstCharsAsync(firstCharsOfOrderNumber));
        }

        [HttpGet("productMinamalsByPartOfProductOrderNumber/{partOfProductOrderNumber}")]
        [AllowAnonymous]
        public async Task<ActionResult<ResultModel<ProductMinimalDTO[]>>> SearchProductMinamalsByPartOfProductOrderNumber(string partOfProductOrderNumber)
        {
            return Ok(await _productsService.GetProductMinamalsByPartOfProductOrderNumber(partOfProductOrderNumber));
        }

    }

}
