using SoftUniBazar.Models;

namespace SoftUniBazar.Services.Interfaces
{
    public interface IAdService
    {
        Task<IEnumerable<AdViewModel>> GetAllAdsAsync();

        Task<AdViewModel> GetAdViewModelByIdAsync(int id);

        Task AddToCartAsync(string userId, AdViewModel model);

        Task<IEnumerable<AdViewModel>> GetCartAdsAsync(string userId);

        Task RemoveFromCartAsync(string userId, AdViewModel model);

        Task<AddAdViewModel> GetAddAdViewModelAsync();

        Task<AddAdViewModel> GetAddAdViewModelByIdAsync(int id);
        Task AddAsync(AddAdViewModel ad, string userId);
        Task EditAsync(int id, AddAdViewModel ad, string userId);
    }
}
