using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using static SeminarHub.Data.Common.EntityValidationConstants.Seminar;

namespace SeminarHub.Data.Models
{
    public class Seminar
    {
        public Seminar()
        {
            SeminarsParticipants = new HashSet<SeminarParticipant>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(TopicMaxLenght, MinimumLength = TopicMinLenght)]
        public string Topic { get; set; } = null!;

        [Required]
        [StringLength(LecturerMaxLenght, MinimumLength = LecturerMinLenght)]
        public string Lecturer { get; set; } = null!;

        [Required]
        [StringLength(DetailsMaxLenght, MinimumLength = DetailsMinLenght)]
        public string Details { get; set; } = null!;

        [Required]
        public string OrganizerId { get; set; } = null!;

        [Required]
        public IdentityUser Organizer { get; set; } = null!;

        [Required]
        public DateTime DateAndTime { get; set; }

        [Range(DurationMin, DurationMax)]
        public int? Duration { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public Category Category { get; set; } = null!;

        public ICollection<SeminarParticipant> SeminarsParticipants { get; set; } = null!;
    }
}
