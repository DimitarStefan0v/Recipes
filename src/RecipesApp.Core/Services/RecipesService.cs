using Microsoft.EntityFrameworkCore;
using RecipesApp.Core.Constants;
using RecipesApp.Core.Contracts;
using RecipesApp.Core.Models;
using RecipesApp.Infrastructure.Data;
using RecipesApp.Infrastructure.Data.Repositories;

namespace RecipesApp.Core.Services
{
    public class RecipesService : IRecipesService
    {
        private readonly IApplicationDbRepository repo;
        private readonly IImageDbService imageDbService;
        private readonly ICloudImageService cloudImageService;

        public RecipesService(IApplicationDbRepository _repo,
            IImageDbService _imageDbService,
            ICloudImageService _cloudImageService)
        {
            repo = _repo;
            imageDbService = _imageDbService;
            cloudImageService = _cloudImageService;
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

            var imgResult = await cloudImageService
                .UploadImageAsync(input.Image);

            string imgUrl = imgResult.SecureUri.AbsoluteUri;
            string imgPubId = imgResult.PublicId;

            var imageToWrite = new CloudImage
            {
                PictureUrl = imgUrl,
                PicturePublicId = imgPubId,
                AddedByUserId = userId
            };

            var recipe = new Recipe
            {
                Name = input.Name,
                Instructions = input.Instructions,
                PreparationTime = TimeSpan.FromMinutes(input.PreparationTime),
                CookingTime = TimeSpan.FromMinutes(input.CookingTime),
                PortionsCount = input.PortionsCount,
                Category = category,
                AddedByUserId = userId,
                Image = imageToWrite,
                CreatedOn = DateTime.UtcNow
            };


            foreach (var ingredientInput in input.Ingredients)
            {
                var ingredient = repo
                    .All<Ingredient>()
                    .FirstOrDefault(x => x.Name == ingredientInput.IngredientName);

                if (ingredient == null)
                {
                    ingredient = new Ingredient()
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

            await repo.AddAsync(recipe);
            await repo.SaveChangesAsync();
        }

        public IEnumerable<RecipeInListViewModel> GetAll(int page, int itemsPerPage = 12)
        {
            var recipes = repo.AllReadonly<Recipe>()
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .Select(x => new RecipeInListViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    CategoryId = x.Category.Id,
                    CategoryName = x.Category.Name,
                    Image = x.Image.PictureUrl ?? DefaultImages.DefaultRecipeImageUrl
                }).ToList();

            return recipes;
        }

        public SingleRecipeViewModel GetById(int id)
        {
            var recipe = repo.All<Recipe>()
                .Include(x => x.Category)
                .Include(x => x.Image)
                .Include(x => x.Ingredients)
                .Include(x => x.AddedByUser)
                .Where(x => x.Id == id)
                .FirstOrDefault();

            var viewModelrecipe = new SingleRecipeViewModel
            {
                Id = recipe.Id,
                Name = recipe.Name,
                Instructions = recipe.Instructions,
                CategoryName = recipe.Category.Name,
                CookingTime = recipe.CookingTime,
                PreparationTime = recipe.PreparationTime,
                CreatedOn = recipe.CreatedOn,
                ImageUrl = recipe.Image?.PictureUrl ?? DefaultImages.DefaultRecipeImageUrl,
                AddedByUser = recipe.AddedByUser.UserName,
                PortionsCount = recipe.PortionsCount,
            };

            var ingredientsId = recipe.Ingredients.Select(x => x.IngredientId).ToList();

            foreach (var item in ingredientsId)
            {
                var ingredientName = repo.All<Ingredient>()
                    .Where(x => x.Id == item)
                    .Select(x => x.Name)
                    .FirstOrDefault();

                var ingredientQuantity = recipe.Ingredients
                    .Where(x => x.IngredientId == item)
                    .Select(x => x.Quantity)
                    .FirstOrDefault();

                if (ingredientName != null && ingredientQuantity != null)
                {
                    var ingredient = new IngredientsViewModel
                    {
                        IngredientName = ingredientName,
                        Quantity = ingredientQuantity,
                    };

                    viewModelrecipe.Ingredients.Add(ingredient);
                }
            }

            return viewModelrecipe;
        }

        public int GetCount()
        {
            return repo.All<Recipe>().Count();
        }
    }
}
