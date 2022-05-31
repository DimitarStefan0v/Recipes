namespace RecipesApp.Core.Models
{
    public class RecipeInListViewModel
    {
        public int Id { get; set; } 

        public string Image { get; set; }

        public string Name { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }
    }
}
