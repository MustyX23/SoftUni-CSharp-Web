using System.ComponentModel.DataAnnotations;

namespace SoftUniBazar.Data.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 3)]
        public string Name { get; set; } = null!;

        public ICollection<Ad> Ads { get; set; } = new HashSet<Ad>();
    }
}