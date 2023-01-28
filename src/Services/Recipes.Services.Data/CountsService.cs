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

        public CountsService(
            IDeletableEntityRepository<Recipe> recipesRepository,
            IDeletableEntityRepository<Ingredient> ingredientsRepository,
            IDeletableEntityRepository<Category> categoriesRepository,
            IDeletableEntityRepository<FavoriteRecipe> favoriteRecipesRepository)
        {
            this.recipesRepository = recipesRepository;
            this.ingredientsRepository = ingredientsRepository;
            this.categoriesRepository = categoriesRepository;
            this.favoriteRecipesRepository = favoriteRecipesRepository;
        }

        public IndexStatsViewModel GetStats()
        {
            var data = new IndexStatsViewModel
            {
                RecipesCount = this.recipesRepository.AllAsNoTracking().Count(),
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
                .Count();
        }

        public int GetFavoriteRecipesCount(string userId)
        {
            return this.favoriteRecipesRepository
                .AllAsNoTracking()
                .Where(x => x.AddedByUserId == userId)
                .Count();
        }

        public int GetRecipesCountByName(string search)
        {
            return this.recipesRepository
                .AllAsNoTracking()
                .Where(x => x.Name.Contains(search.ToLower().Trim()))
                .Count();
        }

        public int GetRecipesCountByCategoryId(int id)
        {
            return this.recipesRepository
                .AllAsNoTracking()
                .Where(x => x.CategoryId == id)
                .Count();
        }

        public int GetUnapprovedRecipesCount()
        {
            return this.recipesRepository
                .AllAsNoTracking()
                .Where(x => x.IsApproved == false)
                .Count();
        }
    }
}
