using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using MVCIntroDemo.Models;
using System.Text;
using System.Text.Json;

namespace MVCIntroDemo.Controllers
{
    public class ProductController : Controller
    {
        private IEnumerable<ProductViewModel> products = new List<ProductViewModel>()
        {
            new ProductViewModel()
            {
                Id = 1,
                Name = "Cheese",
                Price = 7.00m,
            },
            new ProductViewModel()
            {
                Id= 2,
                Name = "Bread",
                Price = 1.50m
            },
            new ProductViewModel()
            {
                Id = 3,
                Name = "Water",
                Price = 1.20m
            }
        };

        public IActionResult Index()
        {
            return View();
        }

        [ActionName("My-Products")]
        public IActionResult All(string keyword)
        {
            if (keyword != null)
            {
                var foundProducts = products
                    .Where(p => p.Name.ToLower()
                    .Contains(keyword.ToLower()));

                return View(foundProducts);
            }
            return View(products);
        }
        public IActionResult ProductById(int id)
        {
            
            var product = products.FirstOrDefault(x => x.Id == id);

            if (product == null)
            {
                return BadRequest();
            }

            ViewBag.Title = $"ProductId: {product.Id}";

            return View(product);
        }

        public IActionResult AllAsJson() 
        {
            return Json(products, new JsonSerializerOptions()
            {
                WriteIndented = true
            });
        }

        public IActionResult AllAsText() 
        {
            StringBuilder sb = new StringBuilder();

            foreach (var product in products)
            {
                sb.AppendLine($"ProductId: {product.Id} - {product.Name} - {product.Price} lv.");
            }

            return Content(sb.ToString().TrimEnd());
        }

        public IActionResult AllAsTextFile()
        {
            var sb = new StringBuilder();

            foreach (var product in products)
            {
                sb.AppendLine($"ProductId: {product.Id} - {product.Name} - {product.Price} lv.");
            }

            Response.Headers.Add(HeaderNames.ContentDisposition,
                @"attachment;filename=products.txt");

            return File(Encoding.UTF8.GetBytes(sb.ToString().TrimEnd()), "text/plain");
        }
    }
}
