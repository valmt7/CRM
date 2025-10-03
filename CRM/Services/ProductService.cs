using CRM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;

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
            return await _context.Products.ToListAsync();
        }
        public async Task<Products> AddProduct(string name, double price, string type)
        {
            var product = new Products
            {
                Name = name,
                Price = price,
                Type = type
            };
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

    }
}
