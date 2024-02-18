using System.ComponentModel.DataAnnotations;
using static SeminarHub.Data.Common.EntityValidationConstants.Category;

namespace SeminarHub.Data.Models
{
    public class Category
    {
        public Category()
        {
            Seminars = new HashSet<Seminar>();
        }

        [Key]
        public int Id {  get; set; }

        [Required]
        [StringLength(NameMaxLenght, MinimumLength = NameMinLenght)]
        public string Name { get; set; } = null!;

        public ICollection<Seminar> Seminars { get; set; } = null!;

    }
}
