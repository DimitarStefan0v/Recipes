using System.ComponentModel.DataAnnotations;

namespace RecipesApp.Core.Models
{
    public class CommentInputModel
    {
        [Required]
        [MinLength(1)]
        public string Content { get; set; }

        public int RecipeId { get; set; }

    }
}
