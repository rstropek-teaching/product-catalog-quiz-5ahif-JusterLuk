using ProductCatalogMVC.Models;
using System.Collections.Generic;

namespace ProductCatalogMVC.Services
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();
        Product GetById(int id);
        void CreateProduct(Product prod);
        void DeleteProduct(int id);
        IEnumerable<Product> ProductSearch(string name);
    }
}
