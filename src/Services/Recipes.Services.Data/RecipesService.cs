namespace Recipes.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Recipes.Data.Common.Repositories;
    using Recipes.Data.Models;
    using Recipes.Services.Mapping;
    using Recipes.Web.ViewModels.Recipes;

    public class RecipesService : IRecipesService
    {
        private readonly IDeletableEntityRepository<Recipe> recipesRepository;
        private readonly IDeletableEntityRepository<Ingredient> ingredientsRepository;
        private readonly IDeletableEntityRepository<Category> categoriesRepository;
        private readonly ICloudImagesService imagesService;

        public RecipesService(
            IDeletableEntityRepository<Recipe> recipesRepository,
            IDeletableEntityRepository<Ingredient> ingredientsRepository,
            IDeletableEntityRepository<Category> categoriesRepository,
            ICloudImagesService imagesService)
        {
            this.recipesRepository = recipesRepository;
            this.ingredientsRepository = ingredientsRepository;
            this.categoriesRepository = categoriesRepository;
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

            var imgResult = await this.imagesService.UploadImageAsync(input.Image);

            string imgUrl = imgResult.SecureUrl.AbsoluteUri;
            string imgPubId = imgResult.PublicId;

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
            };

            foreach (var ingredientInput in input.Ingredients)
            {
                this.AddIngredientsToRecipe(recipe, ingredientInput);
            }

            await this.recipesRepository.AddAsync(recipe);
            await this.recipesRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var recipe = this.recipesRepository.All().FirstOrDefault(x => x.Id == id);
            this.recipesRepository.Delete(recipe);
            await this.recipesRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.recipesRepository
                .AllAsNoTracking()
                .To<T>()
                .ToList();
        }

        public T GetById<T>(int id)
        {
            return this.recipesRepository
                .AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefault();
        }

        public async Task UpdateAsync(int id, EditRecipeInputModel input)
        {
            var recipe = this.recipesRepository.All().FirstOrDefault(x => x.Id == id);
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

            await this.recipesRepository.SaveChangesAsync();
        }

        private void AddIngredientsToRecipe(Recipe recipe, IngredientInputModel ingredientInput)
        {
            var ingredient = this.ingredientsRepository
                                    .AllAsNoTracking()
                                    .FirstOrDefault(i => i.Name.ToLower() == ingredientInput.IngredientName.ToLower().Trim());

            if (ingredient == null)
            {
                ingredient = new Ingredient
                {
                    Name = ingredientInput.IngredientName.ToLower().Trim(),
                };
            }

            recipe.Ingredients.Add(new RecipeIngredient
            {
                Ingredient = ingredient,
                Quantity = ingredientInput.Quantity.ToLower().Trim(),
            });
        }
    }
}
