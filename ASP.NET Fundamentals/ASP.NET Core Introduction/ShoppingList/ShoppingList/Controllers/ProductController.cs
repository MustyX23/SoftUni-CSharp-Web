using Microsoft.AspNetCore.Mvc;
using ShoppingList.Data;
using ShoppingList.Data.Models;
using ShoppingList.Models;
using System.Xml.Linq;

namespace ShoppingList.Controllers
{
    public class ProductController : Controller
    {
        private readonly ShoppingListAppDbContext _data;

        public ProductController(ShoppingListAppDbContext data)
        {
            _data = data;
        }
        public IActionResult All()
        {
            var products = _data.Products
                .Select(p => new ProductViewModel()
                {
                    Id = p.Id,
                    Name = p.Name
                })
                .ToList();
            
            return View(products);
        }

        //[HttpPost]
        public IActionResult Add(ProductViewModel viewModel) 
        {
            var product = new Product() 
            {
                Name = viewModel.Name,
            }; 

            _data.Products.Add(product);
            _data.SaveChanges();
            return RedirectToAction("All"); 
        }
    }
}
