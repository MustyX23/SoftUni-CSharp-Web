using SoftUniBazar.Models;

namespace SoftUniBazar.Services.Interfaces
{
    public interface IAdService
    {
        Task<IEnumerable<AdAllViewModel>> GetAllAdsAsync();


    }
}
