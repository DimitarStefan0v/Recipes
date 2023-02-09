﻿namespace Recipes.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Recipes.Web.ViewModels.Home;

    public interface IUsersService
    {
        Task RemoveRecipeFromFavoritesAsync(int recipeId, string userId);

        bool IsRecipeInUserFavorites(int recipeId, string userId);

        public IEnumerable<T> GetFavorites<T>(int page, int itemsPerPage, string userId);
    }
}
