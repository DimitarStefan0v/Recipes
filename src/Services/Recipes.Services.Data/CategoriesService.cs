namespace Recipes.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using Recipes.Data.Common.Repositories;
    using Recipes.Data.Models;
    using Recipes.Services.Mapping;

    public class CategoriesService : ICategoriesService
    {
        private readonly IDeletableEntityRepository<Category> categoriesRepository;

        public CategoriesService(IDeletableEntityRepository<Category> categoriesRepository)
        {
            this.categoriesRepository = categoriesRepository;
        }

        public ICollection<T> GetCategories<T>()
        {
            return this.categoriesRepository.AllAsNoTracking().To<T>().ToList();
        }
    }
}
