using Microsoft.AspNetCore.Mvc;
using ProductCatalogMVC.Models;
using ProductCatalogMVC.Services;

namespace ProductCatalogMVC.Controllers
{
    public class ProductController : Controller
    {

        private IProductRepository productRepository;


        public ProductController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

      
        public IActionResult Index()
        {
            return View(productRepository.GetAll());
        }

       
        public IActionResult Details(int id)
        {
            var proddetails = this.productRepository.GetById(id);
            if(proddetails == null)
            {
                return NotFound();
            }
            return View(proddetails);
        }

        
        public IActionResult Create(Product prod)
        {
            if (ModelState.IsValid)
            {
                if (prod != null)
                {
                    this.productRepository.CreateProduct(prod);
                }
            }
            return View();
        }

        
        public IActionResult Delete(int id)
        {
            var productdelete = productRepository.GetById(id);
            if(productdelete == null)
            {
                return NotFound();
            }
            return View(productdelete);
        }

        
        public IActionResult DeleteConfirmed(int id)
        {
            productRepository.DeleteProduct(id);
            return RedirectToAction("Index");
        }

        
        public IActionResult ProductSearch(string name)
        {
            return View(productRepository.ProductSearch(name));
        }
    }
}