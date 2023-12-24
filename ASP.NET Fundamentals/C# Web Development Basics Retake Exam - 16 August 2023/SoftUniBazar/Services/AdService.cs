using Microsoft.EntityFrameworkCore;
using SoftUniBazar.Data;
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
        public async Task<IEnumerable<AdAllViewModel>> GetAllAdsAsync()
        {
            var allAds = await dbContext
                .Ads
                .Select(a => new AdAllViewModel()
                {
                    Id = a.Id,
                    Name = a.Name,
                    Description = a.Description,
                    CreatedOn = a.CreatedOn.ToString("yyyy-MM-dd H:mm"),
                    ImageUrl = a.ImageUrl,
                    Owner = a.Owner.Email,
                    Price = a.Price
                }).ToArrayAsync(); 

            return allAds;
        }
    }
}
