using System.ComponentModel.DataAnnotations;
using SeminarHub.Models.ViewModels;
using static SeminarHub.Data.Common.EntityValidationConstants.Seminar;

namespace SeminarHub.Models.FormModels
{
    public class AddSeminarFormModel
    {
        [Required]
        [StringLength(TopicMaxLenght, MinimumLength = TopicMinLenght)]
        public string Topic { get; set; } = null!;

        [Required]
        [StringLength(LecturerMaxLenght, MinimumLength = LecturerMinLenght)]
        public string Lecturer { get; set; } = null!;

        [Required]
        [StringLength(DetailsMaxLenght, MinimumLength = DetailsMinLenght)]
        public string Details { get; set; } = null!;

        [Required(ErrorMessage = "Please enter a valid date! Example: 18/02/2024 23:30")]
        [DisplayFormat(DataFormatString = "dd/MM/yyyy HH:mm")]
        public string DateAndTime { get; set; } = null!;

        [Range(DurationMin, DurationMax)]
        public int? Duration { get; set; }

        public int CategoryId { get; set; }

        public IEnumerable<CategoryViewModel> Categories { get; set; }
            = new HashSet<CategoryViewModel>();
    }
}
