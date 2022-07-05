namespace RecipesApp.Core.Models
{
    public class PagingViewModel
    {
        public int PageNumber { get; set; }

        public int RecipesCount { get; set; }

        public int ItemsPerPage { get; set; }

        public bool HasPreviousPage => PageNumber > 1;

        public bool HasNextPage => PageNumber < PagesCount;

        public int PagesCount => (int)Math.Ceiling((double)RecipesCount / ItemsPerPage);

        public int PreviousPageNumber => PageNumber - 1;

        public int NextPageNumber => PageNumber + 1;

        public bool FromCategoriesController { get; set; }
    }
}