namespace Recipes.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using Recipes.Data.Common.Repositories;
    using Recipes.Data.Models;
    using Recipes.Web.ViewModels.Home;

    public class CountsService : ICountsService
    {
        private readonly IDeletableEntityRepository<Recipe> recipesRepository;
        private readonly IDeletableEntityRepository<Ingredient> ingredientsRepository;
        private readonly IDeletableEntityRepository<Category> categoriesRepository;
        private readonly IDeletableEntityRepository<FavoriteRecipe> favoriteRecipesRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;
        private readonly IDeletableEntityRepository<Message> messagesRepository;

        public CountsService(
            IDeletableEntityRepository<Recipe> recipesRepository,
            IDeletableEntityRepository<Ingredient> ingredientsRepository,
            IDeletableEntityRepository<Category> categoriesRepository,
            IDeletableEntityRepository<FavoriteRecipe> favoriteRecipesRepository,
            IDeletableEntityRepository<ApplicationUser> usersRepository,
            IDeletableEntityRepository<Message> messagesRepository)
        {
            this.recipesRepository = recipesRepository;
            this.ingredientsRepository = ingredientsRepository;
            this.categoriesRepository = categoriesRepository;
            this.favoriteRecipesRepository = favoriteRecipesRepository;
            this.usersRepository = usersRepository;
            this.messagesRepository = messagesRepository;
        }

        public IndexViewModel GetStats()
        {
            var data = new IndexViewModel
            {
                UsersCount = this.usersRepository.AllAsNoTracking().Count(),
                ApprovedRecipesCount = this.recipesRepository.AllAsNoTracking().Where(x => x.IsApproved).Count(),
                WaitingForApprovalRecipesCount = this.recipesRepository.AllAsNoTracking().Where(x => x.IsApproved == false).Count(),
                IngredientsCount = this.ingredientsRepository.AllAsNoTracking().Count(),
                CategoriesCount = this.categoriesRepository.AllAsNoTracking().Count(),
            };

            return data;
        }

        public async Task IncreaseViews(int id, bool forCategory)
        {
            if (forCategory == false)
            {
                var recipe = this.recipesRepository.All().Where(x => x.Id == id).FirstOrDefault();
                if (recipe == null)
                {
                    return;
                }

                recipe.ViewsCount += 1;

                await this.recipesRepository.SaveChangesAsync();
            }
            else if (forCategory == true)
            {
                var category = this.categoriesRepository.All().Where(x => x.Id == id).FirstOrDefault();
                if (category == null)
                {
                    return;
                }

                category.ViewsCount += 1;

                await this.categoriesRepository.SaveChangesAsync();
            }
        }

        public int GetRecipesCount()
        {
            return this.recipesRepository
                .AllAsNoTracking()
                .Where(x => x.IsApproved == true)
                .Count();
        }

        public int GetFavoriteRecipesCount(string userId)
        {
            return this.favoriteRecipesRepository
                .AllAsNoTracking()
                .Where(x => x.AddedByUserId == userId && x.Recipe.IsApproved == true)
                .Count();
        }

        public int GetRecipesCountByName(string search)
        {
            return this.recipesRepository
                .AllAsNoTracking()
                .Where(x => x.Name.Contains(search.ToLower().Trim()) && x.IsApproved == true)
                .Count();
        }

        public int GetRecipesCountByCategoryId(int id)
        {
            return this.recipesRepository
                .AllAsNoTracking()
                .Where(x => x.CategoryId == id && x.IsApproved == true)
                .Count();
        }

        public int GetUnapprovedRecipesCount()
        {
            return this.recipesRepository
                .AllAsNoTracking()
                .Where(x => x.IsApproved == false)
                .Count();
        }

        public int GetMessagesCount()
        {
            return this.messagesRepository
                .AllAsNoTracking()
                .Count();
        }
    }
}
