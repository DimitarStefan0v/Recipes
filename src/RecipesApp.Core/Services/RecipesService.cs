using Microsoft.EntityFrameworkCore;
using RecipesApp.Core.Constants;
using RecipesApp.Core.Contracts;
using RecipesApp.Core.Models;
using RecipesApp.Infrastructure.Data;
using RecipesApp.Infrastructure.Data.Identity;
using RecipesApp.Infrastructure.Data.Repositories;

namespace RecipesApp.Core.Services
{
    public class RecipesService : IRecipesService
    {
        private readonly IApplicationDbRepository repo;
        private readonly IImageDbService imageDbService;
        private readonly ICloudImageService cloudImageService;
        private readonly IVotesService votesService;
        private readonly ApplicationDbContext applicationDbContext; // Used only for Hard Delete

        public RecipesService(IApplicationDbRepository _repo,
            IImageDbService _imageDbService,
            ICloudImageService _cloudImageService,
            IVotesService _votesService,
            ApplicationDbContext _applicationDbContext)
        {
            repo = _repo;
            imageDbService = _imageDbService;
            cloudImageService = _cloudImageService;
            votesService = _votesService;
            applicationDbContext = _applicationDbContext;
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
                    .FirstOrDefault(x => x.Name.ToLower() == ingredientInput.IngredientName.ToLower());

                if (ingredient == null)
                {
                    ingredient = new Ingredient()
                    {
                        Name = ingredientInput.IngredientName.ToLower(),
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
                .Where(x => x.IsDeleted == false)
                .OrderByDescending(x => x.CreatedOn)
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
                .Include(x => x.Comments)
                .Where(x => x.Id == id && x.IsDeleted == false)
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
                AverageVotesValue = votesService.GetAverageVotes(recipe.Id),
            };

            foreach (var comment in recipe.Comments.Where(x => x.IsDeleted == false))
            {
                var commentViewModel = new CommentViewModel
                {
                    Id = comment.Id,
                    Content = comment.Content,
                    AddedByUser = comment.AddedByUser.ToString(),
                    CreatedOn = comment.CreatedOn,
                };

                if (commentViewModel != null)
                {
                    viewModelrecipe.Comments.Add(commentViewModel);
                }
            }

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
                        IngredientName = ingredientName.ToLower().Trim(),
                        Quantity = ingredientQuantity.ToLower().Trim(),
                    };

                    viewModelrecipe.Ingredients.Add(ingredient);
                }
            }

            return viewModelrecipe;
        }

        public int GetCount()
        {
            return repo.All<Recipe>().Where(x => x.IsDeleted == false).Count();
        }

        public IEnumerable<RecipeInListViewModel> GetRecentRecipes(int count = 3)
        {
            var recipes = repo.AllReadonly<Recipe>()
                .Where(x => x.IsDeleted == false)
                .OrderByDescending(x => x.CreatedOn)
                .Take(count)
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

        public IEnumerable<RecipeInListViewModel> GetMostCommentedRecipes(int count = 3)
        {
            var recipes = repo.AllReadonly<Recipe>()
                .Where(x => x.IsDeleted == false)
                .OrderByDescending(x => x.Comments.Count)
                .Take(count)
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

        public IEnumerable<RecipeInListViewModel> GetMostVotedRecipes(int count = 3)
        {
            var recipes = repo.AllReadonly<Recipe>()
                .Where(x => x.IsDeleted == false)
                .OrderByDescending(x => x.Votes.Count)
                .Take(count)
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

        public async Task UpdateAsync(int id, EditRecipeInputModel input)
        {
            var recipe = repo.All<Recipe>().FirstOrDefault(x => x.Id == id);

            var category = repo.All<Category>().FirstOrDefault(x => x.Id == input.CategoryId);

            if (category != null)
            {
                recipe.Category = category;
            }

            recipe.Name = input.Name;
            recipe.Instructions = input.Instructions;
            recipe.PreparationTime = TimeSpan.FromMinutes(input.PreparationTime);
            recipe.CookingTime = TimeSpan.FromMinutes(input.CookingTime);
            recipe.PortionsCount = input.PortionsCount;

            await repo.SaveChangesAsync();
        }

        public IEnumerable<RecipeInListViewModel> GetRecipesByIngredients(int page, IEnumerable<int> ingredientIds, int itemsPerPage = 12)
        {
            var query = repo.All<Recipe>().Where(x => x.IsDeleted == false);

            foreach (var ingredientId in ingredientIds)
            {
                query = query.Where(x => x.Ingredients.Any(i => i.IngredientId == ingredientId));
            }

            return query
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
        }

        public async Task DeleteAsync(int id)
        {
            var recipe = repo.All<Recipe>().FirstOrDefault(x => x.Id == id);
            recipe.IsDeleted = true;

            await repo.SaveChangesAsync();
        }

        public IEnumerable<RecipeInListViewModel> GetRecipesByName(string name, int page, int itemsPerPage = 12)
        {
            var recipes = repo.AllReadonly<Recipe>()
                .Where(x => x.IsDeleted == false && x.Name.Contains(name))
                .OrderByDescending(x => x.Name)
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

        public IEnumerable<RecipeInListViewModel> GetRecipesByCategory(int id, int page, int itemsPerPage = 12)
        {
            var recipes = repo.AllReadonly<Recipe>()
                .Where(x => x.Category.Id == id)
                .Where(x => x.IsDeleted == false)
                .OrderByDescending(x => x.CreatedOn)
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

        public IEnumerable<RecipeInListViewModel> GetRecipesByCategory(string name, int page, int itemsPerPage = 12)
        {
            var recipes = repo.AllReadonly<Recipe>()
                .Where(x => x.Category.Name == name)
                .Where(x => x.IsDeleted == false)
                .OrderByDescending(x => x.CreatedOn)
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

        public async Task AddFavoriteRecipeAsync(string userId, int recipeId)
        {
            var user = repo.All<ApplicationUser>()
                .Where(x => x.Id == userId)
                .Select(x => x.Id)
                .FirstOrDefault();

            if (user == null)
            {
                return;
            }

            var favoriteRecipe = repo.All<FavoriteRecipeId>()
                .Where(x => x.LikedByUserId == user && x.RecipeId == recipeId)
                .FirstOrDefault();

            if (favoriteRecipe != null)
            {
                return;   
            }

            favoriteRecipe = new FavoriteRecipeId
            {
                RecipeId = recipeId,
                LikedByUserId = user
            };

            await repo.AddAsync(favoriteRecipe);
            await repo.SaveChangesAsync();
        }

        public bool IsRecipeFavorite(string userId, int recipeId)
        {
            var user = repo.All<ApplicationUser>()
                .Where(x => x.Id == userId)
                .Select(x => x.Id)
                .FirstOrDefault();

            if (user == null)
            {
                return false;
            }

            var isRecipeFavorite = repo.AllReadonly<FavoriteRecipeId>()
                .Where(x => x.LikedByUserId == user && x.RecipeId == recipeId)
                .FirstOrDefault();

            if (isRecipeFavorite == null)
            {
                return false;
            }

            return true;
        }

        public IEnumerable<RecipeInListViewModel> GetFavoriteRecipes(string userId, int page, int itemsPerPage = 12)
        {
            var user = repo.AllReadonly<ApplicationUser>()
                .Where(x => x.Id == userId)
                .Select(x => x.Id)
                .FirstOrDefault();

            if (user == null)
            {
                return null;
            }

            var favoriteRecipes = repo.AllReadonly<FavoriteRecipeId>()
                .Where(x => x.LikedByUserId == user)
                .ToList();

            var recipesToReturn = new List<RecipeInListViewModel>();

            foreach (var favoriteRecipe in favoriteRecipes)
            {
                var recipe = repo.AllReadonly<Recipe>()
                    .Where(x => x.IsDeleted == false)
                    .Select(x => new RecipeInListViewModel
                    {
                        Id = x.Id,
                        Name = x.Name,
                        CategoryId = x.Category.Id,
                        CategoryName = x.Category.Name,
                        Image = x.Image.PictureUrl ?? DefaultImages.DefaultRecipeImageUrl
                    })
                    .FirstOrDefault(x => x.Id == favoriteRecipe.RecipeId);

                if (recipe != null)
                {
                    recipesToReturn.Add(recipe);
                }
            }

            recipesToReturn
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .ToList();

            
            return recipesToReturn;
        }

        public async Task DeleteFavoriteRecipe(string userId, int recipeId)
        {
            var favoriteRecipeToRemove = repo.AllReadonly<FavoriteRecipeId>()
                .Where(x => x.LikedByUserId == userId && x.RecipeId == recipeId)
                .FirstOrDefault();

            if (favoriteRecipeToRemove != null)
            {
                applicationDbContext.FavoriteRecipeIds.Remove(favoriteRecipeToRemove);
                // ApplicationDbContext used only here for HardDelete
                await repo.SaveChangesAsync();
            }
        }

    }
}
