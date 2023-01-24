﻿namespace Recipes.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Recipes.Web.ViewModels.Recipes;

    public interface IRecipesService
    {
        Task CreateAsync(CreateRecipeInputModel input, string userId);

        IEnumerable<T> GetAll<T>(int page, int itemsPerPage);

        IEnumerable<T> GetAllRecipesByName<T>(string search, int page, int itemsPerPage);

        T GetById<T>(int id);

        Task UpdateAsync(int id, EditRecipeInputModel input);

        Task DeleteAsync(int id);

        int GetRecipesCount();

        int GetRecipesCountByName(string search);
    }
}
