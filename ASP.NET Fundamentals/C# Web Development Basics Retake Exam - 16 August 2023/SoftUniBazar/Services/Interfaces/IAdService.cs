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
    }
}
