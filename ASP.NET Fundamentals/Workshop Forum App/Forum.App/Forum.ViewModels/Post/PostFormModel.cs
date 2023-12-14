using System.ComponentModel.DataAnnotations;

namespace Forum.ViewModels.Post
{
    using static Forum.App.Common.EntityValidations.Post;
    public class PostFormModel
    {
        [Required]
        [StringLength(TitleMaxLength, MinimumLength= TitleMinLength)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(ContentMaxLength, MinimumLength = ContentMinLength)]
        public string Content { get; set; } = null!;
    }
}
