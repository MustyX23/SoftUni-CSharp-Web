using ASPNETCoreIntroduction.Models;
using ASPNETCoreIntroduction.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ASPNETCoreIntroduction.Controllers
{
    public class CarsController : Controller
    {
        private readonly ICarService carService;

        public CarsController(ICarService carService)
        {
            this.carService = carService;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Add(AddCarViewModel viewModel) 
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Car", "Unexpected Error! x(");
                return this.View(viewModel);
            }

            return this.RedirectToAction("Index", "Home");
        }
    }
}
