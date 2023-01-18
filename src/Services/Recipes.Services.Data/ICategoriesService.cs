﻿namespace Recipes.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Recipes.Web.ViewModels.Categories;

    public interface ICategoriesService
    {
        ICollection<CategoryInListViewModel> GetCategories();

        ICollection<T> GetCategoryNames<T>();

        Task CreateAsync(CreateCategoryInputModel input, string userId);

        Task DeleteAsync(int id);
    }
}
