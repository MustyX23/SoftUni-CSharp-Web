using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SoftUniBazar.Extensions;
using SoftUniBazar.Models;
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
        [HttpPost]
        public async Task<IActionResult> Add(AddAdViewModel ad)
        {
            var userId = User.GetUserId();

            await adService.AddAsync(ad, userId);

            return RedirectToAction("All", "Ad");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction("All", "Ad");
            }
            var ad = await adService.GetAddAdViewModelByIdAsync(id);

            return View(ad);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, AddAdViewModel ad)
        {
            var userId = User.GetUserId();

            await adService.EditAsync(id, ad, userId);

            return RedirectToAction("All", "Ad");
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
                 return RedirectToAction("Cart", "Ad");
             }

             await adService.AddToCartAsync(userId, ad);
             return RedirectToAction("Cart", "Ad");
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
            return RedirectToAction("All", "Ad");
        }


    }
}
