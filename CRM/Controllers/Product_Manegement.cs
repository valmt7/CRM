using CRM.Services;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Controllers
{
    [ApiController]
    [Route("api/")]

    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }


        [HttpGet("products")]
        public async Task<ActionResult<IEnumerable<Products>>> GetProducts()
        {
            return Ok(await _productService.GetProductsAsync());
        }
        [HttpPost("products")]
        public async Task<IActionResult> AddProduct(string name, double price, string type)
        {
            return Ok(await _productService.AddProduct(name, price, type));
        }
    }
}
