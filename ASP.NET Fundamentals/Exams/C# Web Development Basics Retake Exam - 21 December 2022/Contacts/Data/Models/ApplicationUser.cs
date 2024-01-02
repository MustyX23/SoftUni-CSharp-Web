using Humanizer;
using System.ComponentModel.DataAnnotations;
using System.Security.Policy;

namespace Contacts.Data.Models
{
    public class ApplicationUser
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(20, MinimumLength =5)]
        public string UserName { get; set; } = null!;

        [Required]
        [StringLength(60, MinimumLength = 10)]
        public string Email { get; set; } = null!;

        [Required]
        [StringLength(20, MinimumLength = 5)]
        public string Password { get; set; } = null!;

        public ICollection<ApplicationUserContact> ApplicationUsersContacts { get; set; } = new HashSet<ApplicationUserContact>();

    }
}
