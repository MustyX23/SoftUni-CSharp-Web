using SeminarHub.Data.Models;
using SeminarHub.Models.FormModels;
using SeminarHub.Models.ViewModels;

namespace SeminarHub.Services.Interfaces
{
    public interface ISeminarService
    {
        Task<Seminar> GetSeminarByIdAsync(int id);
        Task AddSeminarAsync(AddSeminarFormModel model, string organizerId);
        Task<IEnumerable<CategoryViewModel>> GetAllCategoriesAsync();
        Task<IEnumerable<JoinedSeminarViewModel>> GetAllJoinedSeminarsAsync(string organizerId);
        Task<IEnumerable<AllSeminarsViewModel>> GetAllSeminarsAsync();
        Task<EditSeminarFormModel> GetSeminarForEditByIdAsync(int id);
        Task EditSeminarAsync(int id, EditSeminarFormModel model);
        Task JoinSeminarAsync(string organizerId, JoinedSeminarViewModel joinedSeminar);
        Task<JoinedSeminarViewModel> GetJoinedSeminarByIdAsync(int id);
        Task<bool> IsTheSeminarJoinedAsync(int id, string organizerId);
        Task LeaveSeminarAsync(string organizerId, JoinedSeminarViewModel joinedSeminar);
        Task<DetailsSeminarViewModel> GetDetailsSeminarViewModelByIdAsync(int id);
        Task<DeleteSeminarViewModel> GetSeminarForDeleteByIdAsync(int id);
        Task DeleteSeminarByIdAsync(int id, string organizerId);
        Task<SeminarParticipant> GetSeminarParticipantByIdAsync(int id);
    }
}
