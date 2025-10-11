
using CRM.MidMiddleware;
using Microsoft.EntityFrameworkCore;


namespace CRM.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Products>> GetProductsAsync()
        {
            var products = await _context.Products.ToListAsync();
            if (products.Count == 0)
            {
                throw new NotFoundPrductsExeption();
            }
            return products;
        }
        public async Task<Products> AddProduct(string name, double price, string type, string warehouseLocation)
        {
            var product = new Products
            {
                Name = name,
                Price = price,
                Type = type,
                WarehouseLocation = warehouseLocation
            };
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }
        
        public async Task KillDataAsync()
        {
            _context.Products.RemoveRange(_context.Products);
            await _context.SaveChangesAsync();
        }

    }
}
