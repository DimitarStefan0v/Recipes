namespace Recipes.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Recipes.Web.ViewModels.Categories;

    public interface ICategoriesService
    {
        IEnumerable<CategoryInListViewModel> GetCategories();

        ICollection<T> GetCategoryNames<T>();

        Task CreateAsync(CreateCategoryInputModel input, string userId);

        Task UpdateAsync(int id, EditCategoryInputModel input, string userId);

        Task DeleteAsync(int id);

        T GetById<T>(int id);
    }
}
