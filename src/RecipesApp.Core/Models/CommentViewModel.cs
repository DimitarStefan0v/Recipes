namespace RecipesApp.Core.Models
{
    public class CommentViewModel
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public string AddedByUser { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
