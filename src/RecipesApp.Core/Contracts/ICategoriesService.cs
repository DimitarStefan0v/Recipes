using RecipesApp.Core.Models;

namespace RecipesApp.Core.Contracts
{
    public interface ICategoriesService
    {
        ICollection<CategoriesViewModel> GetAllCategories();
    }
}
