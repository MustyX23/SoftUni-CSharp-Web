using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Library.Data.Models
{
    public class IdentityUserBook
    {
        [Required]
        public string CollectorId { get; set; } = null!;

        public IdentityUser Collector { get; set; } = null!;

        [Required]
        public int BookId {  get; set; }

        public Book Book { get; set; } = null!;

    }
}