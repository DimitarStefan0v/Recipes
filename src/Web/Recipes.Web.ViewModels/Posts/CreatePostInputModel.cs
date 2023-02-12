namespace Recipes.Web.ViewModels.Posts
{
    using System.ComponentModel.DataAnnotations;

    using global::Recipes.Common.Constants;

    public class CreatePostInputModel
    {
        [Display(Name = "Заглавие на поста")]
        [Required(ErrorMessage = PostErrorMessages.NameRequired)]
        [MinLength(3, ErrorMessage = PostErrorMessages.NameLength)]
        [MaxLength(50, ErrorMessage = PostErrorMessages.NameLength)]
        public string Name { get; set; }

        [Display(Name = "Съдържание към поста")]
        [Required(ErrorMessage = PostErrorMessages.ContentRequired)]
        [MinLength(10, ErrorMessage = PostErrorMessages.ContentLength)]
        public string Content { get; set; }
    }
}
