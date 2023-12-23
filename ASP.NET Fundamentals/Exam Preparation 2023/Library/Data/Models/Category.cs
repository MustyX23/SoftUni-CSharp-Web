using Humanizer;
using System.ComponentModel.DataAnnotations;
using System.Security.Policy;

namespace Library.Data.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength =5)]
        public string Name { get; set; } = null!;

        public ICollection<Book> Books { get; set; } =  new HashSet<Book>();
    }
}