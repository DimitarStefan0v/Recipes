namespace Recipes.Services.Data
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

            return recipesToReturn
                        .Skip((page - 1) * itemsPerPage)
                        .Take(itemsPerPage)
                        .ToList();
        }
    }
}
