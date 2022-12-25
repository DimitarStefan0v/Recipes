namespace Recipes.Services.Data
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

        public RecipesService(
            IDeletableEntityRepository<Recipe> recipesRepository,
            IDeletableEntityRepository<Ingredient> ingredientsRepository)
        {
            this.recipesRepository = recipesRepository;
            this.ingredientsRepository = ingredientsRepository;
        }

        public async Task CreateAsync(CreateRecipeInputModel input, string userId)
        {
            var recipe = new Recipe
            {
                Name = input.Name,
                Description = input.Description,
                PreparationTime = TimeSpan.FromMinutes(Convert.ToDouble(input.PreparationTime == 0 ? null : input.PreparationTime)),
                CookingTime = TimeSpan.FromMinutes(Convert.ToDouble(input.CookingTime == 0 ? null : input.PreparationTime)),
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
