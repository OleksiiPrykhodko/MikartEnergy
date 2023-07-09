using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MikartEnergy.BLL.Services;
using MikartEnergy.Common.DTO.CallbackRequest;
using MikartEnergy.Common.DTO.Pagination;
using MikartEnergy.Common.DTO.Product;
using MikartEnergy.Common.Models.Result;
using System.Security.Cryptography.X509Certificates;

namespace MikartEnergy.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ProductsController : Controller
    {
        private readonly ProductService _productsService;

        public ProductsController(ProductService productService)
        {
            _productsService = productService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<ResultModel<PaginationResponseDTO<ProductDTO>>>> Get([FromQuery] PaginationRequestDTO request)
        {
            return Ok(await _productsService.GetAllProductsAsync(request));
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<ResultModel<IEnumerable<ProductDTO>>>> Get(string id)
        {
            return Ok(await _productsService.GetProductByIdAsync(id));
        }

        [HttpGet("minimal")]
        [AllowAnonymous]
        public async Task<ActionResult<ResultModel<PaginationResponseDTO<ProductMinimalDTO>>>> GetProductsMinamal([FromQuery] PaginationRequestDTO request)
        {
            return Ok(await _productsService.GetAllProductsMinamalAsync(request));
        }

        [HttpGet("minimal/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<ResultModel<IEnumerable<ProductMinimalDTO>>>> GetProductMinamal(string id)
        {
            return Ok(await _productsService.GetProductMinamalByIdAsync(id));
        }
    }
}
