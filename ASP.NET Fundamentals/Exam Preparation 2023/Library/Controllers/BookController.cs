using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    [Authorize]
    public class BookController : Controller
    {
        public IActionResult All()
        {
            return View();
        }
        public IActionResult Mine()
        {
            return View();
        }
        public IActionResult Add()
        {
            return View();
        }
        public IActionResult Remove()
        {
            return View();
        }
    }
}
