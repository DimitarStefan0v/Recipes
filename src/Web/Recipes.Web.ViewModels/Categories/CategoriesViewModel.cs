using Recipes.Data.Models;
using Recipes.Services.Mapping;

namespace Recipes.Web.ViewModels.Categories
{
    public class CategoriesViewModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
