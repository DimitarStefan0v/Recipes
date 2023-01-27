﻿namespace Recipes.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Recipes.Web.ViewModels.Home;
    using Recipes.Web.ViewModels.Recipes;

    public interface IUsersService
    {
        Task CreateMessageAsync(ContactInputModel input, string userId);

        Task AddRecipeToFavoritesAsync(int recipeId, string userId);

        Task RemoveRecipeFromFavoritesAsync(int recipeId, string userId);

        bool IsRecipeInUserFavorites(int recipeId, string userId);

        public IEnumerable<T> GetFavorites<T>(int page, int itemsPerPage, string userId);
    }
}
