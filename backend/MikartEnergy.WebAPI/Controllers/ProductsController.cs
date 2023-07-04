using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MikartEnergy.BLL.Services;
using MikartEnergy.Common.DTO.CallbackRequest;
using MikartEnergy.Common.DTO.Product;
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
        public async Task<ActionResult<IEnumerable<ProductDTO>>> Get()
        {
            return Ok(await _productsService.GetAllProductsAsync());
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> Get(string id)
        {
            return Ok(await _productsService.GetProductByIdAsync(id));
        }

        [HttpGet("[controller]/minimal")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ProductMinimalDTO>>> GetProductsMinamal()
        {
            return Ok(await _productsService.GetAllProductsMinamalAsync());
        }

        [HttpGet("[controller]/minimal/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ProductMinimalDTO>>> GetProductMinamal(string id)
        {
            return Ok(await _productsService.GetProductMinamalByIdAsync(id));
        }
    }
}
