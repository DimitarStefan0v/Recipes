namespace Recipes.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using Recipes.Data.Common.Repositories;
    using Recipes.Data.Models;
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
        }

        public async Task CreateMessageAsync(ContactInputModel input, string userId)
        {
            var message = new Message
            {
                 Title = input.Title.Trim(),
                 Content = input.Content.Trim(),
                 Name = input.Name.Trim(),
                 Email = input.Email.Trim(),
                 AddedByUserId = userId,
            };

            await this.messagesRepository.AddAsync(message);
            await this.messagesRepository.SaveChangesAsync();
        }
    }
}
