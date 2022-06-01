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
            
            foreach (var image in input.Images)
            {
                var imgResult = await cloudImageService
                .UploadImageAsync(image);

                string imgUrl = imgResult.SecureUri.AbsoluteUri;
                string imgPubId = imgResult.PublicId;

                var imageToWrite = new CloudImage
                {
                    PictureUrl = imgUrl,
                    PicturePublicId = imgPubId,
                };

                recipe.Images.Add(imageToWrite);
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
                    // TODO: Images
                }).ToList();

            return recipes;
        }

        public int GetCount()
        {
            return repo.All<Recipe>().Count();
        }
    }
}
