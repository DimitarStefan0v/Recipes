﻿namespace Recipes.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CloudinaryDotNet.Actions;
    using Recipes.Data.Common.Repositories;
    using Recipes.Data.Models;
    using Recipes.Services.Data.Contracts;
    using Recipes.Services.Mapping;
    using Recipes.Web.ViewModels.Recipes;

    public class RecipesService : IRecipesService
    {
        private readonly IDeletableEntityRepository<Recipe> recipesRepository;
        private readonly IDeletableEntityRepository<Ingredient> ingredientsRepository;
        private readonly IDeletableEntityRepository<Category> categoriesRepository;
        private readonly IDeletableEntityRepository<FavoriteRecipe> favoriteRecipesRepository;
        private readonly ICloudImagesService imagesService;

        public RecipesService(
            IDeletableEntityRepository<Recipe> recipesRepository,
            IDeletableEntityRepository<Ingredient> ingredientsRepository,
            IDeletableEntityRepository<Category> categoriesRepository,
            IDeletableEntityRepository<FavoriteRecipe> favoriteRecipesRepository,
            ICloudImagesService imagesService)
        {
            this.recipesRepository = recipesRepository;
            this.ingredientsRepository = ingredientsRepository;
            this.categoriesRepository = categoriesRepository;
            this.favoriteRecipesRepository = favoriteRecipesRepository;
            this.imagesService = imagesService;
        }

        public async Task CreateAsync(CreateRecipeInputModel input, string userId)
        {
            if (input.CategoryId == 0)
            {
                input.CategoryId = this.categoriesRepository
                    .AllAsNoTracking()
                    .Where(x => x.Name == "Други")
                    .Select(x => x.Id)
                    .FirstOrDefault();
            }

            string imgUrl = null;
            string imgPubId = null;
            ImageUploadResult imgResult = null;

            try
            {
                imgResult = await this.imagesService.UploadImageAsync(input.Image);
            }
            catch (Exception)
            {
            }

            imgUrl = imgResult == null ? null : imgResult.SecureUrl.AbsoluteUri;
            imgPubId = imgResult == null ? null : imgResult.PublicId;

            var imageToWrite = new CloudImage
            {
                PictureUrl = imgUrl,
                PicturePublicId = imgPubId,
                AddedByUserId = userId,
            };

            var recipe = new Recipe
            {
                Name = input.Name.Trim(),
                Description = input.Description.Trim(),
                PreparationTime = TimeSpan.FromMinutes(Convert.ToDouble(input.PreparationTime)),
                CookingTime = TimeSpan.FromMinutes(Convert.ToDouble(input.CookingTime)),
                PortionsCount = input.PortionsCount,
                CategoryId = input.CategoryId,
                AddedByUserId = userId,
                Image = imageToWrite,
                IsApproved = false,
            };

            this.AddIngredientsToRecipe(input, recipe);

            await this.recipesRepository.AddAsync(recipe);
            await this.recipesRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var recipe = this.recipesRepository.All().Where(x => x.Id == id).FirstOrDefault();

            this.recipesRepository.Delete(recipe);
            await this.recipesRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, EditRecipeInputModel input)
        {
            var recipe = this.recipesRepository
                .All()
                .Where(x => x.Id == id)
                .FirstOrDefault();

            if (recipe.Name != input.Name.Trim())
            {
                recipe.Name = input.Name.Trim();
            }

            if (recipe.Description != input.Description.Trim())
            {
                recipe.Description = input.Description.Trim();
            }

            if (input.PreparationTime > 0 && recipe.PreparationTime != TimeSpan.FromMinutes(Convert.ToDouble(input.PreparationTime)))
            {
                recipe.PreparationTime = TimeSpan.FromMinutes(Convert.ToDouble(input.PreparationTime));
            }

            if (input.CookingTime > 0 && recipe.CookingTime != TimeSpan.FromMinutes(Convert.ToDouble(input.CookingTime)))
            {
                recipe.CookingTime = TimeSpan.FromMinutes(Convert.ToDouble(input.CookingTime));
            }

            if (recipe.PortionsCount != input.PortionsCount)
            {
                recipe.PortionsCount = input.PortionsCount;
            }

            recipe.IsApproved = false;
            await this.recipesRepository.SaveChangesAsync();
        }

        public T GetById<T>(int id)
        {
            return this.recipesRepository
                .AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefault();
        }

        public IEnumerable<T> GetAll<T>(string sort, int page, int itemsPerPage)
        {
            var query = this.recipesRepository
                .AllAsNoTracking()
                .Where(x => x.IsApproved == true)
                .AsQueryable();

            SortRecipes(ref sort, ref query);

            return query.Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .To<T>()
                .ToList();
        }

        public IEnumerable<T> GetAllRecipesByName<T>(string search, string sort, int page, int itemsPerPage)
        {
            var query = this.recipesRepository
                .AllAsNoTracking()
                .Where(x => x.Name.Contains(search.ToLower().Trim()) && x.IsApproved == true)
                .AsQueryable();

            SortRecipes(ref sort, ref query);

            return query.Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .To<T>()
                .ToList();
        }

        public IEnumerable<T> GetRecipesByCategoryId<T>(int categoryId, string sort, int page, int itemsPerPage)
        {
            var query = this.recipesRepository
                .AllAsNoTracking()
                .Where(x => x.CategoryId == categoryId && x.IsApproved == true)
                .AsQueryable();

            SortRecipes(ref sort, ref query);

            return query.Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .To<T>()
                .ToList();
        }

        public IEnumerable<T> GetAllUnapproved<T>(int page, int itemsPerPage)
        {
            return this.recipesRepository
                .AllAsNoTracking()
                .Where(x => x.IsApproved == false)
                .OrderByDescending(x => x.CreatedOn)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .To<T>()
                .ToList();
        }

        public IEnumerable<T> GetPersonalRecipes<T>(string sort, int page, int itemsPerPage, string userId)
        {
            var query = this.recipesRepository
                .AllAsNoTracking()
                .Where(x => x.IsApproved == true && x.AddedByUserId == userId)
                .AsQueryable();

            SortRecipes(ref sort, ref query);

            return query.Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .To<T>()
                .ToList();
        }

        public async Task ApproveRecipe(int id)
        {
            var recipe = this.recipesRepository
                            .All()
                            .Where(x => x.Id == id)
                            .FirstOrDefault();

            if (recipe == null)
            {
                return;
            }

            recipe.IsApproved = true;

            await this.recipesRepository.SaveChangesAsync();
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

        public bool IsRecipeInUserFavorites(int recipeId, string userId)
        {
            var favoriteRecipe = this.favoriteRecipesRepository
                .AllAsNoTracking()
                .Where(x => x.RecipeId == recipeId && x.AddedByUserId == userId)
                .FirstOrDefault();

            return favoriteRecipe == null ? false : true;
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

        public IEnumerable<T> GetFavorites<T>(int page, int itemsPerPage, string userId)
        {
            var favoriteRecipes = this.favoriteRecipesRepository
                .AllAsNoTracking()
                .Where(x => x.AddedByUserId == userId)
                .OrderByDescending(x => x.CreatedOn)
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

                if (recipe == null)
                {
                    continue;
                }

                recipesToReturn.Add(recipe);
            }

            return recipesToReturn
                        .Skip((page - 1) * itemsPerPage)
                        .Take(itemsPerPage)
                        .ToList();
        }

        public string GetAuhorId(int id)
        {
            var authorId = this.recipesRepository
                            .AllAsNoTracking()
                            .Where(x => x.Id == id)
                            .Select(x => x.AddedByUserId)
                            .FirstOrDefault();

            return authorId;
        }

        private static void SortRecipes(ref string sort, ref IQueryable<Recipe> query)
        {
            if (sort == null)
            {
                sort = "descending";
            }

            sort = sort.ToLower().Trim();

            switch (sort)
            {
                case "ascending":
                    query = query.OrderBy(x => x.CreatedOn).ThenBy(x => x.Name);
                    break;
                case "descending":
                    query = query.OrderByDescending(x => x.CreatedOn).ThenBy(x => x.Name);
                    break;
                case "popularity":
                    query = query.OrderByDescending(x => x.ViewsCount).ThenBy(x => x.Name);
                    break;
                case "comments":
                    query = query.OrderByDescending(x => x.Comments.Count()).ThenBy(x => x.Name);
                    break;
                default:
                    sort = "descending";
                    query = query.OrderByDescending(x => x.CreatedOn).ThenBy(x => x.Name);
                    break;
            }
        }

        private void AddIngredientsToRecipe(CreateRecipeInputModel input, Recipe recipe)
        {
            var ingredients = this.ingredientsRepository.AllAsNoTracking().ToList();

            foreach (var ingredientInput in input.Ingredients)
            {
                if (string.IsNullOrWhiteSpace(ingredientInput.IngredientName))
                {
                    continue;
                }

                ingredientInput.IngredientName = ingredientInput.IngredientName
                                                                    .ToLower().Trim();

                var ingredient = ingredients
                                    .Where(i => i.Name == ingredientInput.IngredientName)
                                    .FirstOrDefault();

                if (ingredient == null)
                {
                    ingredient = new Ingredient
                    {
                        Name = ingredientInput.IngredientName.ToLower().Trim(),
                    };

                    recipe.Ingredients.Add(new RecipeIngredient
                    {
                        Ingredient = ingredient,
                        Quantity = ingredientInput.Quantity == null ? null : ingredientInput.Quantity.ToLower().Trim(),
                    });

                    continue;
                }

                recipe.Ingredients.Add(new RecipeIngredient
                {
                    IngredientId = ingredient.Id,
                    Quantity = ingredientInput.Quantity == null ? null : ingredientInput.Quantity.ToLower().Trim(),
                });
            }
        }
    }
}
