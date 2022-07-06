namespace RecipesApp.Core.Models
{
    public class SingleRecipeViewModel
    {
        public SingleRecipeViewModel()
        {
            Ingredients = new HashSet<IngredientsViewModel>();
            Comments = new HashSet<CommentViewModel>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public string Instructions { get; set; }

        public TimeSpan? PreparationTime { get; set; }

        public TimeSpan? CookingTime { get; set; }

        public int? TotalTime => (int)(PreparationTime.Value.TotalMinutes + CookingTime.Value.TotalMinutes);

        public int? PortionsCount { get; set; }

        public string CategoryName { get; set; }

        public string AddedByUser { get; set; }

        public ICollection<IngredientsViewModel> Ingredients { get; set; }

        public string ImageUrl { get; set; }

        public DateTime CreatedOn { get; set; }

        public double AverageVotesValue { get; set; }

        public ICollection<CommentViewModel> Comments { get; set; }

        public bool IsRecipeFavorite { get; set; }

        public bool IsTakenFromRecipeGotvachWebsite { get; set; }

    }
}
