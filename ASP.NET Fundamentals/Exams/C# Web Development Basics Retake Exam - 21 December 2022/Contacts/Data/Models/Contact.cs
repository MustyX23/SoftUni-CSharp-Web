using System.ComponentModel.DataAnnotations;

namespace Contacts.Data.Models
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string LastName { get; set; } = null!;

        [Required]
        [StringLength(60, MinimumLength = 10)]
        public string Email { get; set; } = null!;

        [Required]
        [StringLength(13, MinimumLength = 10)]
        [RegularExpression(@"^(\+359|0)(\d{3}[ -]?\d{2}[ -]?\d{2}[ -]?\d{2})$", ErrorMessage = "Invalid phone number format.")]
        public string PhoneNumber { get; set; } = null!;

        public string Address { get; set; } = null!;

        [Required]
        [RegularExpression(@"^www\.[\w\d-]+\.bg$", ErrorMessage = "Invalid website format.")]
        public string Website { get; set; } = null!;

        public ICollection<ApplicationUserContact> ApplicationUsersContacts { get; set; } = new HashSet<ApplicationUserContact>();
    }
}
