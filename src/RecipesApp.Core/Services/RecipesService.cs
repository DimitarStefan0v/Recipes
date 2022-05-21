using RecipesApp.Core.Contracts;
using RecipesApp.Core.Models;
using RecipesApp.Infrastructure.Data;
using RecipesApp.Infrastructure.Data.Repositories;

namespace RecipesApp.Core.Services
{
    public class RecipesService : IRecipesService
    {
        private readonly IApplicationDbRepository repo;

        public RecipesService(IApplicationDbRepository _repo)
        {
            repo = _repo;
        }

        public async Task CreateAsync(RecipeInputModel input, string userId)
        {
            var category = repo
                .All<Category>()
                .FirstOrDefault(c => c.Id == input.CategoryId);

            if (category == null)
            {
                category = repo
                    .All<Category>()
                    .Where(c => c.Name == "Други")
                    .FirstOrDefault();
            }

            var recipe = new Recipe
            {
                Name = input.Name,
                Instructions = input.Instructions,
                PreparationTime = TimeSpan.FromMinutes(input.PreparationTime),
                CookingTime = TimeSpan.FromMinutes(input.CookingTime),
                PortionsCount = input.PortionsCount,
                Category = category,
                AddedByUserId = userId,
            };

            foreach (var ingredientInput in input.Ingredients)
            {
                var ingredient = repo
                    .All<Ingredient>()
                    .FirstOrDefault(x => x.Name == ingredientInput.Name);

                if (ingredient == null)
                {
                    ingredient = new Ingredient()
                    {
                        Name = ingredientInput.Name,
                    };
                }

                recipe.Ingredients.Add(new RecipeIngredient
                {
                    Ingredient = ingredient,
                    Quantity = ingredientInput.Quantity,
                });
            }

            await repo.AddAsync(recipe);
            await repo.SaveChangesAsync();
        }

    }
}
