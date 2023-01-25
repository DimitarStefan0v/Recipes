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

        public CountsService(
            IDeletableEntityRepository<Recipe> recipesRepository,
            IDeletableEntityRepository<Ingredient> ingredientsRepository,
            IDeletableEntityRepository<Category> categoriesRepository)
        {
            this.recipesRepository = recipesRepository;
            this.ingredientsRepository = ingredientsRepository;
            this.categoriesRepository = categoriesRepository;
        }

        public IndexStatsViewModel GetStats()
        {
            var data = new IndexStatsViewModel
            {
                RecipesCount = this.recipesRepository.AllAsNoTracking().Where(x => x.IsApproved).Count(),
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

                category.ViewCount += 1;

                await this.categoriesRepository.SaveChangesAsync();
            }
        }
    }
}
