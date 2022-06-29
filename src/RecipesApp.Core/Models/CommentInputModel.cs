using RecipesApp.Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace RecipesApp.Core.Models
{
    public class CommentInputModel
    {
        [Required(ErrorMessage = CommentErrorMessages.CommentContentRequired)]
        [MinLength(1, ErrorMessage = CommentErrorMessages.CommentContentName)]
        public string Content { get; set; }

        public int RecipeId { get; set; }

    }
}
