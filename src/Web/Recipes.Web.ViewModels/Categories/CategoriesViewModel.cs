namespace Recipes.Web.ViewModels.Categories
{
    using global::Recipes.Data.Models;
    using global::Recipes.Services.Mapping;

    public class CategoriesViewModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
