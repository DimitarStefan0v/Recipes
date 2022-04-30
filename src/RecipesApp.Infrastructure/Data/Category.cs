using System.ComponentModel.DataAnnotations;

namespace RecipesApp.Infrastructure.Data
{
    public class Category
    {
        public Category()
        {
            this.Recipes = new HashSet<Recipe>();
        }
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Name { get; set; }

        public virtual ICollection<Recipe> Recipes { get; set; }
    }
}
