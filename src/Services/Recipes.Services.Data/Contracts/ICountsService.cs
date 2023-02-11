﻿namespace Recipes.Services.Data.Contracts
{
    using System.Threading.Tasks;

    using Recipes.Web.ViewModels.Home;

    public interface ICountsService
    {
        IndexViewModel GetStats();

        Task IncreaseViews(int id, bool forCategory);

        int GetRecipesCount();

        int GetFavoriteRecipesCount(string userId);

        int GetRecipesCountByName(string search);

        int GetRecipesCountByCategoryId(int id);

        int GetUnapprovedRecipesCount();

        int GetPersonalRecipesCount(string userId);

        int GetMessagesCount();
    }
}