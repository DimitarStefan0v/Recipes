namespace RecipesApp.Core.Models
{
    public class SingleRecipeViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Instructions { get; set; }

        public TimeSpan? PreparationTime { get; set; }

        public TimeSpan? CookingTime { get; set; }

        public int? PortionsCount { get; set; }

        public string CategoryName { get; set; }

        public string AddedByUserId { get; set; }

        public ICollection<IngredientsViewModel> Ingredients { get; set; }

        public int ImageId { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
