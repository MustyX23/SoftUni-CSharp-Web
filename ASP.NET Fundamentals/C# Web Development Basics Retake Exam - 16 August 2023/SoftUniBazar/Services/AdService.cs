using Microsoft.EntityFrameworkCore;
using SoftUniBazar.Data;
using SoftUniBazar.Data.Models;
using SoftUniBazar.Models;
using SoftUniBazar.Services.Interfaces;

namespace SoftUniBazar.Services
{
    public class AdService : IAdService
    {
        private BazarDbContext dbContext;

        public AdService(BazarDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddToCartAsync(string userId, AdViewModel ad)
        {
            var existingAd = await dbContext
                .AdBuyers
                .FirstOrDefaultAsync(a => a.BuyerId == userId && a.Ad.Id == ad.Id);

            if (existingAd == null)
            {
                var adBuyer = new AdBuyer()
                {
                    AdId = ad.Id,
                    BuyerId = userId
                };

                dbContext.AdBuyers.Add(adBuyer);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<AddAdViewModel> GetAddAdViewModelAsync()
        {
            var categories = await dbContext.Categories.Select(c => new CategoryViewModel()
            {
                Id = c.Id,
                Name = c.Name
            }).ToArrayAsync();

            var ad = new AddAdViewModel()
            {
                Categories = categories
            };

            return ad;
        }

        public async Task<AdViewModel> GetAdViewModelByIdAsync(int id)
        {
            var ad = await dbContext.Ads.Select(a => new AdViewModel()
            {
                Id = a.Id,
                Name = a.Name,
                Description = a.Description,
                Price = a.Price,
                Category = a.Category.Name,
                CreatedOn = a.CreatedOn.ToString("yyyy-MM-dd H:mm"),
                ImageUrl = a.ImageUrl,
                Owner = a.Owner.Email
            }).FirstAsync(a => a.Id == id);

            return ad;
        }

        public async Task<IEnumerable<AdViewModel>> GetAllAdsAsync()
        {
            var allAds = await dbContext
                .Ads
                .Select(a => new AdViewModel()
                {
                    Id = a.Id,
                    Name = a.Name,
                    Description = a.Description,
                    Category = a.Category.Name,
                    CreatedOn = a.CreatedOn.ToString("yyyy-MM-dd H:mm"),
                    ImageUrl = a.ImageUrl,
                    Owner = a.Owner.Email,
                    Price = a.Price
                }).ToArrayAsync(); 

            return allAds;
        }

        public async Task<IEnumerable<AdViewModel>> GetCartAdsAsync(string userId)
        {
            var allAds = await dbContext
                .AdBuyers
                .Where(a => a.BuyerId == userId)
                .Select(a => new AdViewModel()
                {
                    Id = a.Ad.Id,
                    Name = a.Ad.Name,
                    Description = a.Ad.Description,
                    Category = a.Ad.Category.Name,
                    CreatedOn = a.Ad.CreatedOn.ToString("yyyy-MM-dd H:mm"),
                    ImageUrl = a.Ad.ImageUrl,
                    Owner = a.Ad.Owner.Email,
                    Price = a.Ad.Price
                }).ToArrayAsync();

            return allAds;
        }

        public async Task RemoveFromCartAsync(string userId, AdViewModel ad)
        {
            var adToRemove = await dbContext
                .AdBuyers.FirstAsync(a => a.BuyerId == userId && a.AdId == ad.Id);

            if (adToRemove != null)
            {
                dbContext.AdBuyers.Remove(adToRemove);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
