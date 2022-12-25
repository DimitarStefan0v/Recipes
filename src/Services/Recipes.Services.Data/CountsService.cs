namespace Recipes.Services.Data
{
    using System.Linq;

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
                RecipesCount = this.recipesRepository.AllAsNoTracking().Count(),
                IngredientsCount = this.ingredientsRepository.AllAsNoTracking().Count(),
                CategoriesCount = this.categoriesRepository.AllAsNoTracking().Count(),
            };

            return data;
        }
    }
}
