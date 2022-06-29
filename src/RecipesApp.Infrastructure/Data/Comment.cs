using RecipesApp.Infrastructure.Data.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipesApp.Infrastructure.Data
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(1)]
        public string Content { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreatedOn { get; set; }

        [ForeignKey(nameof(AddedByUser))]
        public string AddedByUserId { get; set; }

        public ApplicationUser AddedByUser { get; set; }

        [ForeignKey(nameof(Recipe))]
        public int RecipeId { get; set; }

        public Recipe Recipe { get; set; }
    }
}
