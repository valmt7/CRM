using Microsoft.AspNetCore.Mvc;

namespace CRM.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Products>> GetProductsAsync();
        Task<Products> AddProduct(string name, double price, string type);
        Task KillDataAsync();
    }
}