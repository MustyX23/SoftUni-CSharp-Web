using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SoftUniBazar.Services.Interfaces;

namespace SoftUniBazar.Controllers
{
    [Authorize]
    public class AdController : Controller
    {
        private IAdService adService;

        public AdController(IAdService adService)
        {
            this.adService = adService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var allAds = await adService.GetAllAdsAsync();
            return View(allAds);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int id)
        {

        }


    }
}
