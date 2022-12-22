namespace Recipes.Web.ViewModels.Categories
{
    using Recipes.Data.Models;
    using Recipes.Services.Mapping;

    public class CategoriesViewModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
