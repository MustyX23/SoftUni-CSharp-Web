using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SoftUniBazar.Extensions;
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
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var ad = await adService.GetAddAdViewModelAsync();

            return View(ad);
        }
        public async Task<IActionResult> Cart()
        {
            var userId = User.GetUserId();

            var cartAds = await adService.GetCartAdsAsync(userId);

            return View(cartAds);
        }


        [HttpPost]
        public async Task<IActionResult> AddToCart(int id)
        {
             var ad = await adService.GetAdViewModelByIdAsync(id);
             var userId = User.GetUserId();

             if (ad == null)
             {
                 return RedirectToAction("All", "Ad");
             }

             await adService.AddToCartAsync(userId, ad);
             return RedirectToAction("All", "Ad");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int id)
        {
            var ad = await adService.GetAdViewModelByIdAsync(id);
            var userId = User.GetUserId();

            if (ad == null)
            {
                return RedirectToAction("All", "Ad");
            }

            await adService.RemoveFromCartAsync(userId, ad);
            return RedirectToAction("Cart", "Ad");
        }


    }
}
