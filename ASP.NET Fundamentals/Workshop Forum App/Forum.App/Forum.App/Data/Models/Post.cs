namespace Forum.App.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using static Forum.App.Common.EntityValidations.Post;
    public class Post
    {
        public Post()
        {
            Id = Guid.NewGuid();
        }
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MinLength(TitleMinLength)]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required]
        [MinLength(ContentMinLength)]
        [MaxLength(ContentMaxLength)]
        public string Content { get; set; } = null!;

    }
}
