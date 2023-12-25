using Microsoft.EntityFrameworkCore;
using SoftUniBazar.Data;
using SoftUniBazar.Data.Models;
using SoftUniBazar.Models;
using SoftUniBazar.Services.Interfaces;
using System.Security.Claims;


namespace SoftUniBazar.Services
{
    public class AdService : IAdService
    {
        private BazarDbContext dbContext;

        public AdService(BazarDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddAsync(AddAdViewModel model, string userId)
        {
            Ad ad = new Ad()
            {
                Name = model.Name,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                CreatedOn = model.CreatedOn,
                CategoryId = model.CategoryId,
                Price = model.Price,
                OwnerId = userId
            };

            dbContext.Ads.Add(ad);
            await dbContext.SaveChangesAsync();
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

        public async Task EditAsync(int id, AddAdViewModel model, string userId)
        {
            Ad ad = await dbContext.Ads.FirstAsync(a => a.Id == id);

            ad.Name = model.Name;
            ad.Description = model.Description;
            ad.ImageUrl = model.ImageUrl;
            ad.Price = model.Price;
            ad.CategoryId = model.CategoryId;

            await dbContext.SaveChangesAsync();
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

        public async Task<AddAdViewModel> GetAddAdViewModelByIdAsync(int id)
        {
            var ad = await dbContext.Ads.FirstAsync(a => a.Id == id);

            var categories = await dbContext.Categories.Select(c => new CategoryViewModel()
            {
                Id = c.Id,
                Name = c.Name
            }).ToArrayAsync();

            return new AddAdViewModel()
            {
                Name = ad.Name,
                Description = ad.Description,
                CategoryId = ad.CategoryId,
                ImageUrl = ad.ImageUrl,
                Price = ad.Price,
                Categories = categories
            };
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
