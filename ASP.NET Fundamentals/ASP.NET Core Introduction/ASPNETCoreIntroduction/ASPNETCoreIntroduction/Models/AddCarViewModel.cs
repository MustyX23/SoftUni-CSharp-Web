#nullable disable
namespace ASPNETCoreIntroduction.Models
{
    using System.ComponentModel.DataAnnotations;
    public class AddCarViewModel
    {
        [Required]
        [StringLength(50)]
        public string Make { get; set; }

        [Required]
        [StringLength(50)]
        public string Model { get; set; }

        [Required]
        [Range(1886, 2023)]
        public int Year { get; set; }

        [Required]
        [Range(0, 10000000000.00)]
        public decimal Price { get; set; }
    }
}
