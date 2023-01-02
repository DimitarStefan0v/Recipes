﻿namespace Recipes.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Recipes.Data.Common.Repositories;
    using Recipes.Data.Models;
    using Recipes.Web.ViewModels.Recipes;

    public class RecipesService : IRecipesService
    {
        private readonly IDeletableEntityRepository<Recipe> recipesRepository;
        private readonly IDeletableEntityRepository<Ingredient> ingredientsRepository;
        private readonly IDeletableEntityRepository<Category> categoriesRepository;

        public RecipesService(
            IDeletableEntityRepository<Recipe> recipesRepository,
            IDeletableEntityRepository<Ingredient> ingredientsRepository,
            IDeletableEntityRepository<Category> categoriesRepository)
        {
            this.recipesRepository = recipesRepository;
            this.ingredientsRepository = ingredientsRepository;
            this.categoriesRepository = categoriesRepository;
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

            var recipe = new Recipe
            {
                Name = input.Name,
                Description = input.Description,
                PreparationTime = TimeSpan.FromMinutes(Convert.ToDouble(input.PreparationTime)),
                CookingTime = TimeSpan.FromMinutes(Convert.ToDouble(input.CookingTime)),
                PortionsCount = input.PortionsCount,
                CategoryId = input.CategoryId,
                AddedByUserId = userId,
            };

            foreach (var ingredientInput in input.Ingredients)
            {
                var ingredient = this.ingredientsRepository
                    .AllAsNoTracking()
                    .FirstOrDefault(i => i.Name == ingredientInput.IngredientName);

                if (ingredient == null)
                {
                    ingredient = new Ingredient
                    {
                        Name = ingredientInput.IngredientName,
                    };
                }

                recipe.Ingredients.Add(new RecipeIngredient
                {
                    Ingredient = ingredient,
                    Quantity = ingredientInput.Quantity,
                });
            }

            await this.recipesRepository.AddAsync(recipe);
            await this.recipesRepository.SaveChangesAsync();
        }
    }
}
