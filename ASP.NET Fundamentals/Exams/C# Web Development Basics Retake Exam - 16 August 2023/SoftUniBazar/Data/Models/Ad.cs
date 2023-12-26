using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftUniBazar.Data.Models
{
    public class Ad
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(25, MinimumLength = 5)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(250, MinimumLength = 15)]
        public string Description { get; set; } = null!;

        [Required]
        public decimal Price {  get; set; }

        [Required]
        [ForeignKey(nameof(Owner))]
        public string OwnerId { get; set; } = null!;

        [Required]
        public IdentityUser Owner { get; set; } = null!;

        [Required]
        public string ImageUrl {  get; set; } = null!;

        [Required]
        public DateTime CreatedOn { get; set; } 

        [Required]
        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }

        [Required]
        public Category Category { get; set; } = null!;
    }
}
