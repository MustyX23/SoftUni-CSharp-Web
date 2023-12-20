using System.ComponentModel.DataAnnotations;
using static TaskboardApp.Common.EntityValidationConstants.Board;

namespace TaskBoardApp.Data.Models
{
    public class Board
    {
        public Board()
        {
            Tasks = new HashSet<Task>();
        }
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(BoardMaxName)]
        [MinLength(BoardMinName)]
        public string Name { get; set; } = null!;

        public virtual ICollection<Task> Tasks { get; set; } = null!;
    }
}
