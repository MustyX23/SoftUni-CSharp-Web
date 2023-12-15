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

        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
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
        [HttpGet]
        public IActionResult Edit(int id) 
        {
            Product product = _data.Products.FirstOrDefault(p => p.Id == id);

            return View(new ProductViewModel()
            {
                Name = product.Name
            });
        }
        [HttpPost]
        public IActionResult Edit(int id, Product model)
        {
            var productToModify = _data.Products.FirstOrDefault(p => p.Id == id);

            productToModify.Name = model.Name;

            _data.SaveChanges();

            return RedirectToAction("All");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            Product product = _data.Products.FirstOrDefault(x => x.Id == id);

            _data.Products.Remove(product);
            _data.SaveChanges();
            return RedirectToAction("All");
        }
    }
}
