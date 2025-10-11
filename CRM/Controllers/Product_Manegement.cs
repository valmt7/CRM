using CRM.Services;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Controllers
{
    [ApiController]
    [Route("api/products")]

    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Products>>> GetProducts()
        {
            var product = await _productService.GetProductsAsync();
            return Ok(product);
           
        }
        [HttpPost]
        public async Task<IActionResult> AddProduct(string name, double price, string type,string warehouseLocation)
        {
            return Ok(await _productService.AddProduct(name, price, type,warehouseLocation));
        }

        [HttpDelete("database")]
        public async Task<IActionResult> KillData()
        {
            await _productService.KillDataAsync();
            return Ok();
        }
    }
}
