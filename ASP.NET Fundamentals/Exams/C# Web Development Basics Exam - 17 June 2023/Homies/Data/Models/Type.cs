using Humanizer;
using System.ComponentModel.DataAnnotations;
using System.Security.Policy;

namespace Homies.Data.Models
{
    public class Type
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(15, MinimumLength =5)]
        public string Name { get; set; } = null!;

        public ICollection<Event> Events { get; set; } = new HashSet<Event>();
    }
}
