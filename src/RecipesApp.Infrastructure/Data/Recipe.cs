using RecipesApp.Infrastructure.Data.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipesApp.Infrastructure.Data
{
    public class Recipe
    {
        public Recipe()
        {
            Ingredients = new HashSet<RecipeIngredient>();
            Votes = new HashSet<Vote>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MinLength(10)]
        public string Instructions { get; set; }

        public TimeSpan? PreparationTime { get; set; }

        public TimeSpan? CookingTime { get; set; }

        public  int? PortionsCount { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool IsChecked { get; set; }

        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        [ForeignKey(nameof(AddedByUser))]
        public string AddedByUserId { get; set; }

        public ApplicationUser AddedByUser { get; set; }

        [ForeignKey(nameof(CloudImage))]
        public int ImageId { get; set; }

        public CloudImage Image { get; set; }

        public ICollection<RecipeIngredient> Ingredients { get; set; }

        public ICollection<Vote> Votes { get; set; }
    }
}
