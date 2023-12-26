using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Homies.Models
{
    public class DetailsEventViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string Organiser { get; set; } = null!;

        public string CreatedOn { get; set; } = null!;

        public string Start { get; set; } = null!;

        public string End { get; set; } = null!;

        public string Type { get; set; } = null!;
    }
}
