using Microsoft.EntityFrameworkCore;
using SeminarHub.Data;
using SeminarHub.Data.Models;
using SeminarHub.Models.FormModels;
using SeminarHub.Models.ViewModels;
using SeminarHub.Services.Interfaces;
using System.Globalization;

namespace SeminarHub.Services
{
    public class SeminarService : ISeminarService
    {
        private readonly SeminarHubDbContext dbContext;

        public SeminarService(SeminarHubDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddSeminarAsync(AddSeminarFormModel model, string organizerId)
        {
            Seminar seminar = new Seminar() 
            {
                Topic = model.Topic,
                Lecturer = model.Lecturer,
                Details = model.Details,
                DateAndTime = DateTime.ParseExact
                (model.DateAndTime, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture),
                Duration = model.Duration,
                CategoryId = model.CategoryId,
                OrganizerId = organizerId
            };
            
            await dbContext.AddAsync(seminar);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteSeminarByIdAsync(int id, string organizerId)
        {
            var seminarAndParticipant = await dbContext
                .SeminarsParticipants
                .FirstOrDefaultAsync(s => s.SeminarId == id && s.Seminar.OrganizerId == organizerId);

            var seminarForDelete = await dbContext
                .Seminars
                .FirstOrDefaultAsync(s => s.Id == id);

            if (seminarAndParticipant != null && seminarForDelete != null)
            {
                dbContext.SeminarsParticipants.Remove(seminarAndParticipant);
                dbContext.Seminars.Remove(seminarForDelete);

                await dbContext.SaveChangesAsync();
            }                         
        }

        public async Task EditSeminarAsync(int id, EditSeminarFormModel model)
        {
            Seminar seminarForEdit = await dbContext
                .Seminars.FirstAsync(s => s.Id == id);

            seminarForEdit.Topic = model.Topic;
            seminarForEdit.Details = model.Details;
            seminarForEdit.Lecturer = model.Lecturer;
            seminarForEdit.DateAndTime = DateTime.Parse(model.DateAndTime);
            seminarForEdit.Duration = model.Duration;
            seminarForEdit.CategoryId = model.CategoryId;

            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<CategoryViewModel>> GetAllCategoriesAsync()
        {
            var allCategories = await dbContext.Categories
                .Select(c => new CategoryViewModel()
                {
                    Id = c.Id,
                    Name = c.Name,
                })
                .ToArrayAsync();

            return allCategories;
        }

        public async Task<IEnumerable<JoinedSeminarViewModel>> GetAllJoinedSeminarsAsync(string organizerId)
        {
            var joinedSeminars = await dbContext
                .SeminarsParticipants
                .Where(sp => sp.ParticipantId == organizerId)
                .Select(sp => new JoinedSeminarViewModel
                {
                    Id = sp.Seminar.Id,
                    Topic = sp.Seminar.Topic,
                    DateAndTime = sp.Seminar.DateAndTime.ToString("dd/MM/yyyy HH:mm"),
                    Lecturer = sp.Seminar.Lecturer,
                    Category = sp.Seminar.Category.Name,
                    Organizer = sp.Seminar.Organizer.UserName
                }).ToArrayAsync();

            return joinedSeminars;
        }

        public async Task<IEnumerable<AllSeminarsViewModel>> GetAllSeminarsAsync()
        {
            var allSeminars = await dbContext.Seminars
                .Select(s => new AllSeminarsViewModel()
                {
                    Id= s.Id,
                    Topic = s.Topic,
                    DateAndTime = s.DateAndTime.ToString("dd/MM/yyyy HH:mm"),
                    Category = s.Category.Name,
                    Lecturer = s.Lecturer,
                    Organizer = s.Organizer.UserName
                })
                .ToArrayAsync();

            return allSeminars;
        }

        public async Task<DetailsSeminarViewModel> GetDetailsSeminarViewModelByIdAsync(int id)
        {
            var detailedSeminar = await dbContext.Seminars
                .Where(s => s.Id == id)
                .Select(s => new DetailsSeminarViewModel()
                {
                    Id = s.Id,
                    Topic = s.Topic,
                    DateAndTime= s.DateAndTime.ToString("dd/MM/yyyy HH:mm"),
                    Details = s.Details,
                    Duration = s.Duration,
                    Category = s.Category.Name,
                    Lecturer= s.Lecturer,
                    Organizer= s.Organizer.UserName
                }).FirstAsync();

            return detailedSeminar;
        }

        public async Task<JoinedSeminarViewModel> GetJoinedSeminarByIdAsync(int id)
        {
            var joinedSeminar = await dbContext
                .Seminars
                .Include(s => s.Category)
                .Include(s => s.Organizer)
                .FirstAsync(s => s.Id == id);

            var categories = await dbContext.Categories.Select(t => new CategoryViewModel()
            {
                Id = t.Id,
                Name = t.Name
            }).ToArrayAsync();

            return new JoinedSeminarViewModel()
            {
                Id = joinedSeminar.Id,
                Topic = joinedSeminar.Topic,
                DateAndTime = joinedSeminar.DateAndTime.ToString("dd/MM/yyyy HH:mm"),
                Category= joinedSeminar.Category.Name,
                Lecturer = joinedSeminar.Lecturer,
                Organizer = joinedSeminar.Organizer.UserName
            };
        }

        public async Task<Seminar> GetSeminarByIdAsync(int id)
        {
            var seminar = await dbContext
                .Seminars
                .FirstAsync(s => s.Id == id);

            return seminar;
        }

        public async Task<DeleteSeminarViewModel> GetSeminarForDeleteByIdAsync(int id)
        {
            var seminarForDelete = await dbContext
                .Seminars
                .FirstAsync(s => s.Id == id);

            return new DeleteSeminarViewModel
            {
                Id = seminarForDelete.Id,
                DateAndTime = seminarForDelete.DateAndTime,
                Topic = seminarForDelete.Topic,
            };
        }

        public async Task<EditSeminarFormModel> GetSeminarForEditByIdAsync(int id)
        {
            var seminarForEdit = await dbContext.Seminars
                .FirstAsync(s => s.Id == id);

            var categories = await dbContext.Categories
                .Select(c => new CategoryViewModel()
                {
                    Id = c.Id,
                    Name = c.Name,
                })
                .ToArrayAsync();

            return new EditSeminarFormModel()
            {
                Topic = seminarForEdit.Topic,
                Details = seminarForEdit.Details,
                DateAndTime = seminarForEdit.DateAndTime.ToString("dd/MM/yyyy HH:mm"),
                Duration = seminarForEdit.Duration,
                Categories = categories,
                CategoryId = seminarForEdit.CategoryId,
                Lecturer = seminarForEdit.Lecturer
            };
        }

        public async Task<SeminarParticipant> GetSeminarParticipantByIdAsync(int id)
        {
            var seminarParticipant = await dbContext
                .SeminarsParticipants
                .FirstOrDefaultAsync(s => s.SeminarId == id);

            return seminarParticipant;
        }

        public async Task<bool> IsTheSeminarJoinedAsync(int id, string organizerId)
        {
            bool isJoined = await dbContext.SeminarsParticipants
                .AnyAsync(s => s.SeminarId == id && s.ParticipantId == organizerId);

            return isJoined;
        }

        public async Task JoinSeminarAsync(string organizerId, JoinedSeminarViewModel joinedSeminar)
        {
            var existingSeminar = await dbContext
                .SeminarsParticipants
                .FirstOrDefaultAsync(s => s.SeminarId == joinedSeminar.Id && s.ParticipantId == organizerId);

            if (existingSeminar == null)
            {
                var seminarParticipant = new SeminarParticipant()
                {
                    SeminarId = joinedSeminar.Id,
                    ParticipantId = organizerId,
                };

                dbContext.SeminarsParticipants.Add(seminarParticipant);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task LeaveSeminarAsync(string organizerId, JoinedSeminarViewModel joinedSeminar)
        {
            var seminarForRemoval = await dbContext
                .SeminarsParticipants
                .FirstOrDefaultAsync(s => s.SeminarId == joinedSeminar.Id && s.ParticipantId == organizerId);

            if (seminarForRemoval != null)
            {
                dbContext.SeminarsParticipants.Remove(seminarForRemoval);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
