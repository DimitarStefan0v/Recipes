﻿namespace Recipes.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Recipes.Data.Common.Repositories;
    using Recipes.Data.Models;
    using Recipes.Services.Mapping;
    using Recipes.Web.ViewModels.Home;

    public class UsersService : IUsersService
    {
        private readonly IDeletableEntityRepository<Message> messagesRepository;
        private readonly IDeletableEntityRepository<FavoriteRecipe> favoriteRecipesRepository;
        private readonly IDeletableEntityRepository<Recipe> recipesRepository;

        public UsersService(
            IDeletableEntityRepository<Message> messagesRepository,
            IDeletableEntityRepository<FavoriteRecipe> favoriteRecipesRepository,
            IDeletableEntityRepository<Recipe> recipesRepository)
        {
            this.messagesRepository = messagesRepository;
            this.favoriteRecipesRepository = favoriteRecipesRepository;
            this.recipesRepository = recipesRepository;
        }

        public async Task AddRecipeToFavoritesAsync(int recipeId, string userId)
        {
            var recipe = this.recipesRepository.AllAsNoTracking().Where(x => x.Id == recipeId).FirstOrDefault();
            if (userId == null || recipe == null)
            {
                return;
            }

            var favoriteRecipe = new FavoriteRecipe
            {
                RecipeId = recipeId,
                AddedByUserId = userId,
            };

            await this.favoriteRecipesRepository.AddAsync(favoriteRecipe);
            await this.favoriteRecipesRepository.SaveChangesAsync();
        }

        public async Task RemoveRecipeFromFavoritesAsync(int recipeId, string userId)
        {
            var recipe = this.favoriteRecipesRepository
                .All()
                .Where(x => x.RecipeId == recipeId && x.AddedByUserId == userId)
                .FirstOrDefault();

            if (recipe == null)
            {
                return;
            }

            this.favoriteRecipesRepository.HardDelete(recipe);
            await this.favoriteRecipesRepository.SaveChangesAsync();
        }

        public async Task CreateMessageAsync(ContactInputModel input, string userId)
        {
            var message = new Message
            {
                Title = input.Title.Trim(),
                Content = input.Content.Trim(),
                Name = input.Name.Trim(),
                Email = input.Email.Trim(),
                Ip = input.Ip,
                AddedByUserId = userId,
            };

            await this.messagesRepository.AddAsync(message);
            await this.messagesRepository.SaveChangesAsync();
        }

        public bool IsRecipeInUserFavorites(int recipeId, string userId)
        {
            var favoriteRecipe = this.favoriteRecipesRepository
                .AllAsNoTracking()
                .Where(x => x.RecipeId == recipeId && x.AddedByUserId == userId)
                .FirstOrDefault();

            return favoriteRecipe == null ? false : true;
        }

        public IEnumerable<T> GetFavorites<T>(int page, int itemsPerPage, string userId)
        {
            var favoriteRecipes = this.favoriteRecipesRepository
                .AllAsNoTracking()
                .Where(x => x.AddedByUserId == userId)
                .Select(x => x.RecipeId)
                .ToList();

            var recipesToReturn = new HashSet<T>();

            foreach (var favoriteRecipeId in favoriteRecipes)
            {
                var recipe = this.recipesRepository
                    .AllAsNoTracking()
                    .Where(x => x.Id == favoriteRecipeId && x.IsApproved == true)
                    .To<T>()
                    .FirstOrDefault();

                recipesToReturn.Add(recipe);
            }

            return recipesToReturn;
        }
    }
}
